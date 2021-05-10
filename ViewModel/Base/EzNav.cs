using System;
using System.Collections.Generic;
using System.Text;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel.Base
{
    public class EzNav
    {
        public static void Go<T>(PageTransition transition = PageTransition.SlideForward, Action<T> config = null) where T : EzPage => Zebble.Mvvm.ViewModel.Go<T>(vm => { vm.Setup(); config?.Invoke(vm); }, transition);
    }
}
