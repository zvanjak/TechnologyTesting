using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestTreeViewWithControls
{
    /// <summary>
    /// Interaction logic for TreeViewList.xaml
    /// </summary>
    public partial class TreeViewList : UserControl
    {
        public TreeViewList()
        {
            InitializeComponent();
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var mainWindowViewModel = (MainWindowViewModel)DataContext;
            mainWindowViewModel.SelectedItem = (PersonViewModel)e.NewValue;
        }
    }
}
