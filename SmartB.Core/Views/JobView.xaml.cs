using System;
using Xamarin.Forms.Xaml;

namespace SmartB.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobView
    {
        public JobView()
        {
            InitializeComponent();
        }

        private void ButtonPiece_Clicked(object sender, EventArgs e)
        {
            ButtonPiece.IsEnabled = false;
        }
    }
}