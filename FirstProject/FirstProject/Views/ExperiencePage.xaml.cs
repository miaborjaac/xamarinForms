using FirstProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstProject.Views
{
    public partial class ExperiencePage : ContentPage
    {
        public ExperiencePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ReadExperience();
        }

        private void ReadExperience()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Experience>();
                List<Experience> experiences = conn.Table<Experience>().ToList();
                experiencesListView.ItemsSource = experiences;
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}