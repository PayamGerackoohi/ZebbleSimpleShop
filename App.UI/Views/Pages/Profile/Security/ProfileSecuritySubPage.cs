using Domain;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Plugin;

namespace UI.Pages
{
    partial class ProfileSecuritySubPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupShadows();
            SetupUsernameInput();
        }

        private void SetupUsernameInput()
        {
            Username.Control.Activate();
            Username.Control.Deactivate();

            Password.Control.Deactivate();
            Password.Control.Activate();
        }

        private void SetupShadows()
        {
            ProfileSecurityCard.AddShadow();
            SaveButton.AddShadow();
            LogoutButton.AddShadow();
        }
    }
}
