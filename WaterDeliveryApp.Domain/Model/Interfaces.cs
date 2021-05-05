using System;

namespace WaterDeliveryApp.Domain
{
    public interface IDBOperation
    {
        public object GetData();
        public void Add();
        public void Edit();
        public void Delete();
    }
}
