using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormSend : ContentPage
    {
        public FormSend()
        {
            InitializeComponent();
        }

        public async void ReturnToFormPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}