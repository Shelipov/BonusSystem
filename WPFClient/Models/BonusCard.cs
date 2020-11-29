using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public class BonusCard : INotifyPropertyChanged
    {
        int _BonusCardID;
        string _BonusCardNumber;
        DateTime _BonusCardTimeEnd;
        decimal _BonusCardBalanse;
        DateTime _LastChangedOn;

        public int BonusCardID
        {
            get { return _BonusCardID; }
            set
            {
                _BonusCardID = value;
                OnPropertyChanged("BonusCardID");
            }
        }
        public string BonusCardNumber
        {
            get { return _BonusCardNumber; }
            set
            {
                _BonusCardNumber = value;
                OnPropertyChanged("BonusCardNumber");
            }
        }
        public DateTime BonusCardTimeEnd
        {
            get { return _BonusCardTimeEnd; }
            set
            {
                _BonusCardTimeEnd = value;
                OnPropertyChanged("BonusCardTimeEnd");
            }
        }
        public decimal BonusCardBalanse
        {
            get { return _BonusCardBalanse; }
            set
            {
                _BonusCardBalanse = value;
                OnPropertyChanged("BonusCardBalanse");
            }
        }
        public DateTime LastChangedOn
        {
            get { return _LastChangedOn; }
            set
            {
                _LastChangedOn = value;
                OnPropertyChanged("LastChangedOn");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
