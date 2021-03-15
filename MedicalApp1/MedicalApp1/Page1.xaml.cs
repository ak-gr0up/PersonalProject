using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MedicalClient;

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
            DataPointClient api = new DataPointClient(Application.Current.Properties["token"] as String);
            var datapoint = new DataPoint()
            {
                HeartBeat = int.Parse(hearthbeat.Text),
                Temperature = int.Parse(temperature.Text),
                DistalPressure = int.Parse(distal_pressure.Text),
                SistalPressure = int.Parse(sistal_pressure.Text),
                SelfFeeling = int.Parse(self_feeling.Text),
                Headache = headache.IsChecked,
                Dizziness = Dizziness.IsChecked,
                Cough = cough.IsChecked,
                Rheum = rheum.IsChecked,
                Weakness = weakness.IsChecked,
                Nausea = nausea.IsChecked
            };

            var type = datapoint.GetType();

            var ser = Newtonsoft.Json.JsonConvert.SerializeObject(datapoint);

            await api.PostDataPointAsync(datapoint);
            await Navigation.PushAsync(new FormSend{});
        }
    }
}