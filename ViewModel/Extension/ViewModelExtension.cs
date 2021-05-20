using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;

namespace ViewModel
{
    public static class ViewModelExtension
    {
        public static void Toast(this string message, EzPage page) => page.Toast(message);

        public static void Dialog(this string message, EzPage page) => page.Dialog(message);

        public static void Toast(this string message, EzSubPage page) => page.Toast(message);

        public static void Dialog(this string message, EzSubPage page) => page.Dialog(message);

#if NETCOREAPP
        // ~VM Project Zone
        public static void RunInParallel(this Task task)
        {
            task.Wait();
        }
#endif
    }
}
