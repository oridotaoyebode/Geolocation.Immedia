using System;

namespace Immedia.Demo.Geolocation.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}