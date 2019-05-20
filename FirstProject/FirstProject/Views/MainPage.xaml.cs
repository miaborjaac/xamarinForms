using FirstProject.Models;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirstProject
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        IGeolocator locator = CrossGeolocator.Current;

        public MainPage()
        {
            InitializeComponent();

            locator.PositionChanged += Locator_PositionChanged;
        }

        void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            var position = e.Position;
        }

        private async void GetLocationPermission()
        {
            // added using Plugin.Permissions;
            // added using Plugin.Permissions.Abstractions;
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            if (status != PermissionStatus.Granted)
            {
                // Not granted, request permission
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                {
                    // This is not the actual permission request
                    await DisplayAlert("Need your permission", "We need to access your location", "Ok");
                }

                // This is the actual permission request
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                if (results.ContainsKey(Permission.LocationWhenInUse))
                    status = results[Permission.LocationWhenInUse];
            }

            // Already granted (maybe), go on
            if (status == PermissionStatus.Granted)
            {
                // Granted! Get the location
                GetLocation();
                
            }
            else
            {
                await DisplayAlert("Access to location denied", "We don't have access to your location", "Ok");
            }
        }

        private async void GetLocation()
        {
            var position = await locator.GetPositionAsync();
            await locator.StartListeningAsync(TimeSpan.FromMinutes(30), 500);
        }

        private void EnabledOrDisabledBtn()
        {
            saveBtn.IsEnabled = false;
            if (!string.IsNullOrWhiteSpace(titleExperience.Text) && !string.IsNullOrWhiteSpace(contentExperience.Text))
            {
                saveBtn.IsEnabled = true;
            }
        }

        private void TitleExperience_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnabledOrDisabledBtn();
        }

        private void ContentExperience_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnabledOrDisabledBtn();
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            Experience newExperience = new Experience()
            {
                Title = titleExperience.Text,
                Content = contentExperience.Text,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            int countItems = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Experience>();
                countItems = conn.Insert(newExperience);
            }

            if(countItems > 0)
            {
                titleExperience.Text = string.Empty;
                contentExperience.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Error creando la experiencia, intente de nuevo", "Aceptar");
            }
        }

        void ContentEntry_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetLocationPermission();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            locator.StopListeningAsync();
        }
    }
}
