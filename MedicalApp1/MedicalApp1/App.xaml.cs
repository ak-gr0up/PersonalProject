using Android.Content;
using Android.Content.Res;
using MedicalClient;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

        }

        protected override void OnStart()
        {
            try
            {
                var token = Application.Current.Properties["token"];
                ParticipantClient api = new ParticipantClient(token.ToString());
                api.GetParticipantMeAsync().Wait();
                MainPage = new NavigationPage(new Page1());
            }
            catch
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
