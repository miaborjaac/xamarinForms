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
        //private void button_clicked(object sender, eventargs e)
        //{
        //    if (!string.isnullorwhitespace(nameentry.text))
        //    {
        //        greetinglabel.text = $"hello {nameentry.text}, welcome to 10 days of xamarin.";
        //    }
        //    else
        //    {
        //        displayalert("error", "your name can't be empty", "oh right");
        //        greetinglabel.text = $"";
        //    }

        void Save_Clicked(object sender, EventArgs e)
        {
            titleExperience.Text = $"";
            contentExperience.Text = string.Empty;
        }

        private void EnabledOrDisabledBtn()
        {
            saveBtn.IsEnabled = false;
            if(!string.IsNullOrWhiteSpace(titleExperience.Text) && !string.IsNullOrWhiteSpace(contentExperience.Text))
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
    }
}
