﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Views;

namespace Immedia.Demo.Geolocation.Helpers
{
    public class BaseNavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private readonly Dictionary<string, object> _parametersByKey = new Dictionary<string, object>();

        private const string RootPageKey = "-- ROOT --";
        private const string ParameterKeyName = "ParameterKey";

        public string CurrentPageKey => BaseActivity.CurrentActivity.ActivityKey ?? RootPageKey;

        public void GoBack()
        {
            BaseActivity.GoBack();
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            BaseActivity.CurrentActivity.RunOnUiThread(() =>
            {
                if (BaseActivity.CurrentActivity == null)
                    throw new InvalidOperationException("No CurrentActivity found");

                lock (_pagesByKey)
                {
                    if (!_pagesByKey.ContainsKey(pageKey))
                        throw new ArgumentException(
                            $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?",
                            nameof(pageKey));

                    var intent = new Intent(BaseActivity.CurrentActivity, _pagesByKey[pageKey]);
                    if (parameter != null)
                    {
                        lock (_parametersByKey)
                        {
                            var guid = Guid.NewGuid().ToString();
                            _parametersByKey.Add(guid, parameter);
                            intent.PutExtra(ParameterKeyName, guid);
                        }
                    }

                    BaseActivity.CurrentActivity.StartActivity(intent);
                    BaseActivity.NextPageKey = pageKey;
                }
            });
        }

        public void Configure(string key, Type activityType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                    _pagesByKey[key] = activityType;
                else
                    _pagesByKey.Add(key, activityType);
            }
        }

        public object GetAndRemoveParameter(Intent intent)
        {
            if (intent == null)
                throw new ArgumentNullException(nameof(intent),
                    "This method must be called with a valid Activity intent");

            var stringExtra = intent.GetStringExtra(ParameterKeyName);
            if (string.IsNullOrEmpty(stringExtra))
                return null;

            lock (_parametersByKey)
                return _parametersByKey.ContainsKey(stringExtra) ? _parametersByKey[stringExtra] : null;
        }

        public T GetAndRemoveParameter<T>(Intent intent)
        {
            return (T) GetAndRemoveParameter(intent);
        }
    }
}