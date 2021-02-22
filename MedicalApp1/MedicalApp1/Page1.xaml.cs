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
            PersonApi api = new PersonApi();
            Enum.TryParse(gender.Text, out Gender gender_in);
            Enum.TryParse(role.Text, out PersonRole role_in);
            var person = api.PersonPostPerson(new Person()
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