using CommunityToolkit.Maui.Core;

namespace FindExpert.UI.MVVM.Models.Shared;

internal record ToastMessage(string Text, ToastDuration Duration = ToastDuration.Short, double FontSize = 14);