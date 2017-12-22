using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
using Android.Gms.Common;
using Android.Content;
using Android.Runtime;
using Android.Gms.Plus.Model.People;
using XFLoginGoogle.Droid.Specific;
using Acr.UserDialogs;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using XFLoginGoogle.Droid.Services;
using Xamarin.Forms;
using XFLoginGoogle.Services;

namespace XFLoginGoogle.Droid
{
    [Activity(Label = "XFLoginGoogle", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            //DependencyService.Register<IGoogleService, AndroidGoogleService>(); // nếu ko dùng Prism
            LoadApplication(new App(new PlatformInitializer()));
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                AndroidGoogleService.Instance.OnAuthCompleted(result, resultCode);
                
            }
        }
    }
}

