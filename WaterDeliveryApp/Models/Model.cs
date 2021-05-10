using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterDeliveryApp.Domain
{
    public class Clients
    {
        [Key]
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public int ActiveOrdersCount { get; set; }
        public int OrdersCount { get; set; }

        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = LastName + " " + FirstName + " " + Patronymic; }
        }

        public Clients()
        {

        }
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
        [Key]
        public int WaterTypeId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
    }

    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public DateTime DeliveryTime { get; set; }
        public bool IsDelivered { get; set; }
        //public List<OrderWater> OrderWaters { get; set; }
    }

    public class OrderWater
    {
        [Key]
        public int OrderWaterId { get; set; }
        public int OrderId { get; set; }
        public int WaterTypeId { get; set; }
        public int Count { get; set; }
    }

    public class WaterList
    {

        public int OrderId { get; set; }
        public int WaterTypeId { get; set; }
        public int Count { get; set; }
        public int IsAddToOrder { get; set; }
    }

    public class ViewOrders
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string NameWater { get; set; }
        public decimal SumDelivery { get; set; }
        public DateTime DeliveryTime { get; set; }
        public bool isDelivered { get; set; }
    }
}
