using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DisiMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignaturePage : ContentPage
    {
        private string _dni = null;
        public SignaturePage(string dni)
        {
            InitializeComponent();
            DNILabel.Text = "DNI: \n" + dni;
            _dni = dni;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var settings = new ImageConstructionSettings
            {
                Padding = 12,
                StrokeColor = Color.FromRgb(25, 25, 25),
                BackgroundColor = Color.FromRgb(225, 225, 225),
                ShouldCrop = false
            };
            var stream = await padView.GetImageStreamAsync(SignatureImageFormat.Png, settings);
            var respuesta = await BlobStorage.BlobStorageImage(_dni, stream);

            if(respuesta == true)
            {
                await DisplayAlert("Mainframes", "Firma Registrada", "Finalizar");
            }
            else
            {
                await DisplayAlert("Mainframes", "Error de Firma", "Finalizar");
            }

        }
    }
}