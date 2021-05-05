using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterDeliveryApp.Domain
{
    public class Clients
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }

        public Clients(string firstName, string lastName, string patronymic, string phone, string adress)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Phone = phone;
            Adress = adress;
        }
    }

    public class WaterTypes
    {
        public int WaterTypeId { get; set; }
        public string Name { get; set; }
        public decimal Coast { get; set; }
        public string Description { get; set; }
    }

    public class Orders
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public DateTime DeliveryTime { get; set; }
        public bool IsDelivered { get; set; }
    }

    public class OrderWater
    {
        public int OrderWaterId { get; set; }
        public int OrderId { get; set; }
        public int WaterTypeId { get; set; }
    }
}
