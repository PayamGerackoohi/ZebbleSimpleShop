using Domain.Api;
using Domain.Database;
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
        public Bindable<User> User { get; private set; } = new();

        public async Task OnSave()
        {
            if (User.Value.Credential.IsValid())
            {
                await Api.ShopApi.Save(User);
                "User's credentials are saved!".Toast();
            }
            else
                "Invalid data!".Toast();
        }

        public async Task OnLogout()
        {
            User.Value.Credential.StayLoggedIn = false;
            ShopDatabase.Instance.CredentialDao.Save(User.Value.Credential);
            Holder.EzGo<LoginPage>();
        }

        public override async Task Setup()
        {
            User.Value = await Api.ShopApi.GetUser();
        }
    }
}
