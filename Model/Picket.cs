using System.Collections.ObjectModel;

namespace AgeenkovApp.Model {
    public class Picket : PropChange {
        private int id;
        public int Id {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        private Profile profile;
        public Profile Profile {
            get => profile;
            set { profile = value; OnPropertyChanged(nameof(Profile)); }
        }

        private double x;
        public double X {
            get => x;
            set { x = value; OnPropertyChanged(nameof(X)); }
        }

        private double y;
        public double Y {
            get => y;
            set { y = value; OnPropertyChanged(nameof(Y)); }
        }

        private ObservableCollection<Measuring> measurings;
        public ObservableCollection<Measuring> Measurings {
            get => measurings;
            set { measurings = value; OnPropertyChanged(nameof(Measurings)); }
        }
    }
}
