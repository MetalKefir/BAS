using Syncfusion.UI.Xaml.Grid.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Grid;
using System.Text.RegularExpressions;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace BAS.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            this.InitializeComponent();
        }

        private void dataGrid_AddNewRowInitiating(object sender, AddNewRowInitiatingEventArgs args)
        {
            var data = args.NewObject as CustomerService.Customer;
            data = new CustomerService.Customer()
            {
                Id = null,
                CustomerAddress = null,
                PhoneNumber = null,
                Email = null,
                FName = null,
                LName = null,
                MName= null,
                Age= 0
            };

        }

        private void dataGrid_RowValidating(object sender, RowValidatingEventArgs args)
        {
            if (this.dataGrid.IsAddNewIndex(args.RowIndex))
            {
                var data = args.RowData as CustomerService.Customer;

                if (data.FName == null  ||  data.FName.Length == 0)
                {
                    args.IsValid = false;
                    args.ErrorMessages.Add("FName", "Имя должно быть заполнена");
                }

                if (data.LName == null || data.LName.Length == 0)
                {
                    args.IsValid = false;
                    args.ErrorMessages.Add("LName", "Фамилия должна быть заполнена");
                }

                if (data.CustomerAddress == null || data.CustomerAddress.Length == 0)
                {
                    args.IsValid = false;
                    args.ErrorMessages.Add("CustomerAddress", "Адрес должен быть заполнен");
                }

                if (data.Email == null || data.Email.Length == 0)
                {
                    args.IsValid = false;
                    args.ErrorMessages.Add("Email", "Почта неверна");
                }

                if (data.PhoneNumber == null || data.PhoneNumber.Length == 0)
                {
                    args.IsValid = false;
                    args.ErrorMessages.Add("PhoneNumber", "Номер неверен");
                }

                if (data.Age < 0 || data.Age >= 100)
                {
                    args.IsValid = false;
                    args.ErrorMessages.Add("Age", "Возраст не может быть меньше 0 или больше 100");
                }
            }
        }
    }
}
