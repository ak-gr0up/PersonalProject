using System.Collections.Generic;
using Xamarin.Forms;
using MedicalCommon;
using IO.Swagger.Api;
using System;

namespace MedicalApp1
{
    public partial class MainPage : ContentPage
    {
        public IList<Person> Persons { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Persons = new List <Person>();
            PersonApi api = new PersonApi();
            Persons = api.PersonGetPersonAll();

            BindingContext = this;
        }

        public async void OnCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1
            {
                BindingContext = new Person()
            });
        }

        public async void OnRefreshClicked(object sender, EventArgs e)
        {
            InitializeComponent();

            Persons = new List<Person>();
            PersonApi api = new PersonApi();
            Persons = api.PersonGetPersonAll();
            Persons.Add(new Person
            {
                Name = "lorem",
                Surname = "ispum"
            });
            BindingContext = this;
        }
    }
}