using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

//Added mobile number form
//that must be 10 digits in length,
//otherwise you get an error message.

namespace ContactsApp.ViewModels
{
    public class AddContactViewModel : INotifyPropertyChanged
    {

        public AddContactViewModel()
        {
            SaveContactCommand = new Command(async () => await SaveContact(),
                                            () => !IsBusy);
        }

        string name = "James Montemagno";
        string number = "1234567890";
        bool bestFriend;
        bool isBusy = false;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public bool BestFriend
        {
            get { return bestFriend; }
            set
            {
                bestFriend = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public string DisplayMessage
        {
            get
            {
                return $"Your new friend is named {Name} and " +
                       $"{(bestFriend ? "is" : "is not")} your best friend.";
            }
        }

        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;

                OnPropertyChanged();
                SaveContactCommand.ChangeCanExecute();
            }
        }

        public Command SaveContactCommand { get; }

        async Task SaveContact()
        {
            IsBusy = true;
            await Task.Delay(4000);

            IsBusy = false;
            if (Number.Length != 10)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Mobile number must be 10 digits in length.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Save", "Contact has been saved", "OK");
            }
        }

    }
}