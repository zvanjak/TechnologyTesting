using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TestTreeViewWithControls
{
    public class PersonViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public class StudentViewModel : PersonViewModel
    {
        private double _mark;
        public double Mark
        {
            get { return _mark; }
            set
            {
                _mark = value;
                OnPropertyChanged();
            }
        }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<PersonViewModel> _people;
        private PersonViewModel _selectedItem;

        public MainWindowViewModel()
        {
            People = new ObservableCollection<PersonViewModel>()
                         {
                             new PersonViewModel {Name = "Zvonimir"},
                             new StudentViewModel {Name = "Ante", Mark = 2d}
                         };
        }

        public ObservableCollection<PersonViewModel> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        public PersonViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
    }
}