using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Immedia.Demo.Geolocation.Model;
using Immedia.Demo.Geolocation.Services;
using Microsoft.Practices.ServiceLocation;

namespace Immedia.Demo.Geolocation.ViewModel
{
    public class PlaceDetailsViewModel: ViewModelBase
    {
        private readonly IGooglePlaceService _googlePlaceService;
        private readonly INavigationService _navigationService;
        public PlacesSearchResult.Result Place { get; }
        public PlaceDetails PlaceDetails { get; set; }
        public PlaceDetailsViewModel(IGooglePlaceService googlePlaceService, INavigationService navigationService, PlacesSearchResult.Result place)
        {
            _googlePlaceService = googlePlaceService;
            _navigationService = navigationService;
            Place = place;
            //RefreshCommand.Execute(null);
        }

        private RelayCommand<PlacesSearchResult.Photo> _showDetailsCommand;

        public RelayCommand<PlacesSearchResult.Photo> ShowDetailsCommand
        {
            get
            {
                return _showDetailsCommand
                       ?? (_showDetailsCommand = new RelayCommand<PlacesSearchResult.Photo>(
                           async p =>
                           {


                               _navigationService.NavigateTo(ViewModelLocator.DetailsPageKey, p);

                           }));
            }
        }


    }
}