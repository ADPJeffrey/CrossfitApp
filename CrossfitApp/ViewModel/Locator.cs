using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace CrossfitApp
{
	public class Locator
	{
		public const string MainPage = "MainPage";
		public const string AddNewPRPage = "DetailPage";

		public NavigationService nav { get; set; }
		/// <summary>
		/// Register all the used ViewModels, Services et. al. witht the IoC Container
		/// </summary>
		public Locator()
		{
		 	nav = new NavigationService();
			nav.Configure(Locator.MainPage, typeof(MainPage));
			nav.Configure(Locator.AddNewPRPage, typeof(DetailPage));

			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			// ViewModels
			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<DetailViewModel>();

			// Services
			SimpleIoc.Default.Register<IPeopleService, PeopleServiceStub>();
			SimpleIoc.Default.Register<INavigationService>(() => nav);
		}

		/// <summary>
		/// Gets the Main property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public MainViewModel Main
		{
			get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
		}

		/// <summary>
		/// Gets the Detail property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public DetailViewModel Detail
		{
			get { return ServiceLocator.Current.GetInstance<DetailViewModel>(); }
		}
	}
}
