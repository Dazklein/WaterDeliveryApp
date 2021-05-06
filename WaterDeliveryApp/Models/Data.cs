using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterDeliveryApp.Domain;

namespace WaterDeliveryApp.Models
{
    public static class Data
    {
        public static List<Clients> GetClients()
        {
            using (ApplicationDBContext db = new ApplicationDBContext())
            {
                return db.Clients.ToList();
            }
        }

        public static List<WaterTypes> GetWaterTypes()
        {
            using (ApplicationDBContext db = new ApplicationDBContext())
            {
                return db.WaterTypes.ToList();
            }
        }

        public static List<Orders> GetOrders()
        {
            using (ApplicationDBContext db = new ApplicationDBContext())
            {
                return db.Orders.ToList();
            }
        }

        public static List<ViewOrders> GetViewOrders()
        {
            using (ApplicationDBContext db = new ApplicationDBContext())
            {
                return (from orderWaters in db.OrderWaters
                       join water in db.WaterTypes on orderWaters.WaterTypeId equals water.WaterTypeId
                       join order in db.Orders on orderWaters.OrderId equals order.OrderId
                       join client in db.Clients on order.ClientId equals client.ClientId
                       group new { client, water, order } by new
                       {
                           client.FullName,
                           client.Phone,
                           client.Adress,
                           water.Name,
                           order.DeliveryTime,
                           order.IsDelivered
                       } into g
                       select new ViewOrders()
                       {
                           FullName = g.Key.FullName,
                           Phone = g.Key.Phone,
                           Adress = g.Key.Adress,
                           NameWater = g.Key.Name,
                           SumDelivery = g.Sum(x => x.water.Cost),
                           DeliveryTime = g.Key.DeliveryTime,
                           isDelivered = g.Key.IsDelivered
                       }).ToList();
            }
        }
    }
}
