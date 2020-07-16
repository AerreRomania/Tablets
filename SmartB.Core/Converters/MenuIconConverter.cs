using System;
using System.Globalization;
using SmartB.Core.Enumerations;
using Xamarin.Forms;
namespace SmartB.Core.Converters
{
    public class MenuIconConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (MenuItemType) value;
            switch (type)
            {
                case MenuItemType.Home:
                    return "ic_home.png";
                case MenuItemType.Type1:
                    return "ic_contact.png";
                case MenuItemType.Type2:
                    return "ic_pies.png";
                case MenuItemType.Type3:
                    return "ic_cart.png";
                case MenuItemType.Logout:
                    return "ic_logout.png";
                default:
                    return string.Empty;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Not needed here
            throw new NotImplementedException();
        }
    }
}
