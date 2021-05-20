using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Base
{
    public abstract class EzSubPage : Zebble.Mvvm.ViewModel
    {
        // Error CS0191: A readonly field cannot be assigned to(except in a constructor or init-only setter of the type in which the field is defined or a variable initializer)
        [SuppressMessage("Style", "GCop406:Mark {0} field as read-only.", Justification = "<Pending>")]
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

        protected EzSubPage()
        {
            Setup().RunInParallel();
        }

        public abstract Task Setup();

        public void Toast(string message) => holder.Toast(message);

        public new void Dialog(string message) => holder.Dialog(message);
    }
}
