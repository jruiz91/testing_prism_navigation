using BlankApp1.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlankApp1.ViewModels
{
    public class AppleDialogViewModel : BindableBase, IDialogAware
    {
        private bool _editing = false;
        private AppleModel _myApple;
        public AppleModel MyApple
        {
            get { return _myApple; }
            set { SetProperty(ref _myApple, value); }
        }
        public AppleDialogViewModel()
        {
            this.MyApple = new AppleModel() { Colour = "null", Name = "null" };
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }
        protected virtual void OnCloseDialog()
        {
            RaiseRequestClose(new Prism.Services.Dialogs.DialogResult(ButtonResult.Cancel));
        }

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            this.MyApple = parameters.GetValue<AppleModel>("apple");
            if (this.MyApple == null)
            {
                this.MyApple = new AppleModel();
                this.Title = "Add new apple";
            }
            else
            {
                this.Title = "Edit apple";
                this._editing = true;
            }
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult) { RequestClose?.Invoke(dialogResult); }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(SaveApple));

        private void SaveApple()
        {
            RaiseRequestClose(new Prism.Services.Dialogs.DialogResult(ButtonResult.OK, new DialogParameters()
            {
                { "apple", MyApple },
                { "editing",  _editing }
            }));
        }


    }
}
