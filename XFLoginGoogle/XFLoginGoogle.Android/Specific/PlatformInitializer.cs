using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DryIoc;
using Prism.DryIoc;
using XFLoginGoogle.Services;
using XFLoginGoogle.Droid.Services;
using Android.Gms.Common.Apis;

namespace XFLoginGoogle.Droid.Specific
{
    public class PlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainer container)
        {
            container.RegisterInstance<IGoogleService>(new AndroidGoogleService());
        }
    }
}