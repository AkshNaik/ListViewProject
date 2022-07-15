using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using InterviewTestApp.Services;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using static InterviewTestApp.Models.Detail;

namespace InterviewTestApp.ViewModels
{
    public class ListPageViewModel : INotifyPropertyChanged
    {
        public ICommand listTapped { get; set; }
        public ICommand loadMoreItemsCommand { get; set; }

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Datum> dataList { get; set; }
        private InfiniteScrollCollection<Datum> userInfo;
        public InfiniteScrollCollection<Datum> UserInfo
        {
            get { return userInfo; }
            set
            {
                userInfo = value;
                OnPropertyChanged("UserInfo");
            }
        }
        int batchSize = 3;
        private int currentIndex = 1;
        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        public static RestService<Object, ObservableCollection<Root>> DataServices { get; private set; }
        #endregion
        INavigationService _navigationService;

        public ListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            DataServices = new RestService<object, ObservableCollection<Root>>();
            listTapped = new Command(ListTappedAction);
            loadMoreItemsCommand = new Command(LoadMoreAsync);
            RetrieveData();
        }

        #region Methods
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private async void LoadMoreAsync(object obj)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await Task.Delay(1000);
            UserInfo.AddRange(
                dataList.Skip(batchSize * currentIndex).Take(batchSize)
            );
            currentIndex += batchSize;
            IsBusy = false;
        }
        private void ListTappedAction(object obj)
        {
            Datum datum = (Datum)obj;
            NavigationParameters parameters = new NavigationParameters
            {
                 {"email",datum.email },
                 {"firstName",datum.first_name },
                 {"lastName",datum.last_name },
                 {"avatar",datum.avatar }
            };
            _navigationService.NavigateAsync("ListDetailPage", parameters);
        }
        private async void RetrieveData()
        {
            var data = await RestService<object, Root>.GetDataAsync();
            dataList = data.data;
            UserInfo = new InfiniteScrollCollection<Datum>(dataList.Take(batchSize).ToList());
        }
        #endregion


    }
}

