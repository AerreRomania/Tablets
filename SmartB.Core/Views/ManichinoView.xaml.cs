using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartB.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManichinoView : ContentPage
    {
        public ManichinoView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
            ReverseLandscapeButton.IsVisible = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "preventLandScape");
        }

        private void ButtonPiece_OnClicked(object sender, EventArgs e)
        {
            ButtonPiece.IsEnabled = false;
            ButtonBadPiece.IsEnabled = false;
        }

        private void ButtonBadPiece_OnClicked(object sender, EventArgs e)
        {
            ButtonPiece.IsEnabled = false;
            ButtonBadPiece.IsEnabled = false;
        }

        private void ReverseLandscapeButton_OnClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "allowReverseLandScapePortrait");
            ReverseLandscapeButton.IsVisible = false;
        }
    }
}