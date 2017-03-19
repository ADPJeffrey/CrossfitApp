using System;
using CrossfitApp.Base;
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
			var prOverviewPage = new NavigationPage(new PROverviewPage());

			Locator.nav.Initialize(prOverviewPage);

			// The root page of your application
			MainPage = prOverviewPage;
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
