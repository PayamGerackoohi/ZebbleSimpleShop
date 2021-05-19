using Domain.Database;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel.Base
{
    public abstract class EzPage : FullScreen
    {
        public List<EzSubPage> Children { get; private set; } = new();
        public EzPage PreviousPage;

        public virtual async Task OnBack()
        {
            Back();
            PreviousPage?.OnRefresh();
        }

        public virtual async Task OnRefresh()
        {
            Children.Do(child => child.Setup().RunInParallel());
        }

        public abstract Task Setup();

        public void EzBack(PageTransition transition = PageTransition.SlideBack) => Back(transition);

        public void EzForward<T>(PageTransition transition = PageTransition.SlideForward, Action<T> config = null) where T : EzPage => Forward<T>(vm => { vm.PreviousPage = this; vm.Setup(); config?.Invoke(vm); }, transition);

        public void EzGo<T>(PageTransition transition = PageTransition.SlideForward, Action<T> config = null) where T : EzPage => EzNav.Go(transition, config);
    }
}
