using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace CrossfitApp
{
	public class PROverviewViewModel : ViewModelBase, IViewModel
	{
		#region Properties
		private readonly INavigationService _navigationService;
		private readonly IDataService _databaseService;

		public ObservableCollection<IPersonalRecord> PersonalRecord { get; private set; }

		private static IDataService DataService { get; } = DependencyService.Get<IDataService>();
		#endregion

		#region Commands
		public ICommand NavigateToAddNewPRCommand { get; set; }
		#endregion

		public PROverviewViewModel(INavigationService navigationService, IDataService databaseService)
		{
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
			_navigationService = navigationService;

			if (databaseService == null) throw new ArgumentNullException(nameof(databaseService));
			_databaseService = databaseService;

			NavigateToAddNewPRCommand = new RelayCommand(() => NavigateToPRPage());

			Task.Run(() => Init());
		}

		public async Task Init()
		{
			if (PersonalRecord != null) return;

			PersonalRecord = new ObservableCollection<IPersonalRecord>(_databaseService.GetPersonalRecords());
		}

		public void NavigateToPRPage()
		{
			var prVM = App.Locator.AddNewPR;
			prVM.PersonalRecord = new PersonalRecord();
			_navigationService.NavigateTo(Locator.AddNewPRPage);
		}
	}
}
