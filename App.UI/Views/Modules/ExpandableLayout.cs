using Domain;
using Domain.Utils;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Plugin;
using ViewModel.Base;

namespace UI.Modules
{
    /// <summary>
    /// Class <c>ExpandableLayout</c> can expand, collpase or toggle its children views both vertically and horizontally.
    /// </summary>
    /// <param name="IsExpanded">The initial and current state of the expasnion.</param>
    /// <param name="Direction">The initial state of the direction of expansion and view layout.</param>
    /// <param name="Duration">The duration of the animation.</param>
    /// <param name="AnimationEasing">The AnimationEasing of the animation.</param>
    /// <param name="ModelHolder">The EzPage holder of the module</param>
    /// 
    /// <exmaple>
    /// <code>
    /// <?xml version="1.0"?>
    /// <zbl xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    ///      xsi:noNamespaceSchemaLocation="../.zebble-schema.xml">
    ///    <class type="TestPage" base="Page" namespace="UI.Pages" viewmodel="ViewModel.TestPage">
    ///       <Stack>
    ///          <Button Text="Toggle" on-Tapped="@Expander.Toggle()" />
    ///          <Modules.ExpandableLayout Id="Expander">
    ///             <TextView Text="Text 1" />
    ///             <TextView Text="Text 2" />
    ///             <TextView Text="Text 3" />
    ///          </Modules.ExpandableLayout>
    ///       </Stack>
    ///    </class>
    /// </zbl>
    /// </code>
    /// </exmaple>
    partial class ExpandableLayout : Canvas
    {
        private readonly Stack holder = new();
        public bool IsAnimating { get; private set; }

        public bool IsExpanded { get; set; }
        public RepeatDirection Direction { get => holder.Direction; set => holder.Direction = value; }
        public int Duration { get; set; } = 300;
        public AnimationEasing AnimationEasing { get; set; } = AnimationEasing.EaseInOut;
        public EzPage ModelHolder { get; set; }

        /// <summary>
        /// It adds views to the holder no the root view, in or to manipulate margin, padding, sizing, etc, without the need to notify any exterior view.
        /// </summary>
        /// <typeparam name="View">The ExpandableLayout who extends <c>View</c>.</typeparam>
        /// <param name="child">The view that is going to be added.</param>
        /// <param name="awaitNative">Determines whether it should wait until it's fully added to the real native UI.</param>
        /// <returns></returns>
        public override async Task<View> Add<View>(View child, bool awaitNative = false) => await holder.Add(child, awaitNative);

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await base.Add(holder);
        }

        public override async Task OnRendered()
        {
            await base.OnRendered();
            await SetupForInitialState();
        }

        private async Task SetupForInitialState()
        {
            if (!IsExpanded)
            {
                await holder.Animate(10.Milliseconds(), v => v.ScaleY(0f));
                this.Height(0f);
            }
        }

        /// <summary>
        /// It toggles between expansion and collapse
        /// </summary>
        /// <returns>Task (!important)</returns>
        public async Task Toggle() => await Expand(!IsExpanded);

        /// <summary>
        /// It collapses the whole layout
        /// </summary>
        /// <returns>Task (!important)</returns>
        public async Task Collapse() => await Expand(false);

        /// <summary>
        /// It expands or collapses the whole layout.
        /// </summary>
        /// <param name="expand">It determines if we wish to either expand or collapse (expand = false).</param>
        /// <returns>Task (!important)</returns>
        public async Task Expand(bool expand = true)
        {
            if (IsAnimating || (IsExpanded ^ !expand)) return;
            IsAnimating = true;
            //holder.ClipChildren = false;
            this.Height(holder.ActualHeight);
            await holder.Animate(Duration.Milliseconds(), AnimationEasing, v => v.ScaleY(expand ? 1f : 0f));
            if (expand)
                this.Height(Length.AutoStrategy.Content);
            else
                this.Height(0f);
            //holder.ClipChildren = true;
            IsAnimating = false;
            IsExpanded = expand;
        }
    }
}
