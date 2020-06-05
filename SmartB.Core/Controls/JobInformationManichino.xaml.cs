using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartB.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobInformationManichino : ContentView
    {
        public JobInformationManichino()
        {
            InitializeComponent();
        }


        private static void EmployeePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (JobInformationManichino)bindable;
            control.EmployeeLabel.Text = newValue.ToString();

        }
        private static void SectorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (JobInformationManichino)bindable;
            control.SectorLabel.Text = newValue.ToString();
        }
        private static void CommessaPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (JobInformationManichino)bindable;
            control.CommessaLabel.Text = newValue.ToString();
        }
        private static void PhasePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (JobInformationManichino)bindable;
            control.PhaseLabel.Text = newValue.ToString();
        }

        public static void MachinePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (JobInformationManichino)bindable;
            control.MachineLabel.Text = newValue.ToString();
        }

        public static readonly BindableProperty EmployeeProperty = BindableProperty.Create(
            propertyName: "Employee", returnType: typeof(string), declaringType: typeof(JobInformationManichino),
            defaultValue: "", defaultBindingMode: BindingMode.TwoWay, propertyChanged: EmployeePropertyChanged);

        public static readonly BindableProperty SectorProperty = BindableProperty.Create(
            propertyName: "Sector", returnType: typeof(string), declaringType: typeof(JobInformationManichino),
            defaultValue: "", defaultBindingMode: BindingMode.TwoWay, propertyChanged: SectorPropertyChanged);

        public static readonly BindableProperty CommessaProperty = BindableProperty.Create(
            propertyName: "Commessa", returnType: typeof(string), declaringType: typeof(JobInformationManichino),
            defaultValue: "", defaultBindingMode: BindingMode.TwoWay, propertyChanged: CommessaPropertyChanged);

        public static readonly BindableProperty PhaseProperty = BindableProperty.Create(
            propertyName: "Phase", returnType: typeof(string), declaringType: typeof(JobInformationManichino),
            defaultValue: "", defaultBindingMode: BindingMode.TwoWay, propertyChanged: PhasePropertyChanged);

        public static readonly BindableProperty MachineProperty = BindableProperty.Create(
            propertyName: "Machine", returnType: typeof(string), declaringType: typeof(JobInformationManichino),
            defaultValue: "", defaultBindingMode: BindingMode.TwoWay, propertyChanged: MachinePropertyChanged);

        public string Employee
        {
            get => (string)GetValue(EmployeeProperty);
            set => SetValue(EmployeeProperty, value);
        }
        public string Sector
        {
            get => (string)GetValue(SectorProperty);
            set => SetValue(SectorProperty, value);
        }

        public string Commessa
        {
            get => (string)GetValue(CommessaProperty);
            set => SetValue(CommessaProperty, value);
        }

        public string Phase
        {
            get => (string)GetValue(PhaseProperty);
            set => SetValue(PhaseProperty, value);
        }
        public string Machine
        {
            get => (string)GetValue(MachineProperty);
            set => SetValue(MachineProperty, value);
        }
    }
}