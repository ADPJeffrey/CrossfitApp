using System;
using Xamarin.Forms;

namespace CrossfitApp
{
	public partial class App : Application
	{
		private readonly static Locator _locator = new Locator();

		public static Locator Locator
		{
			get { return _locator; }
		}

		public App()
		{
			var firstPage = new NavigationPage(new MainPage());

			Locator.nav.Initialize(firstPage);

			// The root page of your application
			MainPage = firstPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
