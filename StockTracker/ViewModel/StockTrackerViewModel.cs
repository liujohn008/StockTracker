using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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

        private List<HistoricalStock> _stockPrice = new List<HistoricalStock>();
        public List<HistoricalStock> StockPrice
        {
            get
            {
                return _stockPrice;
            }
            set
            {
                _stockPrice = value;
                NotifyPropertyChanged();
            }

        }

        public ICommand Run { get; private set; }


    #endregion Field

    #region function

        public void OnRun(object arg)
        {
            //MessageBox.Show(string.Format("Success Running.. Ticker Name: {0}", SelectedTicker));
            //this.StockPrice = DownloadData(this.SelectedTicker, 2015);

            var t = Task.Run(() => DownloadData(SelectedTicker, 2015));
            
        }

        public void DownloadData(string ticker, int yearToStartFrom)
        {
            List<HistoricalStock> retval = new List<HistoricalStock>();
            using (WebClient web = new WebClient())
            {
                string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", ticker, yearToStartFrom));

                data = data.Replace("r", "");

                string[] rows = data.Split('\n');

                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("n", "").Trim() == "") continue;

                    string[] cols = rows[i].Split(',');

                    HistoricalStock hs = new HistoricalStock();
                    hs.Date = Convert.ToDateTime(cols[0]);
                    hs.Open = Convert.ToDouble(cols[1]);
                    hs.High = Convert.ToDouble(cols[2]);
                    hs.Low = Convert.ToDouble(cols[3]);
                    hs.Close = Convert.ToDouble(cols[4]);
                    hs.Volume = Convert.ToDouble(cols[5]);
                    hs.AdjClose = Convert.ToDouble(cols[6]);

                    retval.Add(hs);
                }

                this.StockPrice = retval;
            }
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

    public class HistoricalStock
    {
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double AdjClose { get; set; }
    }
}
