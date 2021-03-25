using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BlankApp1.Models;
using BlankApp1.Services;

namespace BlankApp1.ViewModels
{
    public class AppleDetailViewModel : BindableBase, INavigationAware
    {
        #region Private properties
        private IRegionManager _regionManager;
        private IAppleService _appleService;
        private AppleModel _currentApple;
        private bool _appleReadOnlyMode;
        private bool _editEnabled;
        private bool _saveEnabled;
        private Visibility _pencilVisibility;
        private DelegateCommand _editCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _returnCommand;
        #endregion
        #region Public properties

        public AppleModel CurrentApple
        {
            get { return _currentApple; }
            set { SetProperty(ref _currentApple, value); }
        }
        public bool AppleReadOnlyMode
        {
            get { return _appleReadOnlyMode; }
            set { SetProperty(ref _appleReadOnlyMode, value); }
        }
        public bool EditEnabled
        {
            get { return _editEnabled; }
            set { SetProperty(ref _editEnabled, value); }
        }
        public bool SaveEnabled
        {
            get { return _saveEnabled; }
            set { SetProperty(ref _saveEnabled, value); }
        }
        public Visibility PencilVisibility
        {
            get { return _pencilVisibility; }
            set { SetProperty(ref _pencilVisibility, value); }
        }
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(Save, () => SaveEnabled).ObservesProperty(() => SaveEnabled));
        public DelegateCommand EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand(StartEditing, () => EditEnabled).ObservesProperty(() => EditEnabled));

        public DelegateCommand ReturnCommand =>
            _returnCommand ?? (_returnCommand = new DelegateCommand(GoBack));
        #endregion
        #region Constructor
        public AppleDetailViewModel(IRegionManager regionManager, IAppleService appleService)
        {
            this._regionManager = regionManager;
            this._appleService = appleService;
        }
        #endregion
        #region Command functions
        void Save()
        {
            this._appleService.SaveApple(this.CurrentApple);
        }
        void StartEditing()
        {
            this.AppleReadOnlyMode = false;
            this.SaveEnabled = true;
            this.EditEnabled = false;
        }
        private void GoBack()
        {
            NavigationParameters p = new NavigationParameters() { { "refresh", true } };
            this._regionManager.RequestNavigate("RegionOne", nameof(Views.AppleList), p);
        }
        #endregion
        #region Navigation related functions
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("apple"))
            {
                CurrentApple = navigationContext.Parameters.GetValue<AppleModel>("apple");
                this.AppleReadOnlyMode = true;
                this.EditEnabled = true;
                this.SaveEnabled = false;
                PencilVisibility = Visibility.Visible;
            }
            else
            {
                CurrentApple = new AppleModel();
                this.AppleReadOnlyMode = false;
                this.EditEnabled = false;
                this.SaveEnabled = true;
                PencilVisibility = Visibility.Hidden;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        #endregion
    }
}