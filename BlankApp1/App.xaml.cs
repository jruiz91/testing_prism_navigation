using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using BlankApp1.Services;

namespace BlankApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Views.MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<Views.AppleDialog, ViewModels.AppleDialogViewModel>();
            containerRegistry.RegisterForNavigation<Views.AppleList>(nameof(Views.AppleList));
            containerRegistry.RegisterForNavigation<Views.AppleDetail>(nameof(Views.AppleDetail));
            containerRegistry.RegisterInstance(typeof(IAppleService), new AppleService());
        }
    }
}
