using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Template10.Mvvm;
using BAS.OrderService;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml.Utility;
using System.Collections;
using Template10.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace BAS.ViewModels
{
    class OrdersPageViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }

        private int countUploadElements;
        public int CountUploadElemets
        {
            get
            {
                Windows.Storage.ApplicationDataContainer localSettings =
                    Windows.Storage.ApplicationData.Current.LocalSettings;

                countUploadElements = (int)localSettings.Values["countUploadElements"];

                return countUploadElements;
            }
            private set { }
        }

        private List<Order> DeletedOrders;
        private List<Order> AddedOrders;
        private List<Order> EditedOrders;

        public OrdersPageViewModel()
        {
            Orders = new ObservableCollection<Order>();
            DeletedOrders = new List<Order>();
            AddedOrders = new List<Order>();
            EditedOrders = new List<Order>();

            GetData(null);
        }

        private BaseCommand loadItems;
        public BaseCommand LoadItems
        {
            get
            {
                if (loadItems == null)
                    loadItems = new BaseCommand(GetData, OnCanLoad);

                return loadItems;
            }
        }

        private static bool OnCanLoad(object obj)
        {
            return true;
        }

        private async void GetData(object obj)
        {
            Views.LoadBusy.SetBusy(true);

            OrdersServiceClient ordersClient = new OrdersServiceClient();
            Debug.WriteLine(Orders.LastOrDefault()?.Id ?? 0);

            var orderTask = ordersClient.GetFromToAsync((uint)(Orders.LastOrDefault()?.Id??0) + 1, (uint)(Orders.LastOrDefault()?.Id??0) + (uint)CountUploadElemets);

            var loadOrders = await orderTask;

            await ordersClient.CloseAsync();

            foreach (var order in loadOrders)
            {
                Orders.Add(order);
            }
          
            Views.LoadBusy.SetBusy(false);
        }

        private BaseCommand deleteItems;
        public BaseCommand DeleteItems
        {
            get
            {
                if (deleteItems == null)
                    deleteItems = new BaseCommand(Delete, OnCanDelete);

                return deleteItems;
            }
        }

        private bool OnCanDelete(object deletingObjects)
        {
            return true;
        }

        private async void Delete(object deletingObjects)
        {
            Views.DeleteBusy.SetBusy(true);

            IEnumerable listOfDeletingObjects = deletingObjects as IEnumerable;

            IEnumerable<Order> deletingOrders = listOfDeletingObjects.OfType<Order>();

            DeletedOrders.AddRange(deletingOrders);

            foreach (var deletingOrder in deletingOrders.ToList())
                Orders.Remove(Orders.SingleOrDefault(item => item.Id == deletingOrder.Id));

            Views.DeleteBusy.SetBusy(false);
        }

        private BaseCommand addItem;
        public BaseCommand AddItem
        {
            get
            {
                if (addItem == null)
                    addItem = new BaseCommand(Add, OnCanDelete);

                return addItem;
            }
        }

        private bool OnCanAdd(object addObjects)
        {
            return true;
        }

        private async void Add(object addObjects)
        {
            Views.AddBusy.SetBusy(true);

            Order newOrder = new Order()
            {
                Id = null,
                DateOrder = DateTime.Now,
                Comment = null,
                DeliveryService = null,
                OrderCustomer = null,
                TotalSum = 0,
                OrderList = null,
                OrderStatuses = new List<OrderStatus>() { new OrderStatus() { DateChange = DateTime.Now, Status = "moderation" } }
            };
            AddedOrders.Add(newOrder);
            Orders.Add(newOrder);

            Views.AddBusy.SetBusy(false);
        }

        private BaseCommand editItem;
        public BaseCommand EditItem
        {
            get
            {
                if (editItem == null)
                    editItem = new BaseCommand(Edit, OnCanDelete);

                return editItem;
            }
        }

        private bool OnCanEdit(object editObject)
        {
            return true;
        }

        private async void Edit(object editObject)
        {
            Views.EditBusy.SetBusy(true);
            await Task.Delay(5000);
            Views.EditBusy.SetBusy(false);
        }
    }
}
