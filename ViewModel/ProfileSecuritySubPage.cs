using Domain.Api;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class ProfileSecuritySubPage : EzSubPage
    {
        public Bindable<User> User { get; private set; } = new ();

        public async Task OnSave()
        {
            if (User.Value.Credential.IsValid())
            {
                await Api.ShopApi.SaveUser(User);
                "User's credentials are saved!".Toast();
            }
            else
                "Invalid data!".Toast();
        }
        public async Task OnLogout()
        {
            "Not implemented yet!".Toast();
        }

        //public override async Task OnRefresh()
        //{
        //}

        protected override async Task Setup()
        {
            User.Value = await Api.ShopApi.GetUser();
        }
    }
}
