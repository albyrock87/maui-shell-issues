using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace shell_issues;

public partial class SampleSingleton
{
    public static SampleSingleton Instance { get; } = new SampleSingleton();
    
    private SampleSingleton()
    {
        
    }
    
    // Commenting `[RelayCommand]` below and uncommenting the following two lines
    // avoids the memory leak and demonstrates that the issue is related to the `RelayCommand` attribute
    // private ICommand? _dumbCommand;
    // public ICommand DumbActionCommand => _dumbCommand ??= new Command(DumbAction);
    
    [RelayCommand]
    public void DumbAction()
    {
        
    }
}

public partial class BarPage : ContentPage
{
    public BarPage()
    {
        InitializeComponent();
        BindingContext = SampleSingleton.Instance;
        MainPage.WeakRef = new WeakReference<object>(TheButton);
    }
}