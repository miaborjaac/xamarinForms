using FirstProject.Models;
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
        public MainPage()
        {
            InitializeComponent();
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
    }
}
