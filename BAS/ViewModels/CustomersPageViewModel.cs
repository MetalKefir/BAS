using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BAS.CustomerService;
using Syncfusion.UI.Xaml.Utility;
using System.Collections;

namespace BAS.ViewModels
{
    class CustomersPageViewModel
    {
        public ObservableCollection<Customer> Customers { get; set; }

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

        private List<Customer> DeletedCustomers;
        private List<Customer> AddedCustomers;
        private List<Customer> EditedCustomers;

        public CustomersPageViewModel()
        {
            Customers = new ObservableCollection<Customer>();
            DeletedCustomers = new List<Customer>();
            AddedCustomers = new List<Customer>();
            EditedCustomers = new List<Customer>();

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

            CustomersServiceClient customersClient = new CustomersServiceClient();

            var customerTask = customersClient.GetFromToAsync((uint)(Customers.LastOrDefault()?.Id??0) + 1, (uint)(Customers.LastOrDefault()?.Id??0) + (uint)CountUploadElemets);

            var loadCustomers = await customerTask;

            await customersClient.CloseAsync();

            foreach (var customer in loadCustomers)
            {
                Customers.Add(customer);
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

            IEnumerable<Customer> deletingOrders = listOfDeletingObjects.OfType<Customer>();

            DeletedCustomers.AddRange(deletingOrders);

            foreach (var deletingOrder in deletingOrders.ToList())
                Customers.Remove(Customers.SingleOrDefault(item => item.Id == deletingOrder.Id));

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

        private bool OnCanAdd(object deletingObjects)
        {
            return true;
        }

        private async void Add(object deletingObjects)
        {
            Views.AddForm.SetShow(true);

            //Customer newCustomer = new Customer();

            //Customers.Add(newCustomer);
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

        private bool OnCanEdit(object deletingObjects)
        {
            return true;
        }

        private async void Edit(object deletingObjects)
        {
            Views.EditBusy.SetBusy(true);
            await Task.Delay(5000);
            Views.EditBusy.SetBusy(false);
        }
    }
}
