using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public class ViewModel : INotifyPropertyChanged
    {
        int _BonusCardID;
        string _BonusCardNumber;
        DateTime _BonusCardTimeEnd;
        decimal _BonusCardBalanse;
        int _UserID;
        string _UserEmail;
        string _UserFullName;
        string _UserName;
        string _UserPhoneNumber;
        public decimal? AddSumm { get; set; }
        public decimal? RemuveSumm { get; set; }

        public int UserID
        {
            get { return _UserID; }
            set
            {
                _UserID = value;
                OnPropertyChanged("ClientID");
            }
        }
        public string UserEmail
        {
            get { return _UserEmail; }
            set
            {
                _UserEmail = value;
                OnPropertyChanged("UserEmail");
            }
        }
        public string UserFullName
        {
            get { return _UserFullName; }
            set
            {
                _UserFullName = value;
                OnPropertyChanged("UserFullName");
            }
        }
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }
        public string UserPhoneNumber
        {
            get { return _UserPhoneNumber; }
            set
            {
                _UserPhoneNumber = value;
                OnPropertyChanged("UserPhoneNumber");
            }
        }

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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
