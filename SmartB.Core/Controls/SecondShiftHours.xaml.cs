using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartB.Core.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SecondShiftHours : ContentView
	{
		public SecondShiftHours ()
		{
			InitializeComponent ();
		}

        private static void H15PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H15Label.Text = newValue.ToString();

        }
        private static void H16PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H16Label.Text = newValue.ToString();
        }
        private static void H17PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H17Label.Text = newValue.ToString();
        }
        private static void H18PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H18Label.Text = newValue.ToString();
        }
        private static void H19PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H19Label.Text = newValue.ToString();
        }
        private static void H20PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H20Label.Text = newValue.ToString();
        }
        private static void H21PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H21Label.Text = newValue.ToString();
        }
        private static void H22PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H22Label.Text = newValue.ToString();
        }

        private static void H23PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours) bindable;
            control.H23Label.Text = newValue.ToString();
        }

        private static void H15EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H15LabelEfficiency.Text = newValue.ToString();
        }
        private static void H16EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H16LabelEfficiency.Text = newValue.ToString();
        }
        private static void H17EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H17LabelEfficiency.Text = newValue.ToString();
        }
        private static void H18EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H18LabelEfficiency.Text = newValue.ToString();
        }
        private static void H19EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H19LabelEfficiency.Text = newValue.ToString();
        }
        private static void H20EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H20LabelEfficiency.Text = newValue.ToString();
        }
        private static void H21EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H21LabelEfficiency.Text = newValue.ToString();
        }
        private static void H22EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H22LabelEfficiency.Text = newValue.ToString();
        }
        private static void H23EfficiencyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SecondShiftHours)bindable;
            control.H23LabelEfficiency.Text = newValue.ToString();
        }



        public static readonly BindableProperty H15Property = BindableProperty.Create(
            propertyName: "H15",
            returnType: typeof(int),
            declaringType: typeof(SecondShiftHours),
            defaultValue: 0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: H15PropertyChanged);

        public static readonly BindableProperty H16Property = BindableProperty.Create(
            propertyName: "H16", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H16PropertyChanged);

        public static readonly BindableProperty H17Property = BindableProperty.Create(
            propertyName: "H17", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H17PropertyChanged);

        public static readonly BindableProperty H18Property = BindableProperty.Create(
            propertyName: "H18", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H18PropertyChanged);

        public static readonly BindableProperty H19Property = BindableProperty.Create(
            propertyName: "H19", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H19PropertyChanged);

        public static readonly BindableProperty H20Property = BindableProperty.Create(
            propertyName: "H20", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H20PropertyChanged);

        public static readonly BindableProperty H21Property = BindableProperty.Create(
            propertyName: "H21", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H21PropertyChanged);

        public static readonly BindableProperty H22Property = BindableProperty.Create(
            propertyName: "H22", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H22PropertyChanged);

        public static readonly BindableProperty H23Property = BindableProperty.Create(
            propertyName: "H23", returnType: typeof(int), declaringType: typeof(SecondShiftHours),
            defaultValue: 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H23PropertyChanged);


        public static readonly BindableProperty H15EfficiencyProperty = BindableProperty.Create(
            propertyName: "H15Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H15EfficiencyPropertyChanged);

        public static readonly BindableProperty H16EfficiencyProperty = BindableProperty.Create(
            propertyName: "H16Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H16EfficiencyPropertyChanged);

        public static readonly BindableProperty H17EfficiencyProperty = BindableProperty.Create(
            propertyName: "H17Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H17EfficiencyPropertyChanged);

        public static readonly BindableProperty H18EfficiencyProperty = BindableProperty.Create(
            propertyName: "H18Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H18EfficiencyPropertyChanged);

        public static readonly BindableProperty H19EfficiencyProperty = BindableProperty.Create(
            propertyName: "H19Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H19EfficiencyPropertyChanged);

        public static readonly BindableProperty H20EfficiencyProperty = BindableProperty.Create(
            propertyName: "H20Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H20EfficiencyPropertyChanged);

        public static readonly BindableProperty H21EfficiencyProperty = BindableProperty.Create(
            propertyName: "H21Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H21EfficiencyPropertyChanged);

        public static readonly BindableProperty H22EfficiencyProperty = BindableProperty.Create(
            propertyName: "H22Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H22EfficiencyPropertyChanged);

        public static readonly BindableProperty H23EfficiencyProperty = BindableProperty.Create(
            propertyName: "H23Efficiency", returnType: typeof(double), declaringType: typeof(SecondShiftHours),
            defaultValue: 0.0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: H23EfficiencyPropertyChanged);


        public int H15
        {
            get => (int)GetValue(H15Property);
            set => SetValue(H15Property, value);
        }

        public int H16
        {
            get => (int)GetValue(H16Property);
            set => SetValue(H16Property, value);
        }

        public int H17
        {
            get => (int)GetValue(H17Property);
            set => SetValue(H17Property, value);
        }

        public int H18
        {
            get => (int)GetValue(H18Property);
            set => SetValue(H18Property, value);
        }

        public int H19
        {
            get => (int)GetValue(H19Property);
            set => SetValue(H19Property, value);
        }

        public int H20
        {
            get => (int)GetValue(H20Property);
            set => SetValue(H20Property, value);
        }

        public int H21
        {
            get => (int)GetValue(H21Property);
            set => SetValue(H21Property, value);
        }

        public int H22
        {
            get => (int)GetValue(H22Property);
            set => SetValue(H22Property, value);
        }

        public int H23
        {
            get => (int)GetValue(H23Property);
            set => SetValue(H23Property, value);
        }

        public double H15Efficiency
        {
            get => (double)GetValue(H15EfficiencyProperty);
            set => SetValue(H15EfficiencyProperty, value);
        }
        public double H16Efficiency
        {
            get => (double)GetValue(H16EfficiencyProperty);
            set => SetValue(H16EfficiencyProperty, value);
        }
        public double H17Efficiency
        {
            get => (double)GetValue(H17EfficiencyProperty);
            set => SetValue(H17EfficiencyProperty, value);
        }
        public double H18Efficiency
        {
            get => (double)GetValue(H18EfficiencyProperty);
            set => SetValue(H18EfficiencyProperty, value);
        }
        public double H19Efficiency
        {
            get => (double)GetValue(H19EfficiencyProperty);
            set => SetValue(H19EfficiencyProperty, value);
        }
        public double H20Efficiency
        {
            get => (double)GetValue(H20EfficiencyProperty);
            set => SetValue(H20EfficiencyProperty, value);
        }
        public double H21Efficiency
        {
            get => (double)GetValue(H21EfficiencyProperty);
            set => SetValue(H21EfficiencyProperty, value);
        }
        public double H22Efficiency
        {
            get => (double)GetValue(H22EfficiencyProperty);
            set => SetValue(H22EfficiencyProperty, value);
        }
        public double H23Efficiency
        {
            get => (double)GetValue(H23EfficiencyProperty);
            set => SetValue(H23EfficiencyProperty, value);
        }
    }
}