using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockTracker.ViewModel
{
    public class StockTrackerViewModel: INotifyPropertyChanged
    {

        public StockTrackerViewModel()
        {
            this.Run = new DelegateCommand<object>(this.OnRun);
        }

        #region Field        
        private string _ticker = "Please Choose Ticker";
        public string SelectedTicker
        {
            get
            {
                return _ticker;
            }
            set
            {
                _ticker = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> _tickerList = System.Enum.GetNames(typeof(TickerList)).ToList();
        public List<string> TickerList
        {
            get
            {
                return _tickerList;
            }
        }

        public ICommand Run { get; private set; }


    #endregion Field

    #region function

        public void OnRun(object arg)
        {
            MessageBox.Show(string.Format("Success Running.. Ticker Name: {0}", SelectedTicker));
        }

    #endregion function


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public enum TickerList
    {
        GOOGL,
        AAPL,
        BABA
    }
}
