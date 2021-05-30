using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SearchBooksApp.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly:Dependency(typeof(StatusBar))]
namespace SearchBooksApp.Droid
{
    class StatusBar : IStatusBar
    {
        WindowManagerFlags _originalFlags;

        [Obsolete]
        public void HideStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attributes = activity.Window.Attributes;
            _originalFlags = attributes.Flags;
            attributes.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attributes;
        }

        [Obsolete]
        public void ShowStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attributes = activity.Window.Attributes;
            attributes.Flags = _originalFlags;
            activity.Window.Attributes = attributes;
        }
    }
}