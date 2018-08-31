using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Android.Widget;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Immedia.Demo.Geolocation.Model;
using Immedia.Demo.Geolocation.Services;
using Plugin.Geolocator;

namespace Immedia.Demo.Geolocation.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="Clock" /> property's name.
        /// </summary>
        public const string ClockPropertyName = "Clock";

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IGooglePlaceService _googlePlaceService;
        private readonly IDialogService _dialogService;
        private string _clock = "Starting...";
        private RelayCommand _incrementCommand;
        private int _index;
        private RelayCommand<string> _navigateCommand;
        private bool _runClock;
        private RelayCommand<string> _showDialogCommand;
        private string _welcomeTitle = "Hello MVVM";

        /// <summary>
        /// Sets and gets the Clock property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// Use the "mvvminpc*" snippet group to create more such properties.
        /// </summary>
        public string Clock
        {
            get
            {
                return _clock;
            }
            set
            {
                Set(ref _clock, value);
            }
        }

        private bool _isLoading = true;
        private DateTime _lastLoaded = DateTime.MinValue;



        /// <summary>
        /// Gets the ShowDialogCommand.
        /// Use the "mvvmr*" snippet group to create more such commands.
        /// </summary>



        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            IDataService dataService,
            INavigationService navigationService, IGooglePlaceService googlePlaceService, IDialogService dialogService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _googlePlaceService = googlePlaceService;
            _dialogService = dialogService;
            Places = new ObservableCollection<PlaceDetailsViewModel>();
            RefreshCommand.Execute(null);
        }
        public bool Loading

        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }
        public ObservableCollection<PlaceDetailsViewModel> Places { get; }

        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                       ?? (_refreshCommand = new RelayCommand(
                           async () =>
                           {
                               Places.Clear();
                               _isLoading = true;
                               
                               RefreshCommand.RaiseCanExecuteChanged();
                                 

                               Exception error = null;

                               try
                               {
                                   var geolocation = await CrossGeolocator.Current.GetPositionAsync();

                                   var list = await _googlePlaceService.GetNearbyPlaces(
                                       new GeoLocation(geolocation.Latitude, geolocation.Longitude));


                                   foreach (var result in list.Results)
                                   {
                                       Places.Add(new PlaceDetailsViewModel(_googlePlaceService, result));
                                   }

                                  
                               }
                               catch (Exception ex)
                               {
                                   error = ex;
                               }

                               if (error != null)
                               {
                                   var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                                   await dialog.ShowError(error, "Error when refreshing", "OK", null);
                               }
                               Loading = false;
                               
                               RefreshCommand.RaiseCanExecuteChanged();
                              
                           }));
            }
        }
        private RelayCommand<PlaceDetailsViewModel> _showDetailsCommand;

        public RelayCommand<PlaceDetailsViewModel> ShowDetailsCommand
        {
            get
            {
                return _showDetailsCommand
                       ?? (_showDetailsCommand = new RelayCommand<PlaceDetailsViewModel>(async placeResults =>
                           {
                               if (!ShowDetailsCommand.CanExecute(placeResults))
                               {
                                   return;
                               }

                               var placeDetails = await _googlePlaceService.GetPlaceDetails(placeResults.Place.PlaceId);
                               if (placeDetails.Result.Photos!= null && placeDetails.Result.Photos.Any())
                               {
                                   _navigationService.NavigateTo(ViewModelLocator.DetailsPageKey, placeDetails);

                               }
                               else
                               {
                                   await _dialogService.ShowError("No Photos for this location", "Error", "OK", null);
                               }
                           },
                           placeDetails => placeDetails != null));
            }
        }

    }
}