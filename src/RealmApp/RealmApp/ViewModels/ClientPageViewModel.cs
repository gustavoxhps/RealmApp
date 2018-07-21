using Prism.Commands;
using Prism.Navigation;
using RealmApp.Infrastructure.Services;
using RealmApp.Models.ViewModels;
using System;

namespace RealmApp.ViewModels
{
	public class ClientPageViewModel : ViewModelBase
	{
		readonly IClientService _clientService;

		private ClientVM _client;
		public ClientVM Client { get { return _client; } set { SetProperty(ref _client, value); } }

		public ClientPageViewModel(INavigationService navigationService, IClientService clientService)
			: base(navigationService)
		{
			Title = "Adicionar Cliente";
			_clientService = clientService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);
			if (parameters["item"] is ClientVM client)
			{
				Client = client;
			}
			else
			{
				Client = new ClientVM();
			}
		}

		public DelegateCommand _cancelCommand;
		public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(OnCancel));
		Action OnCancel => new Action(async () => await NavigationService.GoBackToRootAsync());

		public DelegateCommand _saveCommand;
		public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(OnSave));
		Action OnSave => new Action(async () =>
		{
			try
			{
				if (Client.Id > 0)
				{
					_clientService.Update(Client);
				}
				else
				{
					_clientService.Add(Client);
				}
				await App.Current.MainPage.DisplayAlert("Add", "Client Added", "OK");
				await NavigationService.GoBackAsync();
			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		});

		public DelegateCommand _deleteCommand;
		public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(OnDelete));
		Action OnDelete => new Action(async () =>
		{
			try
			{
				if (Client.Id > 0)
				{
					var result = await App.Current.MainPage.DisplayAlert("Confirm", "Tem certeza que deseja excluir este cliente?", "Yes", "No");
					if (result)
					{
						_clientService.Delete(Client.Id);
						await App.Current.MainPage.DisplayAlert("Add", "Client Deleted", "OK");
						await NavigationService.GoBackAsync();
					}
				}
				else
				{
					await App.Current.MainPage.DisplayAlert("Add", "Client not found", "OK");
				}

			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		});
	}
}
