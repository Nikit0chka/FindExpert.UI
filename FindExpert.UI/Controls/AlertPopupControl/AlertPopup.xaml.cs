using CommunityToolkit.Maui.Views;
using FindExpert.UI.MVVM.Models.Shared;

namespace FindExpert.UI.Controls.AlertPopupControl;

public partial class AlertPopup:Popup
{
    public AlertPopup(AlertMessage message)
    {
        InitializeComponent();
        BindingContext = message;
    }

    private void OnDismissClicked(object sender, EventArgs e) { Close(); }
}