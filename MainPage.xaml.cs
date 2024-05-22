namespace shell_issues;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		var shell = Shell.Current;
		try
		{
			await shell.GoToAsync("//main/Foo/Bar");
		}
		catch (Exception ex)
		{ 
			// Add breakpoint here => not hit
			Console.WriteLine(ex.Message);
		}
		finally
		{
			// Add breakpoint here => not hit
			Console.WriteLine("Finally");
		}
	}
}

