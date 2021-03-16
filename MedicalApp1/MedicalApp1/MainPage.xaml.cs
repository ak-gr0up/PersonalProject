using Xamarin.Forms;
using MedicalClient;
using System;

namespace MedicalApp1
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
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