using System.Collections.Generic;
using Xamarin.Forms;
using MedicalClient;
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
            ParticipantClient api = new ParticipantClient();
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
            ParticipantClient api = new ParticipantClient();
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