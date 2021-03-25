using Prism.Regions;
using System.Windows;
using Unity;
using BlankApp1.Views;

namespace BlankApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;
        public MainWindow(IRegionManager regionManager, IUnityContainer container)
        {
            InitializeComponent();
            this._unityContainer = container;
            this._regionManager = regionManager;
            this._regionManager.RegisterViewWithRegion("RegionOne", typeof(AppleList));
            this._regionManager.RegisterViewWithRegion("RegionOne", typeof(AppleDetail));
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this._regionManager.RequestNavigate("RegionOne", "AppleList");
        }
    }
}
