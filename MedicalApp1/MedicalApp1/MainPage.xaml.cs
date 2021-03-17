using Xamarin.Forms;
using MedicalClient;
using System;
using Android.Content;
using Android.Preferences;

namespace MedicalApp1
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

        }

        protected override async void OnAppearing()
        {
            try
            {
                var token = Application.Current.Properties["token"];
                ParticipantClient api = new ParticipantClient(token.ToString());
                await api.GetParticipantMeAsync();
                await Navigation.PushAsync(new Page1
                {
                    BindingContext = new DataPoint()
                });
            }
            catch
            {
            }
            base.OnAppearing();
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            AccountClient api = new AccountClient();
            //try
            //{
                var response = await api.TokenParticipantAsync(username.Text);
                
                Application.Current.Properties["token"] = response;

                await Navigation.PushAsync(new Page1
                {
                    BindingContext = new DataPoint()
                });
            //}
            //catch
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert", "Wrong phone number", "OK");
            //}
        }

    }
}