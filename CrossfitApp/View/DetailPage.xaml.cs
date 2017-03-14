using System;
using System.Collections.Generic;
using System.Diagnostics;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace CrossfitApp
{
	public partial class DetailPage : ContentPage
	{
		public PersonalRecord PersonalRecord { get; set; }

		public DetailPage()
		{
			InitializeComponent();
			var viewModel = App.Locator.Detail;
		}

		public DetailPage(Person person)
		{
			InitializeComponent();
			var viewModel = App.Locator.Detail;
			viewModel.Person = person;

			BindingContext = viewModel;

			viewModel.ClickMeCallBackAction = () => DisplayAlert("Hello", viewModel.Person.FullName, "Ok");
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var currentPageKeyString = ServiceLocator.Current
				.GetInstance<INavigationService>()
				.CurrentPageKey;
			Debug.WriteLine("Current page key: " + currentPageKeyString);
		}
	}
}
