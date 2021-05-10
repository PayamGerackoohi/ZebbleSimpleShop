using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base.Interface;

namespace ViewModel.Base
{
    abstract class EzSubPage : Zebble.Mvvm.ViewModel
    {
        public EzSubPage()
        {
            Setup().RunInParallel();
        }

        public EzPage Holder;

        protected abstract Task Setup();
    }
}
