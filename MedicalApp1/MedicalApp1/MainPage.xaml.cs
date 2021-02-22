using System.Collections.Generic;
using Xamarin.Forms;
using MedicalCommon;
using IO.Swagger.Api;
using System;

namespace MedicalApp1
{
    public partial class MainPage : ContentPage
    {
        public IList<Participant> Participants { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Participants = new List <Participant>();
            ParticipantApi api = new ParticipantApi();
            Participants = api.ParticipantGetParticipantAll();

            BindingContext = this;
        }

        public async void OnCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1
            {
                BindingContext = new Participant()
            });
        }

        public async void OnRefreshClicked(object sender, EventArgs e)
        {
            InitializeComponent();

            Participants = new List<Participant>();
            ParticipantApi api = new ParticipantApi();
            Participants = api.ParticipantGetParticipantAll();
            Participants.Add(new Participant
            {
                Name = "lorem",
                Surname = "ispum"
            });
            BindingContext = this;
        }
    }
}