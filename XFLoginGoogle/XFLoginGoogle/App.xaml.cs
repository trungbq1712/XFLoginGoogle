
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DryIoc;
using Prism.DryIoc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFLoginGoogle.Views;
using XFLoginGoogle.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFLoginGoogle
{
    public partial class App 
    {
        public App()
        {

        }
        public App(IPlatformInitializer platform): base(platform)
        {

        }

        protected override void OnInitialized()
        {

            InitializeComponent();
            NavigationService.NavigateAsync("/Navigation/LoginPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>("LoginPage");
        }

       
    }
}
