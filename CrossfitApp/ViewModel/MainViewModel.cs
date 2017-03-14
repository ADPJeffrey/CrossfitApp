using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace CrossfitApp
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IPeopleService _peopleService;
		private readonly INavigationService _navigationService;

		public ObservableCollection<IPerson> People { get; private set; }

		public ICommand NavigateToAddNewPRCommand { get; set; }

		public MainViewModel(IPeopleService peopleService, INavigationService navigationService)
		{
			if (peopleService == null) throw new ArgumentNullException(nameof(peopleService));
				_peopleService = peopleService;

			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
				_navigationService = navigationService;

			NavigateToAddNewPRCommand = new RelayCommand(() => _navigationService.NavigateTo(Locator.AddNewPRPage, new Person("Alex", "Paradeis")));

			Task.Run(() => Init());
		}

		//TODO create new Constructor for pr paramter

		public async Task Init()
		{
			if (People != null) return;

			People = new ObservableCollection<IPerson>(await _peopleService.GetPeople());
			RaisePropertyChanged(() => People);
		}
	}
}
