using System.Windows;
using System.Windows.Controls;

namespace TestTreeViewWithControls
{
    public class PersonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PersonDataTemplate { get; set; }

        public DataTemplate StudentDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is StudentViewModel)
            {
                return StudentDataTemplate;
            }

            if (item is PersonViewModel)
            {
                return PersonDataTemplate;
            }
            
            return PersonDataTemplate;
        }
    }
}