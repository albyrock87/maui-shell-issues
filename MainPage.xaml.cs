using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Controls;
using DevExpress.Maui.Core;

namespace shell_issues;

class MyPopup : DXPopup
{
	public MyPopup()
	{
		AllowScrim = true;
		CloseOnScrimTap = true;
		ScrimColor = Colors.DarkSlateGray;

		var verticalStackLayout = new VerticalStackLayout
		{
			Padding = 24
		};
		verticalStackLayout.Add(new Label { Text = "Super!" });
		verticalStackLayout.Add(new Label { Text = "Super duper popup!" });
		verticalStackLayout.Add(new HorizontalStackLayout
		{
			new DXButton { Content = "A button" },
			new DXButton { Content = "A second button" },
		});
		Content = verticalStackLayout;
	}
}

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

	private void DXButtonBase_OnClicked(object? sender, EventArgs e)
	{
		Shell.Current.GoToAsync("Bar");
	}
}

