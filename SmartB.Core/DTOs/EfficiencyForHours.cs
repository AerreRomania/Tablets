using System;
using System.Reflection;
namespace SmartB.Core.DTOs
{
    public class EfficiencyForHours
    {
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(EfficiencyForHours);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(EfficiencyForHours);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }

        }
        public double H6Efficiency { get; set; }
        public double H7Efficiency { get; set; }
        public double H8Efficiency { get; set; }
        public double H9Efficiency { get; set; }
        public double H10Efficiency { get; set; }
        public double H11Efficiency { get; set; }
        public double H12Efficiency { get; set; }
        public double H13Efficiency { get; set; }
        public double H14Efficiency { get; set; }
        public double H15Efficiency { get; set; }
        public double H16Efficiency { get; set; }
        public double H17Efficiency { get; set; }
        public double H18Efficiency { get; set; }
        public double H19Efficiency { get; set; }
        public double H20Efficiency { get; set; }
        public double H21Efficiency { get; set; }
        public double H22Efficiency { get; set; }
        public double H23Efficiency { get; set; }
    }
}