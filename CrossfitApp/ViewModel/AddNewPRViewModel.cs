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
		#region Members
		private readonly INavigationService _navigationService;
		private readonly IDataService _databaseService;
		private bool _isMetersVisible;
		private bool _isRMVisible;
		private bool _isKgsVisible;
		private bool _isRepsVisible;
		private bool _isTimeVisible;

		public PersonalRecord PersonalRecord { get; set; }
		public List<ExerciseTypeEnum> ExersiceTypes { get; set; }
		public ExerciseTypeEnum CurExercise { get; set; }

		private static IDataService DataService { get; } = DependencyService.Get<IDataService>();
		#endregion

		#region Properties
		public bool IsMetersVisible
		{
			get
			{
				return _isMetersVisible;
			}
			set
			{
				_isMetersVisible = value;
				RaisePropertyChanged("IsMetersVisible");

			}
		}

		public bool IsRMVisible
		{
			get
			{
				return _isRMVisible;
			}
			set
			{
				_isRMVisible = value;
				RaisePropertyChanged("IsRMVisible");

			}
		}

		public bool IsKgsVisible
		{
			get
			{
				return _isKgsVisible;
			}
			set
			{
				_isKgsVisible = value;
				RaisePropertyChanged("IsKgsVisible");

			}
		}

		public bool IsRepsVisible
		{
			get
			{
				return _isRepsVisible;
			}
			set
			{
				_isRepsVisible = value;
				RaisePropertyChanged("IsRepsVisible");

			}
		}

		public bool IsTimeVisible
		{
			get
			{
				return _isTimeVisible;
			}
			set
			{
				_isTimeVisible = value;
				RaisePropertyChanged("IsTimeVisible");

			}
		}
		#endregion

		#region Commands
		public ICommand SaveNewPRCommand { get; set; }
		public ICommand SelectedItemCommand { get; set; }
		#endregion

		public IMediaPicker MediaPicker { get; set; }

		public AddNewPRViewModel(INavigationService navigationService, IDataService databaseService)
		{
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
			_navigationService = navigationService;

			if (databaseService == null) throw new ArgumentNullException(nameof(databaseService));
			_databaseService = databaseService;

			MediaPicker = DependencyService.Get<IMediaPicker>();
			SaveNewPRCommand = new RelayCommand(() => SaveNewPR(PersonalRecord));
			SelectedItemCommand = new RelayCommand(SetNewItems);

			Init();
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

		public void ResetFields()
		{
			CurExercise = 0;
			IsRMVisible = false;
			IsKgsVisible = false;
			IsMetersVisible = false;
			IsRepsVisible = false;
			IsTimeVisible = false;
		}

		private void Init()
		{
			ExersiceTypes = Enum.GetValues(typeof(ExerciseTypeEnum)).Cast<ExerciseTypeEnum>().ToList();
		}

		public void SetNewItems()
		{
			switch (CurExercise)
			{
				case ExerciseTypeEnum.Weight:
					IsRMVisible = true;
					IsKgsVisible = true;
					IsMetersVisible = false;
					IsRepsVisible = false;
					IsTimeVisible = false;
					break;
				case ExerciseTypeEnum.Time:
					IsRMVisible = false;
					IsKgsVisible = false;
					IsMetersVisible = false;
					IsRepsVisible = false;
					IsTimeVisible = true;
					break;
				case ExerciseTypeEnum.Reps:
					IsRMVisible = false;
					IsKgsVisible = false;
					IsMetersVisible = false;
					IsRepsVisible = true;
					break;
				case ExerciseTypeEnum.Distance:
					IsRMVisible = false;
					IsKgsVisible = false;
					IsMetersVisible = true;
					IsRepsVisible = false;
					break;
			}

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
