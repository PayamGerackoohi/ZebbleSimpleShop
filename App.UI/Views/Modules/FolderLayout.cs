using Domain.Utils;
using Domain.Modules;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zebble;
using ViewModel.Base;

namespace UI.Modules
{
    /// <summary> Class <c>FolderLayout</c> makes a file-folder style layout out of a nesting list of data.</summary>
    /// <param name="FolderData">Holds the compact version of the actual nested data<./param>
    /// <param name="ViewGen">Generates View from the data.</param>
    /// <param name="Indentation">The space added to the inner lists.</param>
    /// <param name="OnItemSelected">It would be called when a file is clicked and would return the actual data.</param>
    /// <param name="Duration">The duration of the animation</param>
    /// <param name="ModelHolder">The EzPage holder of the module</param>
    /// <example>
    /// 
    /// Zebble File:
    /// <code>
    /// <?xml version="1.0"?>
    /// <zbl xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    ///      xsi:noNamespaceSchemaLocation="../.zebble-schema.xml">
    ///    <class type="TestPage" base="Page" namespace="UI.Pages" viewmodel="ViewModel.TestPage">
    ///       <Modules.FolderLayout of="Domain.Models.Category, CategoryViewGen"
    ///                             OnItemSelected="@data => NotifyUser(data.Name)"
    ///                             ViewGen="@new CategoryViewGen()"
    ///                             FolderData="@await GetData()" />
    ///    </class>
    /// </zbl>
    /// </code>
    /// 
    /// C# File:
    /// <code>
    /// namespace UI.Pages
    /// {
    ///     partial class TestPage
    ///     {
    ///         private void NotifyUser(string message)
    ///         {
    ///             Alert.Show(message);
    ///         }
    /// 
    ///         public async Task<IEnumerable<FolderData<Category>>> GetData()
    ///         {
    ///             var categories = await Api.ShopApi.GetCategories();
    ///             return FolderData<Category>.Compact(categories, c => c.Categories);
    ///         }
    /// 
    ///     public class CategoryViewGen : IViewGenerator<Category>
    ///     {
    ///         public View Generate(Category data)
    ///         {
    ///             return new TextView { Text = data.Name };
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public class FolderLayout<T, TViewGen> : Stack where TViewGen : IViewGenerator<T>
    {
        public IEnumerable<FolderData<T>> FolderData { private get; set; }
        public TViewGen ViewGen { private get; set; }
        public int Indentation { get; set; } = 16;
        public Action<T> OnItemSelected { get; set; }
        public int? Duration { get; set; }
        public EzPage ModelHolder { get; set; }

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            Extract(FolderData, this, Indentation);
        }

        private void Extract(IEnumerable<FolderData<T>> list, View viewHolder, int padding)
        {
            foreach (var data in list)
            {
                if (data.IsFile)
                    viewHolder.Add(ViewGen.Generate(data.Data, false, ModelHolder).On(v => v.Tapped, () => OnItemSelected(data.Data)));
                else
                {
                    var expander = new ExpandableLayout()
                        .Set(x => x.Padding(left: padding))
                        .Set(x => x.ModelHolder = ModelHolder);
                    if (Duration != null)
                        expander.Duration = Duration.Value;
                    viewHolder.Add(ViewGen.Generate(data.Data, true, ModelHolder).On(v => v.Tapped, () => expander.Toggle()));
                    viewHolder.Add(expander);
                    Extract(data.Children, expander, padding + 8);
                }
            }
        }
    }

    /// <summary>
    /// An object who generates a view from a data.
    /// </summary>
    /// <typeparam name="T">The type of the data that would be turned into a view.</typeparam>
    /// <param name="data">The raw data containing the nested elements.</param>
    /// <param name="isHead">Determines whether the item is an expandable folder (header) or an actionable file.</param>
    /// <param name="holder">The EzPage holder of the module</param>
    public interface IViewGenerator<T>
    {
        View Generate(T data, bool isHead, EzPage holder);
    }
}
