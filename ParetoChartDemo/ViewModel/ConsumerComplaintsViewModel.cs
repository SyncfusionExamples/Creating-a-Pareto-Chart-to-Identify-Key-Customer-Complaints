using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ParetoChartDemo
{
    public class ComplaintsModel
    {
        public string Defect { get; set; }
        public double Frequency { get; set; }
    }
    
    public class ConsumerComplaintsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ComplaintsModel> data;
        
        public ObservableCollection<ComplaintsModel> DataValues
        {
            get { return data; }
            set
            {
                if (value != data)
                {
                    data = value;
                    CalculateCumulative(data);
                }
                
                OnPropertyChanged("DataValues");
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public ObservableCollection<CumulativeModel> CumulativeSource { get; set; }

        public ObservableCollection<Brush> CustomPalette { get; set; }

        public ConsumerComplaintsViewModel()
        {
            CumulativeSource = new ObservableCollection<CumulativeModel>();

            DataValues = new ObservableCollection<ComplaintsModel>()
            {
                new ComplaintsModel(){Defect = "Mortgage", Frequency=225394 },
                new ComplaintsModel(){Defect = "Debt collection", Frequency=145071},
                new ComplaintsModel(){Defect = "Credit reporting", Frequency=139929},
                new ComplaintsModel(){Defect = "Credit card", Frequency=88471},
                new ComplaintsModel(){Defect = "Bank account\n or service", Frequency=84643},
                new ComplaintsModel(){Defect = "Student loan", Frequency=32315},
                new ComplaintsModel(){Defect = "Consumer Loan", Frequency=31411},
                new ComplaintsModel(){Defect = "Payday loan", Frequency=5523},
                new ComplaintsModel(){Defect = "Money transfers", Frequency=5155},
                new ComplaintsModel(){Defect = "Prepaid card", Frequency=3774},
                new ComplaintsModel(){Defect = "Other financial\n service", Frequency=1031},
                new ComplaintsModel(){Defect = "Virtual currency", Frequency=17},
            };
            
            CustomPalette = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#E91E63")),
                new SolidColorBrush(Color.FromArgb("#C2185B")),
                new SolidColorBrush(Color.FromArgb("#9C27B0")),
                new SolidColorBrush(Color.FromArgb("#5727B0")),
                new SolidColorBrush(Color.FromArgb("#272AB0")),
                new SolidColorBrush(Color.FromArgb("#276BB0")),
                new SolidColorBrush(Color.FromArgb("#57ACDC")),
                new SolidColorBrush(Color.FromArgb("#570CBE")),
                new SolidColorBrush(Color.FromArgb("#60C689")),
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CalculateCumulative(object source)
        {
            var dataSource = source as ObservableCollection<ComplaintsModel>;
            if (dataSource != null && dataSource.Count > 0)
            {
                CumulativeSource.Clear();
                var orderedList = dataSource.OrderByDescending(x => x.Frequency).ToList();
                double cumulativeCount = 0;
                foreach (var data in orderedList)
                {
                    cumulativeCount += data.Frequency;
                    CumulativeSource.Add(new CumulativeModel() { Defect = data.Defect, YValues = data.Frequency, CumulativeCount = cumulativeCount });
                }

                if (cumulativeCount > 0)
                {
                    foreach (var data in CumulativeSource)
                    {
                        data.CumulativeFrequency = (data.CumulativeCount / cumulativeCount) * 100;
                    }
                }
            }

        }
    }
}
