using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Modules
{
    /// <summary>
    /// Class <c>FolderData</c> is a holder for the nested list of data.
    /// </summary>
    /// <typeparam name="T">The type of data the would be held.</typeparam>
    /// <param name="Data">The actual data</param>
    /// <param name="Children">The nested list of T which belong to the Data.</param>
    /// <param name="IsFile">Determines that if the Data is either a file or a folder.</param>
    public class FolderData<T>
    {
        public T Data { get; set; }
        public IEnumerable<FolderData<T>> Children { get; set; } = new List<FolderData<T>>();
        public bool IsFile { get => Children.None(); }

        /// <summary>
        /// It's a utility method to convert a nested list of T into a compact list of <c>FolderData</c>
        /// </summary>
        /// <param name="list">The actual list of nested data.</param>
        /// <param name="itemExtracter">It extracts the nesting list out of the parent data.</param>
        /// <returns>The compact list of just parents and children.</returns>
        public static IEnumerable<FolderData<T>> Compact(IEnumerable<T> list, Func<T, IEnumerable<T>> itemExtracter)
        {
            var folders = new List<FolderData<T>>();
            if (!list.None())
                foreach (var item in list)
                    folders.Add(new FolderData<T> { Data = item, Children = Compact(itemExtracter(item), itemExtracter) });
            return folders;
        }
    }
}
