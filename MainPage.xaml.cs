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
	private int _counter;

	public MainPage()
	{
		InitializeComponent();
		foreach (var view in (Layout)Content)
		{
			if (view is DXButton button)
			{
				button.Command = OpenAndWaitPopupCommand;
			}
		}
	}

	[RelayCommand]
	private async Task OpenAndWaitPopup()
	{
		if (Interlocked.Increment(ref _counter) % 5 != 0)
		{
			return;
		}
			
		var p = new MyPopup();
		var tcs = new TaskCompletionSource();
		p.ClosingAnimationCompleted += (o, args) =>
		{
			Console.WriteLine("ClosingAnimationCompleted");
			tcs.TrySetResult();
		};

		Console.WriteLine("Opening");
		p.Show();

		await tcs.Task;

		Console.WriteLine("Complete");
	}
}

