using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Template10.Common;
using Template10.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BAS.Views
{
    public sealed partial class DeleteBusy : UserControl
    {
        public DeleteBusy()
        {
            InitializeComponent();
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(DeleteBusy), new PropertyMetadata(false));

        // hide and show busy dialog
        public static void SetBusy(bool busy)
        {
            WindowWrapper.Current().Dispatcher.Dispatch(() =>
            {
                var modal = Window.Current.Content as ModalDialog;
                if (!(modal.ModalContent is DeleteBusy view))
                    modal.ModalContent = view = new DeleteBusy();
                modal.IsModal = view.IsBusy = busy;
            });
        }
    }
}
