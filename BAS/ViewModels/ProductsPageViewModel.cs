using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAS.ProductService;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml;
using Syncfusion.UI.Xaml.Grid;
using System.Threading;
using Syncfusion.UI.Xaml.Utility;
using System.Collections;
using System.Diagnostics;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;

namespace BAS.ViewModels
{
    class ProductsPageViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

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

        private List<Product> DeletedProducts;
        private List<Product> AddedProducts;
        private List<Product> EditedProducts;

        public ProductsPageViewModel()
        {
            Products = new ObservableCollection<Product>();
            DeletedProducts = new List<Product>();
            AddedProducts = new List<Product>();
            EditedProducts = new List<Product>();

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

            ProductsServiceClient productsClient = new ProductsServiceClient();

            var productTask = productsClient.GetFromToAsync((uint)(Products.LastOrDefault()?.Articulus??0) + 1, (uint)(Products.LastOrDefault()?.Articulus ?? 0) + (uint)CountUploadElemets);

            foreach (var product in await productTask)
            {
                Products.Add(product);
            }
            await productsClient.CloseAsync();

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

            IEnumerable<Product> deletingOrders = listOfDeletingObjects.OfType<Product>();

            DeletedProducts.AddRange(deletingOrders);

            foreach (var deletingOrder in deletingOrders.ToList())
                Products.Remove(Products.SingleOrDefault(item => item.Articulus == deletingOrder.Articulus));

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
            Views.AddBusy.SetBusy(true);
            await Task.Delay(5000);
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

    public class ColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var input = (value as Product).Quantity;

            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;

            int minCountProducts = (int)localSettings.Values["minProductsCount"];
            int warningThreshold = (int)localSettings.Values["warningThreshold"];

            if (input > minCountProducts + warningThreshold)
                return new SolidColorBrush(Colors.LightGreen);

            else if (input <= minCountProducts + warningThreshold && input > minCountProducts)
                return new SolidColorBrush(Colors.Yellow);

            else if (input <= minCountProducts)
                return new SolidColorBrush(Colors.Red);

            else
                return DependencyProperty.UnsetValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
