using AgeenkovApp.Model;
using AgeenkovApp.View;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Windows;

namespace AgeenkovApp.ViewModel {
    class PicketVM : PropChange {
        AggContext db = AggContext.LoadAll();

        public Picket Picket { get; set; }
        public Operator Operator { get; set; }
        public ObservableCollection<Measuring> Measurings { get; set; }

        public RelayCommand AddMeasuringCmd { get; set; }
        public RelayCommand DeleteMeasuringCmd { get; set; }
        public RelayCommand RefreshCmd { get; set; }

        public PicketVM(Picket picket, Operator oper) {
            Picket = picket;
            Operator = oper;

            Measurings = picket.Measurings;

            AddMeasuringCmd = new(AddMeasuring);
            DeleteMeasuringCmd = new(DeleteMeasuring);
            RefreshCmd = new(Refresh);
        }

        void AddMeasuring(object obj) {
            var meas = new Measuring() { MeasuringValue = 0 };
            if(new NewMeasuring(meas).ShowDialog() == false) return;
            //else if(meas.MeasuringValue == null) { return; } 
            else {
                meas.Operator = Operator;
                meas.Picket = Picket;
                meas.MeasuringDateTime = DateTime.Now;
                db.Measurings.Add(meas);
                db.SaveChanges();
                OnPropertyChanged(nameof(Picket));
                SetPlotModel();
            }
        }

        void DeleteMeasuring(object obj) {
            if(MessageBox.Show("Are you sure?", "Delete measuring",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                db.Measurings.Remove((Measuring)obj);
                db.SaveChanges();
                SetPlotModel();
            }
        }

        void Refresh(object obj) {
            SetPlotModel();
        }

        private PlotModel plotModel;
        public PlotModel PlotModel {
            get => plotModel;
            set { plotModel = value; OnPropertyChanged(nameof(PlotModel)); }
        }
        private void SetPlotModel() {
            var plotModel = new PlotModel() { Title = "График значений" };

            // Configure X-axis for DateTime (assuming MeasuringDateTime is DateTime type)
            var xAxis = new DateTimeAxis {
                Position = AxisPosition.Top,
                Title = "Время измерения",
                StringFormat = "MM/dd/yyyy HH:mm" // Adjust format as needed
            };
            var yAxis = new LinearAxis {
                Position = AxisPosition.Left,
                Title = "Значение измерения",
                StartPosition = Double.MaxValue,
                EndPosition = Double.MinValue
            }; // Set initial range based on data

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            // Create line series for measurements
            var lineSeries = new LineSeries {
                Title = "Измерения",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.LightBlue
            };

            // Add data points from Measurings collection
            foreach(var measuring in Picket.Measurings) {
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(measuring.MeasuringDateTime), measuring.MeasuringValue));
                // Update axis ranges based on data
                yAxis.StartPosition = Math.Min(yAxis.StartPosition, measuring.MeasuringValue);
                yAxis.EndPosition = Math.Max(yAxis.EndPosition, measuring.MeasuringValue);
            }

            plotModel.Series.Add(lineSeries);

            PlotModel = plotModel;
        }
    }
}
