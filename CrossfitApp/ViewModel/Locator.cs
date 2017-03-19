using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace CrossfitApp
{
	public class Locator
	{
		public const string PROverviewPage = "PROverviewPage";
		public const string AddNewPRPage = "AddNewPRPage";

		public NavigationService nav { get; set; }
		/// <summary>
		/// Register all the used ViewModels, Services et. al. witht the IoC Container
		/// </summary>
		public Locator()
		{
			nav = new NavigationService();
			nav.Configure(Locator.PROverviewPage, typeof(PROverviewPage));
			nav.Configure(Locator.AddNewPRPage, typeof(AddNewPRPage));

			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			// ViewModels
			SimpleIoc.Default.Register<PROverviewViewModel>();
			SimpleIoc.Default.Register<AddNewPRViewModel>();

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
		public PROverviewViewModel PROverview
		{
			get { return ServiceLocator.Current.GetInstance<PROverviewViewModel>(); }
		}

		/// <summary>
		/// Gets the Detail property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public AddNewPRViewModel AddNewPR
		{
			get { return ServiceLocator.Current.GetInstance<AddNewPRViewModel>(); }

		}
	}
}
