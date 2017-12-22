using Android.Gms.Common.Apis;
using System;
using System.Threading.Tasks;
using XFLoginGoogle.Models;
using XFLoginGoogle.Services;
using Android.OS;
using Android.Gms.Common;
using Android.Runtime;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Plus;
using Xamarin.Forms;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.App;

namespace XFLoginGoogle.Droid.Services
{
    public class AndroidGoogleService :  Java.Lang.Object, IGoogleService, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public static GoogleApiClient _mGoogleApiClient;

        public Action<GoogleUser, string> _onLoginComplete;
        public static AndroidGoogleService Instance { get; private set; }


        public AndroidGoogleService()
        {
            Instance = this;
            //_mGoogleApiClient = new GoogleApiClient.Builder(((MainActivity)Forms.Context).ApplicationContext)
            //                    .AddConnectionCallbacks(this)
            //                    .AddOnConnectionFailedListener(this)
            //                    .AddApi(PlusClass.API)
            //                    .AddScope(PlusClass.ScopePlusProfile)
            //                    .AddScope(PlusClass.ScopePlusLogin).Build();

            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                             .RequestEmail()
                                                             .Build();

            _mGoogleApiClient = new GoogleApiClient.Builder(((MainActivity)Forms.Context).ApplicationContext)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .AddScope(new Scope(Scopes.Profile))
                .AddScope(new Scope(Scopes.Email))
                .Build();
        }

        public void OnAuthCompleted(GoogleSignInResult result, Result resultCode)
        {
            if(resultCode == Result.Ok)
            {
                if (result.IsSuccess)
                {
                    GoogleSignInAccount accountt = result.SignInAccount;
                    _onLoginComplete?.Invoke(new GoogleUser()
                    {
                        NameDisplay = accountt.DisplayName,
                        Email = accountt.Email,
                        Picture = new Uri((accountt.PhotoUrl != null ? $"{accountt.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"))
                    }, string.Empty);
                }
                else
                {

                    _onLoginComplete?.Invoke(null, "An error occured!");
                }
            }
            
        }
        public void LogIn(Action<GoogleUser, string> onLoginComplete)
        {
            if (_mGoogleApiClient.IsConnecting == false)// Handling when users click many times on login button, means that cancel request login when processing logging in
            {
                _onLoginComplete = onLoginComplete;
                Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_mGoogleApiClient);
                ((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
                _mGoogleApiClient.Connect();
            }
        }

        public void LogOut()
        {
            Auth.GoogleSignInApi.SignOut(_mGoogleApiClient);
            _mGoogleApiClient.Disconnect();
        }

        public void OnConnected(Bundle connectionHint)
        {
            
        }

        public void OnConnectionSuspended(int cause)
        {
            _onLoginComplete?.Invoke(null, "Canceled!");
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            _onLoginComplete?.Invoke(null, result.ErrorMessage);
        }
    }
}