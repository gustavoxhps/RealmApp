using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealmApp.ViewModels
{
	public class ViewModelBase : BindableBase, INavigationAware, IDestructible
	{
		protected INavigationService NavigationService { get; private set; }

		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public ViewModelBase(INavigationService navigationService)
		{
			NavigationService = navigationService;
		}

		public virtual void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{

		}

		public virtual void OnNavigatingTo(NavigationParameters parameters)
		{

		}

		public virtual void Destroy()
		{

		}

		private bool _canNavigate = true;
		public bool CanNavigate { get => _canNavigate; set => SetProperty(ref _canNavigate, value); }

		protected async Task NavigateToAsync(string route, NavigationParameters parameters = null, bool? useModalNavigation = null)
		{
			if (CanNavigate)
			{
				CanNavigate = false;

				await NavigationService.NavigateAsync(route, parameters, useModalNavigation, false);

				await Task.Factory.StartNew(() =>
				{
					Task.Delay(100).Wait();
					CanNavigate = true;
				});
			}
		}

	}
}
