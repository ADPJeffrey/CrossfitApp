using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace CrossfitApp
{
	public class PROverviewViewModel : ViewModelBase, IViewModel
	{
		#region Properties
		private readonly INavigationService _navigationService;

		public ObservableCollection<IPersonalRecord> PersonalRecord { get; private set; }
		#endregion

		#region Commands
		public ICommand NavigateToAddNewPRCommand { get; set; }
		#endregion

		public PROverviewViewModel(INavigationService navigationService)
		{
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
			_navigationService = navigationService;

			NavigateToAddNewPRCommand = new RelayCommand(() => NavigateToPRPage());

			Task.Run(() => Init());
		}

		public async Task Init()
		{
			if (PersonalRecord != null) return;

			PersonalRecord = new ObservableCollection<IPersonalRecord>();
			//RaisePropertyChanged(() => People);
		}

		public void NavigateToPRPage()
		{
			var prVM = App.Locator.AddNewPR;
			prVM.PersonalRecord = new PersonalRecord();
			_navigationService.NavigateTo(Locator.AddNewPRPage);
		}
	}
}
