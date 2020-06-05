using System;
using System.Reflection;

namespace SmartB.Core.DTOs
{
    public class Hours
    {


     public object this[string propertyName] 
     {
        get{
           // probably faster without reflection:
           // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
           // instead of the following
           Type myType = typeof(Hours);                   
           PropertyInfo myPropInfo = myType.GetProperty(propertyName);
           return myPropInfo.GetValue(this, null);
        }
        set{
           Type myType = typeof(Hours);                   
           PropertyInfo myPropInfo = myType.GetProperty(propertyName);
           myPropInfo.SetValue(this, value, null);
        }

     }
        public int H10 { get; set; }

        public int H11 { get; set; }

        public int H12 { get; set; }

        public int H13 { get; set; }

        public int H14 { get; set; }

        public int H15 { get; set; }

        public int H16 { get; set; }

        public int H17 { get; set; }

        public int H18 { get; set; }

        public int H19 { get; set; }

        public int H20 { get; set; }

        public int H21 { get; set; }

        public int H22 { get; set; }

        public int H23 { get; set; }

        public int H6 { get; set; }

        public int H7 { get; set; }

        public int H8 { get; set; }

        public int H9 { get; set; }
    }
}