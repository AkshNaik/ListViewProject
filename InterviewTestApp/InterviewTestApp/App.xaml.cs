using System;
using InterviewTestApp.ViewModels;
using InterviewTestApp.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterviewTestApp
{
    public partial class App : PrismApplication
    {
        public App (IPlatformInitializer initializer = null): base(initializer)
        {
            
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("ListPage");
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ListPage,ListPageViewModel>();
            containerRegistry.RegisterForNavigation<ListDetailPage, ListDetailPageViewModel>();
        }
    }
}

