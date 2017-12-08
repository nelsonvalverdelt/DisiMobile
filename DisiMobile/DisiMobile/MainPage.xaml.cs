using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DisiMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DNIEntry.TextChanged += DNIEntry_TextChanged;
        }

        private void DNIEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string dni = DNIEntry.Text;
            if(dni.Length > 8)
            {
                dni = dni.Remove(dni.Length - 1);
                DNIEntry.Text = dni;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var dni = DNIEntry.Text;
            await Navigation.PushAsync(new SignaturePage(dni));
        }
    }
}
