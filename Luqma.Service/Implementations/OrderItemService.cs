using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
    public class OrderItemService : IOrderItemService

    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrderItemService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IMenuItemRepository menuItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _menuItemRepository = menuItemRepository;
        }
        public async Task<string> AddItemToOrderAsync(int ItemId, int quantity)
        {
            var cashierid = int.Parse(_orderItemRepository.ExtractUserIdFromToken());
            var orderid = await _orderRepository.GetLastOrderIdAsync(cashierid);
            var item = await _menuItemRepository.GetByIdAsync(ItemId);
            var orderitem = new OrderItem()
            {
                OrderId = orderid,
                ItemId = ItemId,
                Quantity = quantity,
                TotalPrice = Math.Round((item.Price - (item.Price * item.Discount)) * quantity)
            };
            await _orderItemRepository.AddAsync(orderitem);
            return "the item is added to order successfully";
        }
        public async Task<string> DeleteItemFromOrderAsync(int ItemId)
        {
            var cashierid = int.Parse(_orderItemRepository.ExtractUserIdFromToken());
            var orderid = await _orderRepository.GetLastOrderIdAsync(cashierid);
            var orderItem = await _orderItemRepository.getOrderItemAsync(orderid, ItemId);
            await _orderItemRepository.DeleteAsync(orderItem);
            return "the item is deleted from order successfully";
        }
        public async Task<(List<Order>, string)> GetOrdersAsync()
        {
            var cashierid = int.Parse(_orderItemRepository.ExtractUserIdFromToken());
            var orderitems = await _orderItemRepository.getOrderItemsAsync(cashierid);
            var orders = new List<Order>();
            foreach (var oi in orderitems)
            {
                var order = new Order()
                {
                    Id = oi.OrderId,
                    Note = oi.Order.Note,
                    TotalPrice = oi.Order.TotalPrice,
                    Type = oi.Order.Type,
                    Status = oi.Order.Status,
                    Date = oi.Order.Date
                };
                orders.Add(order);
            }
            return (orders, "the orders is fetched successfully");
        }
        public async Task<(List<OrderItem>, string)> GetOrderDetailsAsync(int orderid)
        {
            var orderitems = await _orderItemRepository.getOrderItemsByOrderIdAsync(orderid);
            return (orderitems, "the order details is fetched successfully");

        }
        public async Task<string> UpdateQuantityForItemByCashierAsync(int orderid, int itemid, double quantity)
        {
            var orderitem = await _orderItemRepository.getOrderItemAsync(orderid, itemid);
            orderitem.Quantity = quantity;
            orderitem.TotalPrice = Math.Round((orderitem.MenuItem.Price - (orderitem.MenuItem.Price * orderitem.MenuItem.Discount)) * quantity);
            await _orderItemRepository.SaveChangesAsync();
            var order = await _orderRepository.GetByIdAsync(orderid);
            var orderitems = await _orderItemRepository.getOrderItemsByOrderIdAsync(orderid);
            var TotalPrice = orderitems.Sum(oi => oi.TotalPrice);
            order.TotalPrice = TotalPrice;
            await _orderRepository.SaveChangesAsync();
            return "the quantity is updated by cashier successfully";


        }

        public async Task<(IDocument, string)> getOrderReportAsync(int orderid)
        {
            var orderItems = await _orderItemRepository.getOrderItemsByOrderIdAsync(orderid);
                

            var totalOrderPrice = orderItems.Sum(x => x.TotalPrice);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    // 🔹 Header
                    page.Header()
                        .AlignCenter()
                        .Text("Luqma Order")
                        .SemiBold()
                        .FontSize(28)
                        .FontColor(Colors.Blue.Medium);

                    // 🔹 Content
                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(15);

                            // Order ID
                            column.Item()
                                .Text($"Order ID: {orderid}")
                                .SemiBold()
                                .FontSize(16);

                            // Table
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4); // Item
                                    columns.RelativeColumn(2); // Quantity
                                    columns.RelativeColumn(3); // Total Price
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(6)
                                        .Text("Item").SemiBold();

                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(6)
                                        .AlignCenter().Text("Quantity").SemiBold();

                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(6)
                                        .AlignRight().Text("Total Price").SemiBold();
                                });

                                foreach (var oi in orderItems)
                                {
                                    table.Cell().Padding(6)
                                        .Text(oi.MenuItem.Item);

                                    table.Cell().Padding(6)
                                        .AlignCenter()
                                        .Text(oi.Quantity.ToString());

                                    table.Cell().Padding(6)
                                        .AlignRight()
                                        .Text($"{oi.TotalPrice} $");
                                }
                            });

                            // 🔹 Grand Total
                            column.Item()
                                .PaddingTop(10)
                                .BorderTop(1)
                                .BorderColor(Colors.Grey.Lighten1)
                                .AlignRight()
                                .Text($"Total Price: {totalOrderPrice} $")
                                .SemiBold()
                                .FontSize(18);
                        });

                    // 🔹 Footer
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });

            return (document, "the report is fetched successfully");
        }

    }
}
