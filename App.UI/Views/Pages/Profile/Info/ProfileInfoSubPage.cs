using Domain;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Plugin;

namespace UI.Pages
{
    partial class ProfileInfoSubPage
    {
        public EzPage ModelHolder { get; set; }

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupShadows();
        }

        private void SetupShadows()
        {
            ProfileInfoCard.AddShadow();
            SaveButton.AddShadow();
        }
    }
}
