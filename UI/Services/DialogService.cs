using UI.Views;

namespace UI.Services;

public class DialogService : IDialogService
{
    public string ShowInputDialog(string title)
    {
        var dialog = new AddPolicyWindow
        {
            Title = title
        };

        return dialog.ShowDialog() == true
            ? dialog.Result
            : null;
    }
}