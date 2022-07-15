using System;
using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using static InterviewTestApp.Models.Detail;

namespace InterviewTestApp.ViewModels
{
    public class ListDetailPageViewModel : INavigationAware, INotifyPropertyChanged
    {
       INavigationService _navigationService;
       public DelegateCommand backclicked { get; set; }
       public event PropertyChangedEventHandler PropertyChanged;
      
        #region Properties
        private string email;
        public string Email {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        private string avatarURL;
        public string AvatarURL
        {
            get => avatarURL;
            set
            {
                avatarURL = value;
                OnPropertyChanged("AvatarURL");
            }
        }
        #endregion
        public ListDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            backclicked = new DelegateCommand(BackClickedAction);
        }

        #region Methods
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        private void BackClickedAction()
        {
            _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Email = parameters["email"].ToString();
            FirstName = parameters["firstName"].ToString();
            LastName = parameters["lastName"].ToString();
            AvatarURL = parameters["avatar"].ToString();
        }
    }
}

