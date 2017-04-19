using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace CrossfitApp
{
	public class AddNewPRViewModel : ViewModelBase, IViewModel
	{
		#region Properties
		private readonly INavigationService _navigationService;
		private readonly IDataService _databaseService;

		public PersonalRecord PersonalRecord { get; set; }
		public List<ExerciseTypeEnum> ExersiceTypes { get; set; }

		private static IDataService DataService { get; } = DependencyService.Get<IDataService>();
		#endregion

		#region Commands
		public ICommand SaveNewPRCommand { get; set; }
		#endregion

		public IMediaPicker MediaPicker { get; set; }

		/*
			Types
				Weight
					RM
					Kgs
				Reps 
					Reps
				Distance 
					Meters
				Time
					Time
		*/

		public AddNewPRViewModel(INavigationService navigationService, IDataService databaseService)
		{
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
			_navigationService = navigationService;

			if (databaseService == null) throw new ArgumentNullException(nameof(databaseService));
			_databaseService = databaseService;

			Init();

			MediaPicker = DependencyService.Get<IMediaPicker>();
			SaveNewPRCommand = new RelayCommand(() => SaveNewPR(PersonalRecord));
		}

		public void SaveNewPR(PersonalRecord newPR)
		{
			_databaseService.AddPersonalRecord(newPR);
			var viewModel = App.Locator.PROverview;
			viewModel.PersonalRecord.Add(newPR);
			_navigationService.GoBack();
		}



		#region ImagePicker

		public ImageSource ImageSource
		{
			get;
			set;
		}

		private void Init()
		{
			ExersiceTypes = Enum.GetValues(typeof(ExerciseTypeEnum)).Cast<ExerciseTypeEnum>().ToList();
		}

		private void Setup()
		{
			if (MediaPicker != null)
			{
				return;
			}

			var device = Resolver.Resolve<IDevice>();

			////RM: hack for working on windows phone? 
			MediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
		}

		private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

		private async Task TakePicture()
		{
			Setup();

			ImageSource = null;

			await this.MediaPicker.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
			 {
				 if (t.IsFaulted)
				 {
					 var s = t.Exception.InnerException.ToString();
				 }
				 else if (t.IsCanceled)
				 {
					 var canceled = true;
				 }
				 else
				 {
					 var mediaFile = t.Result;

					 ImageSource = ImageSource.FromStream(() => mediaFile.Source);

					 return mediaFile;
				 }

				 return null;
			 }, _scheduler);
		}

		#endregion
	}
}
