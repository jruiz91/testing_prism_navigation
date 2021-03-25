using Prism.Commands;
using Prism.Mvvm;
using BlankApp1.Models;
using BlankApp1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Prism.Services.Dialogs;
using Prism.Regions;
using BlankApp1.Services;

namespace BlankApp1.ViewModels
{
    public class NotifierObservableCollection<T> : ObservableCollection<T>
    {
        public void Refresh()
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
    class AppleListViewModel : BindableBase, INavigationAware
    {
        private IDialogService _dialogService;
        private IRegionManager _regionManager;
        private IAppleService _appleService;
        private AppleModel _selectedApple;
        private NotifierObservableCollection<AppleModel> _myAppleList;
        private DelegateCommand _addAppleCommand;
        public DelegateCommand AddAppleCommand =>
            _addAppleCommand ?? (_addAppleCommand = new DelegateCommand(AddApple));
        private DelegateCommand<AppleModel> _appleSelectedCommand;
        public AppleModel SelectedApple
        {
            get { return _selectedApple; }
            set { SetProperty(ref _selectedApple, value); }
        }
        public NotifierObservableCollection<AppleModel> MyAppleList
        {
            get { return _myAppleList; }
            set { SetProperty(ref _myAppleList, value); }
        }
        public DelegateCommand<AppleModel> AppleSelectedCommand =>
            _appleSelectedCommand ?? (_appleSelectedCommand = new DelegateCommand<AppleModel>(SelectApple));

        public AppleListViewModel(IDialogService dialogService,
            IRegionManager regionManager,
            IAppleService appleService)
        {
            this._dialogService = dialogService;
            this._regionManager = regionManager;
            this._appleService = appleService;
            this._appleService.SaveApple(new AppleModel() { Name = "Ariane", Colour = "Red" });
            this._appleService.SaveApple(new AppleModel() { Name = "Golden", Colour = "Yellow" });
            this.MyAppleList = new NotifierObservableCollection<AppleModel>();
            this.MyAppleList.AddRange(this._appleService.GetApples());
        }
        void AddApple()
        {
            this._regionManager.RequestNavigate("RegionOne", "AppleDetail");
        }
        void SelectApple(AppleModel apple)
        {
            NavigationParameters p = new NavigationParameters();
            p.Add("apple", apple);
            this._regionManager.RequestNavigate("RegionOne", nameof(Views.AppleDetail), p);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}