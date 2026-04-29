using System.Collections.ObjectModel;
using System.Windows.Input;
using Core;
using Core.Enums;
using Core.Mappings;
using Core.Services;
using UI.Commands;
using UI.Models;
using UI.Services;

namespace UI.ViewModels;

using System.Collections.ObjectModel;
using System.Windows.Input;

public class MainViewModel
{
    private readonly IDialogService _dialog;
    private readonly IPolicyStore _store;

    public ObservableCollection<PolicyViewModel> Websites { get; } = new();
    public ObservableCollection<PolicyViewModel> Applications { get; } = new();

    public ICommand AddWebsiteCommand { get; }
    public ICommand RemoveWebsiteCommand { get; }

    public ICommand AddAppCommand { get; }
    public ICommand RemoveAppCommand { get; }

    public MainViewModel(IDialogService dialog, IPolicyStore store)
    {
        _dialog = dialog;
        _store = store;

        AddWebsiteCommand = new RelayCommand(AddWebsite);
        RemoveWebsiteCommand = new RelayCommand(RemoveWebsite);

        AddAppCommand = new RelayCommand(AddApp);
        RemoveAppCommand = new RelayCommand(RemoveApp);
    }

    // =========================
    // ADD WEBSITE
    // =========================
    private async void AddWebsite(object obj)
    {
        var result = _dialog.ShowInputDialog("Adicionar site");

        if (string.IsNullOrWhiteSpace(result))
            return;

        if (Websites.Any(x => x.Target == result))
            return;

        Websites.Add(new PolicyViewModel
        {
            Target = result,
            Type = PolicyType.Website
        });

        await SaveAsync();
    }

    // =========================
    // REMOVE WEBSITE
    // =========================
    private async void RemoveWebsite(object obj)
    {
        if (obj is PolicyViewModel item)
        {
            Websites.Remove(item);
            await SaveAsync();
        }
    }

    // =========================
    // ADD APP
    // =========================
    private async void AddApp(object obj)
    {
        var result = _dialog.ShowInputDialog("Adicionar aplicativo");

        if (string.IsNullOrWhiteSpace(result))
            return;

        if (Applications.Any(x => x.Target == result))
            return;

        Applications.Add(new PolicyViewModel
        {
            Target = result,
            Type = PolicyType.Application
        });

        await SaveAsync();
    }

    // =========================
    // REMOVE APP
    // =========================
    private async void RemoveApp(object obj)
    {
        if (obj is PolicyViewModel item)
        {
            Applications.Remove(item);
            await SaveAsync();
        }
    }

    // =========================
    // SAVE JSON
    // =========================
    private async Task SaveAsync()
    {
        var websiteTargets = Websites.Select(x => x.Target);
        var appTargets = Applications.Select(x => x.Target);

        var policies = PolicyMapper.Map(websiteTargets, appTargets);

        await _store.SaveAsync(policies);
    }
}