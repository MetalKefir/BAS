using Windows.UI.Xaml;
using System.Threading.Tasks;
using BAS.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;

namespace BAS
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        public App()
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                   Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["minProductsCount"] = localSettings?.Values["minProductsCount"]??10;
            localSettings.Values["countUploadElements"] = localSettings?.Values["countUploadElements"]??50;
            localSettings.Values["warningThreshold"] = localSettings?.Values["warningThreshold"]??20;

            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region app settings
           
            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.LoadBusy(),
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.OrdersPage));
        }
    }
}
