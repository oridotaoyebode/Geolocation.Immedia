using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Immedia.Demo.Geolocation.Helpers;
using Immedia.Demo.Geolocation.ViewModel;

namespace Immedia.Demo.Geolocation
{
    public static class App
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    // Initialize the MVVM Light DispatcherHelper.
                    // This needs to be called on the UI thread.
                    DispatcherHelper.Initialize();

                    // Configure and register the MVVM Light NavigationService
                    var nav = new BaseNavigationService();
                    SimpleIoc.Default.Register<INavigationService>(() => nav);
                    nav.Configure(ViewModelLocator.DetailsPageKey, typeof(PlaceDetailsActivity));
                    nav.Configure(ViewModelLocator.DetailsPageKey, typeof(PlaceDetailsActivity));

                    // Register the MVVM Light DialogService
                    SimpleIoc.Default.Register<IDialogService, BaseDialogService>();

                    _locator = new ViewModelLocator();
                }

                return _locator;
            }
        }
    }
}