using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace BAS.ViewModels
{
    class SettingsPageViewModel : ViewModelBase
    {
        private int minCountProducts;
        public int MinCountProducts
        {
            get { return minCountProducts; }
            set
            {
                Set(ref minCountProducts, value);
                SaveSetting.RaiseCanExecuteChanged();
            }
        }

        private int countUploadElements;
        public int CountUploadElemets
        {
            get { return countUploadElements; }
            set
            {
                Set(ref countUploadElements, value);
                SaveSettingUpload.RaiseCanExecuteChanged();
            }
        }

        private int warningThreshold;
        public int WarningThreshold
        {
            get { return warningThreshold; }
            set
            {
                Set(ref warningThreshold, value);
                SaveSettingwarning.RaiseCanExecuteChanged();
            }
        }

        DelegateCommand saveSetting;
        public DelegateCommand SaveSetting
            => saveSetting ?? (saveSetting = new DelegateCommand(async () =>
            {
                Windows.Storage.ApplicationDataContainer localSettings =
                    Windows.Storage.ApplicationData.Current.LocalSettings;

                localSettings.Values["minProductsCount"] = MinCountProducts;

            }, () => !string.IsNullOrEmpty(Convert.ToString(MinCountProducts))));

        DelegateCommand saveSettingUpload;
        public DelegateCommand SaveSettingUpload
            => saveSettingUpload ?? (saveSettingUpload = new DelegateCommand(async () =>
            {
                Windows.Storage.ApplicationDataContainer localSettings =
                    Windows.Storage.ApplicationData.Current.LocalSettings;

                localSettings.Values["countUploadElements"] = CountUploadElemets;

            }, () => !string.IsNullOrEmpty(Convert.ToString(CountUploadElemets))));

        DelegateCommand saveSettingWarning;
        public DelegateCommand SaveSettingwarning
            => saveSettingWarning ?? (saveSettingWarning = new DelegateCommand(async () =>
            {
                Windows.Storage.ApplicationDataContainer localSettings =
                    Windows.Storage.ApplicationData.Current.LocalSettings;

                localSettings.Values["warningThreshold"] = WarningThreshold;

            }, () => !string.IsNullOrEmpty(Convert.ToString(WarningThreshold))));

        public SettingsPageViewModel()
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                    Windows.Storage.ApplicationData.Current.LocalSettings;

            minCountProducts = (int)localSettings.Values["minProductsCount"];
            countUploadElements = (int)localSettings.Values["countUploadElements"];
            warningThreshold = (int)localSettings.Values["warningThreshold"];
        }
    }
}
