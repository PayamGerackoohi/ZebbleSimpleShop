using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Base
{
    abstract class SubPage : Zebble.Mvvm.ViewModel
    {
        public abstract Task OnUIReady();
    }
}
