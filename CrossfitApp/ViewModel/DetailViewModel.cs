using System;
using System.Collections.Generic;
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
	public class DetailViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;

		public PersonalRecord PersonalRecord { get; set; }
		public ExerciseTypeEnum CurExersice { get; set; }
		public IMediaPicker MediaPicker { get; set; }

		public DetailViewModel(INavigationService navigationService)
		{
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
			_navigationService = navigationService;

			ClickMeCallBackAction = () => { };
			ClickMeCommand = new RelayCommand(() => ClickMeCallBackAction());

			PersonalRecord = new PersonalRecord();
			PersonalRecord.ExerciseType = Enum.GetNames(typeof(ExerciseTypeEnum));
			PersonalRecord.Date = DateTime.Now;
			//PersonalRecord.ExerciseType = new List<string>
			//{
			//	"New York",
			//	"Boston",
			//	"Chicago"
			//};

			MediaPicker = DependencyService.Get<IMediaPicker>();
			SaveNewPRCommand = new RelayCommand(() => SaveNewPR());
		}

		public void SaveNewPR()
		{
			
		}

		public ICommand SaveNewPRCommand { get; set; }

		public ICommand ClickMeCommand { get; set; }
		public Person Person { get; set; }

		public Action ClickMeCallBackAction { get; set; }

		#region ImagePicker

		public ImageSource ImageSource
		{
			get;
			set;
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
