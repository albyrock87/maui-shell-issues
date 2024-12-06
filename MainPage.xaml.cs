using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace shell_issues;

public partial class MainPage : ContentPage
{
	public static WeakReference<object>? WeakRef;

	public MainPage()
	{
		InitializeComponent();
		MonitorReferenceAsync();
	}

	private async void MonitorReferenceAsync()
	{
		while (true)
		{
			await Task.Delay(1000);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			await Task.Yield();
			object? obj = null;
			var isAlive = WeakRef?.TryGetTarget(out obj) == true;
			ReferenceLbl.Text = isAlive
				? $"Reference is alive: {obj}"
				: "No reference is alive";

			if (obj is Element element)
			{
				// Try to disconnect the handler to see if that solves the issue
				// Still, I think that the platform view should hold a weak reference to the command(connector)
				element.Handler?.DisconnectHandler();
			}
		}
	}

	private void OnClicked(object? sender, EventArgs e)
	{
		Shell.Current.GoToAsync("Bar");
	}
}

