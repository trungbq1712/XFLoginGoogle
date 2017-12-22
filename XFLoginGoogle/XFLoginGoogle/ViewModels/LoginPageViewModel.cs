using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Commands;
using Acr.UserDialogs;
using XFLoginGoogle.Services;
using XFLoginGoogle.Models;
using Prism.Services;

namespace XFLoginGoogle.ViewModels
{
    public class LoginPageViewModel :ViewModelBase
    {
        private IGoogleService _googleService;
        private readonly IPageDialogService _dialogService;
        private bool _isLogin;
        private GoogleUser _googleUser;

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                SetProperty(ref _isLogin, value);
            }
        }

        public GoogleUser GoogleUser
        {
            get => _googleUser;
            set => SetProperty(ref _googleUser, value);
        }

        public LoginPageViewModel(INavigationService navigationService,IGoogleService googleService,IPageDialogService dialogService) : base(navigationService)
        {
            _googleService = googleService;
            _dialogService = dialogService;
            LoginCommand = new DelegateCommand(OnLogin);
            LogoutCommand = new DelegateCommand(OnLogout);
            IsLogin = false;
        }

        private void OnLogout()
        {
            IsLogin = false;
            _googleService.LogOut();
        }

        private void OnLogin()
        {
            _googleService.LogIn(OnLoginComplete);
            
        }

        private void OnLoginComplete(GoogleUser user, string mesage)
        {

            //var toastConfig = new ToastConfig("hello "+arg1.NameDisplay);
            //toastConfig.SetDuration(3000);
            //toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));
            //toastConfig.SetPosition(ToastPosition.Top);
            //UserDialogs.Instance.Toast(toastConfig);
            if (user != null)
            {
                GoogleUser = user;
                IsLogin = true;
                //_dialogService.DisplayAlertAsync("Attention", "Hello" + user.NameDisplay, "Close");
            }
            else
            {
                IsLogin = false;
                _dialogService.DisplayAlertAsync("Fail", mesage, "Close");
            }
        }
        

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
