using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private User selectedUser;
        private Client selectedClient;
        private BonusCard selectedBonusCard;
        private ViewModel selectedViewModel;

        public ObservableCollection<User> Phones { get; set; }
        public ObservableCollection<Client> Client { get; set; }
        public ObservableCollection<BonusCard> BonusCard { get; set; }
        public ObservableCollection<ViewModel> ViewModel { get; set; }

        public ViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        public BonusCard SelectedBonusCard
        {
            get { return selectedBonusCard; }
            set
            {
                selectedBonusCard = value;
                OnPropertyChanged("SelectedBonusCard");
            }
        }
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        public ApplicationViewModel()
        {
            Phones = new ObservableCollection<User>();
            Client = new ObservableCollection<Client>();
            BonusCard = new ObservableCollection<BonusCard>();
            ViewModel = new ObservableCollection<ViewModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
