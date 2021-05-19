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
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class SignUpPage : EzPage
    {
        public Bindable<User> User { get; private set; } = new User();
        public BindableCollection<string> Countries { get; private set; } = new() { "Germany", "US", "UK", "Iran", "Other" };
        public BindableCollection<Gender> Genders { get; private set; } = new() { (Gender[])Enum.GetValues(typeof(Gender)) };
        public int MaxBirthYear { get; set; } = DateTime.Now.Year - Constants.MAX_AGE;

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public override async Task OnRefresh()
        {
            await base.OnRefresh();
        }

        public async Task OnSave(string gender, string country)
        {
            User.Value.Gender = gender.ToGender();
            User.Value.Address.Country = country;
            if (User.Value.IsValid() && User.Value.Credential.IsValid())
            {
                await Api.ShopApi.Save(User.Value);
                "Sign up was successful!".Toast();
                await OnBack();
            }
            else
                "Invalid data!".Toast();
        }
    }
}
