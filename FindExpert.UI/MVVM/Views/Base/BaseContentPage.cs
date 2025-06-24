using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.MVVM.Models.Shared;

namespace FindExpert.UI.MVVM.Views.Base;

internal class BaseContentPage:ContentPage
{
    protected BaseContentPage()
    {
        WeakReferenceMessenger.Default.Register<ToastMessage>(this,
                                                              static async void (_, m) =>
                                                              {
                                                                  var toast = Toast.Make(m.Text, m.Duration, m.FontSize);
                                                                  await toast.Show();

                                                              });
    }

    protected override void OnDisappearing()
    {
        WeakReferenceMessenger.Default.Unregister<ToastMessage>(this);
        base.OnDisappearing();
    }
}