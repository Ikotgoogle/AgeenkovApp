using AgeenkovApp.Model;
using AgeenkovApp.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace AgeenkovApp.View {
    public partial class NewMeasuring : Window {
        public NewMeasuring(Measuring measuring, ObservableCollection<Operator> oper, Picket picket) {
            InitializeComponent();
            DataContext = new AddMeasuringVM(measuring, oper, picket);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
