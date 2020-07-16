using SmartB.Core.Models;
using Xamarin.Forms;
namespace SmartB.Core.DataTemplateSelector
{
    public class MachineDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate SpecificDataTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is Masini machine))
                return null;
            return machine.Active ? SpecificDataTemplate : DefaultTemplate;
        }
    }
}