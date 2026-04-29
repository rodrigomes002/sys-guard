using System.Windows;
using UI.ViewModels;

namespace UI.Views;

public partial class AddPolicyWindow : Window
{
    public string Result { get; private set; }

    public AddPolicyWindow()
    {
        InitializeComponent();
        DataContext = new AddPolicyViewModel();
    }

    private void OnSave(object sender, RoutedEventArgs e)
    {
        if (DataContext is AddPolicyViewModel vm)
        {
            Result = vm.Input;
            DialogResult = true;
        }
    }

    private void OnCancel(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}