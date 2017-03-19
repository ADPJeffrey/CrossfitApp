using System;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace CrossfitApp
{
	public class ViewBase<T> : ContentPage where T : IViewModel, new()
	{
		readonly T _viewModel;

		public T ViewModel
		{
			get
			{
				return _viewModel;
			}
		}

		public ViewBase()
		{
			_viewModel = ServiceLocator.Current.GetInstance<T>();
			BindingContext = _viewModel;
		}
	}
}
