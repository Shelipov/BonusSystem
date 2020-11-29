using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public class User : INotifyPropertyChanged
    {
        int _UserID;
        string _UserEmail;
        string _UserFullName;
        string _UserName;
        public string _UserPhoneNumber;
        public DateTime _LastChangedOn;

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
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserPhoneNumber");
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
