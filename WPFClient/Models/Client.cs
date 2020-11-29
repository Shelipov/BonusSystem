using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public class Client : INotifyPropertyChanged
    {
        int _ClientID;
        User _User;
        BonusCard _BonusCard;
        DateTime _LastChangedOn;

        public int ClientID
        {
            get { return _ClientID; }
            set
            {
                _ClientID = value;
                OnPropertyChanged("ClientID");
            }
        }
        public User User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }
        public BonusCard BonusCard
        {
            get { return _BonusCard; }
            set
            {
                _BonusCard = value;
                OnPropertyChanged("BonusCard");
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
