using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Base
{
    public abstract class EzSubPage : Zebble.Mvvm.ViewModel
    {
        private EzPage holder;

        public EzPage Holder
        {
            get => holder;
            set
            {
                holder = value;
                holder.Children.Add(this);
            }
        }

        public EzSubPage()
        {
            Setup().RunInParallel();
        }

        public abstract Task Setup();
    }
}
