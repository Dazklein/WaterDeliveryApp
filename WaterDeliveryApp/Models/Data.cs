using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterDeliveryApp.Domain;
using WaterDeliveryApp.View;

namespace WaterDeliveryApp.Models
{
    public static class Data
    {
        public static List<Clients> GetClients()
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    return db.Clients.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageWindow wnd = new MessageWindow(ex.Message);
                throw;
            }
        }

        public static List<WaterTypes> GetWaterTypes()
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    return db.WaterTypes.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageWindow wnd = new MessageWindow(ex.Message);
                throw;
            }
        }

        public static List<Orders> GetOrders()
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    return db.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageWindow wnd = new MessageWindow(ex.Message);
                throw;
            }
        }
        
        public static List<ViewOrders> GetViewOrders()
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    List<ViewOrders> query = (from orderWaters in db.OrderWaters
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
                    return query;
                }
            }
            catch (Exception ex)
            {
                MessageWindow wnd = new MessageWindow(ex.Message);
                throw;
            }
        }
        
        public static string AddClient(Clients client)
        {
            Clients newClient = new Clients(client.FirstName, client.LastName, client.Patronymic, client.Phone, client.Adress);
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.Clients.Add(newClient);
                    db.SaveChanges();
                }
                return "Успех";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string AddWaterType(WaterTypes water)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.WaterTypes.Add(water);
                    db.SaveChanges();
                }
                return "Успех";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static int AddOrder(Orders order)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return order.OrderId;
                }
            }
            catch (Exception ex)
            {
                MessageWindow wnd = new MessageWindow(ex.Message);
                throw;
            }
        }

        public static string AddOrderWaterRelation(int orderId, int waterId, int countWater)
        {
            OrderWater entity = new OrderWater()
            {
                OrderId = orderId,
                WaterTypeId = waterId,
                Count = countWater
            };

            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.OrderWaters.Add(entity);
                    db.SaveChanges();
                    return "Успех";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EditClient(Clients client)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.Clients.Update(client);
                    db.SaveChanges();
                    return "Успех";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EditOrder(Orders order)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.Orders.Update(order);
                    db.SaveChanges();
                    return "Успех";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EditWaterType(WaterTypes waterType)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.WaterTypes.Update(waterType);
                    db.SaveChanges();
                    return "Успех";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string DeleteClient(Clients client)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                    return "Успех";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string DeleteOrder(Orders order)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    List<OrderWater> orderWater = db.OrderWaters.Where(x => x.OrderId == order.OrderId).ToList();
                    db.OrderWaters.RemoveRange(orderWater);
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    return "Успех";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string DeleteWaterType(WaterTypes waterType)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    db.WaterTypes.Remove(waterType);
                    db.SaveChanges();
                    return "Успех";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
