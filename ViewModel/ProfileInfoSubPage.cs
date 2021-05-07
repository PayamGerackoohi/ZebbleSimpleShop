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
    class ProfileInfoSubPage : SubPage
    {
        public Bindable<User> User { get; set; } = new User();
        public BindableCollection<string> Countries { get; set; } = new() { "Germany", "US", "UK", "Iran", "Other" };
        public BindableCollection<Gender> Genders { get; set; } = new() { (Gender[])Enum.GetValues(typeof(Gender)) };
        public int MaxBirthYear { get; set; } = DateTime.Now.Year - Constants.MAX_AGE;

        override public async Task OnUIReady()
        {
            User.Value = await Api.ShopApi.GetUser();
        }

        public async Task OnSave(string gender, string country)
        {
            User.Value.Gender = gender.ToGender();
            User.Value.Address.Country = country;
            if (User.Value.IsValid())
            {
                await Api.ShopApi.SaveUser(User);
                "User data saved!".Toast();
            }
            else
                "Invalid data!".Toast();
        }
    }
}