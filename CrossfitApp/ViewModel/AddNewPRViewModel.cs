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
	public class AddNewPRViewModel : ViewModelBase
	{
		#region Properties
		private readonly INavigationService _navigationService;

		public PersonalRecord PersonalRecord { get; set; }
		#endregion

		#region Commands
		public ICommand SaveNewPRCommand { get; set; }
		#endregion

		public IMediaPicker MediaPicker { get; set; }

		public AddNewPRViewModel(INavigationService navigationService)
		{
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
			_navigationService = navigationService;

			MediaPicker = DependencyService.Get<IMediaPicker>();
			SaveNewPRCommand = new RelayCommand(() => SaveNewPR(PersonalRecord));
		}

		public void SaveNewPR(PersonalRecord newPR)
		{
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
