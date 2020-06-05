using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartB.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstShiftHours : ContentView
    {
        public FirstShiftHours()
        {
            InitializeComponent();
        }

        private static void H6PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours) bindable;
            control.H6Label.Text = newValue.ToString();
           
        }
        private static void H7PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H7Label.Text = newValue.ToString();
        }
        private static void H8PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H8Label.Text = newValue.ToString();
        }
        private static void H9PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H9Label.Text = newValue.ToString();
        }
        private static void H10PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H10Label.Text = newValue.ToString();
        }
        private static void H11PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H11Label.Text = newValue.ToString();
        }
        private static void H12PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H12Label.Text = newValue.ToString();
        }
        private static void H13PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H13Label.Text = newValue.ToString();
     
        }
        private static void H14PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H14Label.Text = newValue.ToString();
        }
        private static void H15PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H15Label.Text = newValue.ToString();
        }

        private static void H6EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H6LabelEfficiency.Text = newValue.ToString();
        }
        
        private static void H7EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H7LabelEfficiency.Text = newValue.ToString();
        }
        private static void H8EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H8LabelEfficiency.Text = newValue.ToString();
        }
        private static void H9EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H9LabelEfficiency.Text = newValue.ToString();
        }
        private static void H10EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H10LabelEfficiency.Text = newValue.ToString();
        }
        private static void H11EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H11LabelEfficiency.Text = newValue.ToString();
        }
        private static void H12EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H12LabelEfficiency.Text = newValue.ToString();
        }
        private static void H13EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H13LabelEfficiency.Text = newValue.ToString();
        }
        private static void H14EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H14LabelEfficiency.Text = newValue.ToString();
        }
        private static void H15EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FirstShiftHours)bindable;
            control.H15LabelEfficiency.Text = newValue.ToString();
        }


        public static readonly  BindableProperty H6Property = BindableProperty.Create(
            propertyName:"H6",
            returnType: typeof(int), 
            declaringType: typeof(FirstShiftHours), 
            defaultValue:0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: H6PropertyChanged);

        public static readonly BindableProperty H7Property = BindableProperty.Create(
            propertyName: "H7", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H7PropertyChanged);

        public static readonly BindableProperty H8Property = BindableProperty.Create(
            propertyName: "H8", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H8PropertyChanged);

        public static readonly BindableProperty H9Property = BindableProperty.Create(
            propertyName: "H9", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H9PropertyChanged);

        public static readonly BindableProperty H10Property = BindableProperty.Create(
            propertyName: "H10", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H10PropertyChanged);

        public static readonly BindableProperty H11Property = BindableProperty.Create(
            propertyName: "H11", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H11PropertyChanged);

        public static readonly BindableProperty H12Property = BindableProperty.Create(
            propertyName: "H12", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H12PropertyChanged);

        public static readonly BindableProperty H13Property = BindableProperty.Create(
            propertyName: "H13", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H13PropertyChanged);

        public static readonly BindableProperty H14Property = BindableProperty.Create(
            propertyName: "H14", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H14PropertyChanged);

        public static readonly BindableProperty H15Property = BindableProperty.Create(
            propertyName: "H15", returnType: typeof(int), declaringType: typeof(FirstShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H15PropertyChanged);


        public static readonly BindableProperty H6EfficiencyProperty = BindableProperty.Create(
            propertyName: "H6Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H6EfficiencyPropertyChanged);

        public static readonly BindableProperty H7EfficiencyProperty = BindableProperty.Create(
            propertyName: "H7Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H7EfficiencyPropertyChanged);

        public static readonly BindableProperty H8EfficiencyProperty = BindableProperty.Create(
            propertyName: "H8Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H8EfficiencyPropertyChanged);

        public static readonly BindableProperty H9EfficiencyProperty = BindableProperty.Create(
            propertyName: "H9Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H9EfficiencyPropertyChanged);

        public static readonly BindableProperty H10EfficiencyProperty = BindableProperty.Create(
            propertyName: "H10Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H10EfficiencyPropertyChanged);

        public static readonly BindableProperty H11EfficiencyProperty = BindableProperty.Create(
            propertyName: "H11Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H11EfficiencyPropertyChanged);

        public static readonly BindableProperty H12EfficiencyProperty = BindableProperty.Create(
            propertyName: "H12Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H12EfficiencyPropertyChanged);

        public static readonly BindableProperty H13EfficiencyProperty = BindableProperty.Create(
            propertyName: "H13Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H13EfficiencyPropertyChanged);

        public static readonly BindableProperty H14EfficiencyProperty = BindableProperty.Create(
            propertyName: "H14Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H14EfficiencyPropertyChanged);

        public static readonly BindableProperty H15EfficiencyProperty = BindableProperty.Create(
            propertyName: "H15Efficiency", returnType: typeof(double), declaringType: typeof(FirstShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H15EfficiencyPropertyChanged);






        public int H6
        {
            get => (int) GetValue(H6Property);
            set => SetValue(H6Property, value);
        }

        public int H7
        {
            get => (int) GetValue(H7Property);
            set => SetValue(H7Property, value);
        }

        public int H8
        {
            get => (int) GetValue(H8Property);
            set => SetValue(H8Property, value);
        }

        public int H9
        {
            get => (int) GetValue(H9Property);
            set => SetValue(H9Property, value);
        }

        public int H10
        {
            get => (int) GetValue(H10Property);
            set => SetValue(H10Property, value);
        }

        public int H11
        {
            get => (int) GetValue(H11Property);
            set => SetValue(H11Property, value);
        }

        public int H12
        {
            get => (int) GetValue(H12Property);
            set => SetValue(H12Property, value);
        }

        public int H13
        {
            get => (int) GetValue(H13Property);
            set => SetValue(H13Property, value);
        }

        public int H14
        {
            get => (int) GetValue(H14Property);
            set => SetValue(H14Property, value);
        }

        public int H15
        {
            get => (int) GetValue(H15Property);
            set => SetValue(H15Property, value);
        }

        public double H6Efficiency
        {
            get => (double)GetValue(H6EfficiencyProperty);
            set => SetValue(H6EfficiencyProperty, value);
        }
        public double H7Efficiency
        {
            get => (double)GetValue(H7EfficiencyProperty);
            set => SetValue(H7EfficiencyProperty, value);
        }
        public double H8Efficiency
        {
            get => (double)GetValue(H8EfficiencyProperty);
            set => SetValue(H8EfficiencyProperty, value);
        }
        public double H9Efficiency
        {
            get => (double)GetValue(H9EfficiencyProperty);
            set => SetValue(H9EfficiencyProperty, value);
        }
        public double H10Efficiency
        {
            get => (double)GetValue(H10EfficiencyProperty);
            set => SetValue(H10EfficiencyProperty, value);
        }
        public double H11Efficiency
        {
            get => (double)GetValue(H11EfficiencyProperty);
            set => SetValue(H11EfficiencyProperty, value);
        }
        public double H12Efficiency
        {
            get => (double)GetValue(H12EfficiencyProperty);
            set => SetValue(H12EfficiencyProperty, value);
        }
        public double H13Efficiency
        {
            get => (double)GetValue(H13EfficiencyProperty);
            set => SetValue(H13EfficiencyProperty, value);
        }
        public double H14Efficiency
        {
            get => (double)GetValue(H14EfficiencyProperty);
            set => SetValue(H14EfficiencyProperty, value);
        }
        public double H15Efficiency
        {
            get => (double)GetValue(H15EfficiencyProperty);
            set => SetValue(H15EfficiencyProperty, value);
        }

     
    }
}