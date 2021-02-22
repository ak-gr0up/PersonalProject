using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MedicalCommon;
using IO.Swagger.Api;

namespace MedicalApp1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        public async void OnDoneClicked(object sender, EventArgs e)
        {
            ParticipantApi api = new ParticipantApi();
            Enum.TryParse(gender.Text, out Gender gender_in);
            Enum.TryParse(role.Text, out ParticipantRole role_in);
            var Participant = api.ParticipantPostParticipant(new Participant()
            {
                Id = Guid.NewGuid(),
                Name = name.Text,
                Surname = surname.Text,
                Gender = gender_in,
                Login = login.Text,
                BirthDate = DateTime.Now.AddYears(-14)
            });
            await Navigation.PopAsync();
        }
    }
}