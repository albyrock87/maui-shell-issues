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