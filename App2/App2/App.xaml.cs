using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2.Helpers;
using App2.Services;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace App2
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            bool useMockData = true;

            if (useMockData)
            {
                ServiceLocator.Instance.Add<IService, MockService>();
            }
            else
            {
                ServiceLocator.Instance.Add<IService, AzureService>();
            }

//            MainPage = new MenuPage();
            MainPage = new MainPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
