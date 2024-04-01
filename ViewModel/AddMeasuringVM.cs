using AgeenkovApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeenkovApp.ViewModel {
    public class AddMeasuringVM : PropChange {

        public Measuring Measuring { get; set; }
        public ObservableCollection<Operator> Operators { get; set; }
        public Picket Picket { get; set; }

        public RelayCommand ClickOKCmd { get; set; }

        public AddMeasuringVM(Measuring measuring, ObservableCollection<Operator> opers, Picket picket) { 
            Measuring = measuring;
            Operators = opers;
            Picket = picket;
            ClickOKCmd = new(ClickOK);
        }

        private Operator selOper;
        public Operator SelOper {
            get => selOper;
            set { selOper = value; OnPropertyChanged(nameof(SelOper)); }
        }

        private double value;
        public double Value {
            get => value;
            set { this.value = value; OnPropertyChanged(nameof(Value)); }
        }

        void ClickOK (object obj) {
            Measuring.Operator = SelOper;
            Measuring.Picket = Picket;
            Measuring.MeasuringValue = Value;
        }
    }
}
