using Domain.Database;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel.Base
{
    public enum AlertType { Toast, Dialog }

    public abstract class EzPage : FullScreen
    {
        private bool IsUIReady;
        private readonly List<Action> PendingActions = new();
        public List<EzSubPage> Children { get; private set; } = new();
        public EzPage PreviousPage;
        public AlertType AlertType;
        public Bindable<string> AlertMessage { get; set; } = "";

        public void Toast(string message)
        {
            AlertDialog(AlertType.Toast, message);
        }

        public new void Dialog(string message)
        {
            AlertDialog(AlertType.Dialog, message);
        }

        private void AlertDialog(AlertType alertType, string message)
        {
            if (IsUIReady)
            {
                AlertType = alertType;
                AlertMessage.Value = message;
            }
            else
                PendingActions.Add(() => AlertDialog(alertType, message));
        }

        public virtual async Task OnBack()
        {
            Back();
            PreviousPage?.OnRefresh();
        }

        public virtual async Task OnRefresh()
        {
            Children.Do(child => child.Setup().RunInParallel());
        }

        public abstract Task Setup();

        public void EzBack(PageTransition transition = PageTransition.SlideBack) => Back(transition);

        public void EzForward<T>(PageTransition transition = PageTransition.SlideForward, Action<T> config = null) where T : EzPage =>
            Forward<T>(vm => { vm.PreviousPage = this; vm.Setup(); config?.Invoke(vm); }, transition);

        public void EzGo<T>(PageTransition transition = PageTransition.SlideForward, Action<T> config = null) where T : EzPage =>
            EzNav.Go(transition, config);

        public void OnUIReady()
        {
            IsUIReady = true;
            PendingActions.Do(act => act());
            PendingActions.Clear();
        }
    }
}
