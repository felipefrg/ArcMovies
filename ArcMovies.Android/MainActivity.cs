﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ArcMovies.Droid
{
    [Activity(Label = "ArcMovies", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();

#if GORILLA
            {
                LoadApplication(
                                UXDivers
                                .Gorilla
                                .Droid
                                .Player
                                .CreateApplication(
                                                    this
                                                    ,new UXDivers.Gorilla.Config("ArcMovies")
                                                    .RegisterAssemblyFromType<FFImageLoading.Forms.CachedImage>()
                                                    .RegisterAssemblyFromType<ArcMovies.ExtendedControl.StackLayoutExtended>()
                                                    .RegisterAssemblyFromType<CarouselView.FormsPlugin.Abstractions.CarouselViewControl>()
                                                    )

                                );
            }
#else
            {
                LoadApplication(new App());
            }

            Window window = this.Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.SetStatusBarColor(Android.Graphics.Color.Rgb(112, 78, 38));
#endif

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}