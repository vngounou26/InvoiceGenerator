﻿using InvoiceApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class OrderModel:Order
    {
        HttpClient httpClient = new HttpClient();
        IList<OrderDetail> orderDetails = new List<OrderDetail>();
        private double total = 0;

        public OrderModel()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44365/api/");
        }


        //caluler le prix total de la commande
        public string Total
        {
            get
            {
                foreach (var item in this.orderDetails)
                {
                    total += (double) item.UnitPrice * item.Quantity;
                }
                return total.ToString("C");
            }
        }


        //fonction pour formater la date de la commande
        
        /// <summary>
        /// Format Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string DateFormatted(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }



        /// <summary>
        /// obtenir les lignes de commande d'une commande
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<OrderDetail> GetOrderLines(int orderId)
        {
            var response = httpClient.GetStringAsync("OrderDetail/" + orderId).Result;
            var orderLines = JsonConvert.DeserializeObject<IList<OrderDetail>>(response);
            return orderLines;
        }

        //fonction pour obtenir le prix total de chaque commandes

        /// <summary>
        /// Get Invoice Total price
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public string GetTotal(int orderId)
        {
            total = 0;
            orderDetails = GetOrderLines(orderId);
            return Total;
        }

        /// <summary>
        /// obtenir les commandes d'un client
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders()
        {
            string response = httpClient.GetStringAsync("Orders").Result;
            var orders = JsonConvert.DeserializeObject<List<Order>>(response);
            return orders;
        }

        /// <summary>
        /// Get Invoices List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetOrder(int id)
        {
            var response = httpClient.GetStringAsync("Orders/" + id).Result;
            var order = JsonConvert.DeserializeObject<Order>(response);
            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> AddOrder(Order order)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Orders", order);
            if (response.IsSuccessStatusCode)
            {
                order = await response.Content.ReadAsAsync<Order>();
            }
            return order;
        }

        //update order and return updated order
        public void UpdateOrder(int id,Order order)
        {

            var response = httpClient.PutAsJsonAsync("Orders/" + id, order).Result;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            Order order = new Order();
            HttpResponseMessage response = await httpClient.DeleteAsync("Orders/" + id);
            if (response.IsSuccessStatusCode)
            {
                order = await response.Content.ReadAsAsync<Order>();
            }
            return order;
        }
    }
}
