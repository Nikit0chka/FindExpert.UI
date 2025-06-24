namespace FindExpert.UI.MVVM.Models.Shared;

public class AlertMessage
{
    public string Title { get; }
    public string Message { get; }
    public string ButtonText { get; }
    public Color BackgroundColor { get; }
    public Color TextColor { get; }

    public AlertMessage(
        string title,
        string message,
        string buttonText = "OK",
        Color? backgroundColor = null,
        Color? textColor = null)
    {
        Title = title;
        Message = message;
        ButtonText = buttonText;
        BackgroundColor = backgroundColor ?? Colors.Purple;
        TextColor = textColor ?? Colors.White;
    }
}