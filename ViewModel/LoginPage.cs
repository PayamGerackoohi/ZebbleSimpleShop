using Domain.Api;
using Domain.Database;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class LoginPage : EzPage
    {
        public Bindable<User> User { get; set; } = new();
        public string Username { get => User.Value.Credential.Let(c => c.StayLoggedIn ? c.Username : ""); }
        public string Password { get => User.Value.Credential.Let(c => c.StayLoggedIn ? c.Password : ""); }
        public string Hint { get => User.Value.Credential.Let(c => $"Hint: {c.Username} <~> {c.Password}"); }

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public override async Task OnRefresh()
        {
            User.Value = await Api.ShopApi.GetUser();
            await base.OnRefresh();
        }

        public async Task SignUpTapped()
        {
            EzForward<SignUpPage>();
        }

        public async Task LoginTapped(string username, string password, bool rememberMe)
        {
            User.Value.Credential.Also(user =>
            {
                if (user.IsValid() && user.IsAuthenticated(username, password))
                    Login(rememberMe);
                else
                    "Username or password are incorrect!".Toast(this);
            });
        }

        private void Login(bool rememberMe)
        {
            User.Value.Credential.StayLoggedIn = rememberMe;
            ShopDatabase.Instance.CredentialDao.Save(User.Value.Credential);
            EzGo<HomePage>();
        }
    }
}
