using System;
using System.Collections.Generic;
using System.Diagnostics;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace CrossfitApp
{
	public partial class AddNewPRPage : ContentPage
	{
		public AddNewPRPage()
		{
			InitializeComponent();
			//var viewModel = App.Locator.AddNewPR;
			//viewModel.PersonalRecord = new PersonalRecord();
			BindingContext = App.Locator.AddNewPR;
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
