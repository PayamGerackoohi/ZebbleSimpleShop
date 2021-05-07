using Zebble;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ViewModel;
using Zebble.Mvvm;
using System.Threading.Tasks;

namespace App
{
    abstract class DevContext : TestScript
    {
        /// <summary>
        /// Run quick steps to get you to the context you're developing.
        /// </summary>
        public static async Task Reach()
        {
            //On<WelcomePage>().TapLogin();
            //On<LoginPage>().TapLogin();
            //On<ShoesPage>().Items[0].Tap();
        }
    }
}