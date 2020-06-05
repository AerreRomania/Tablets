using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartB.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmartBNavigationPage : NavigationPage
    {
        public SmartBNavigationPage()
        {
            InitializeComponent();
        }

        public SmartBNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}