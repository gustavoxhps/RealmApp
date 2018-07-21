using Prism.Commands;
using Prism.Navigation;
using RealmApp.Helpers;
using RealmApp.Infrastructure.Services;
using RealmApp.Models.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RealmApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		readonly IClientService _clientService;

		private ObservableCollection<Grouping<string, ClientVM>> _clients;
		public ObservableCollection<Grouping<string, ClientVM>> Clients { get => _clients; set => SetProperty(ref _clients, value); }

		public MainPageViewModel(INavigationService navigationService, IClientService clientService)
			: base(navigationService)
		{
			Title = "Main Page";

			_clientService = clientService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			LoadClients();
		}

		private void LoadClients()
		{
			var clients = _clientService.All();
			//clients.Where(o => string.IsNullOrWhiteSpace(o.Department)).Select(o => { o.Department = "No dept"; return o; }).ToList();
			var group = clients?.ToList()?
				.GroupBy(o => o.Department)
				.Select(o => new Grouping<string, ClientVM>(o.Key, o));
			Clients = new ObservableCollection<Grouping<string, ClientVM>>(group);
		}

		public DelegateCommand<ClientVM> _itemTappedCommand;
		public DelegateCommand<ClientVM> ItemTappedCommand =>
			_itemTappedCommand ??
			(_itemTappedCommand = new DelegateCommand<ClientVM>(OnItemTapped));

		Action<ClientVM> OnItemTapped => new Action<ClientVM>(async (selected) =>
		{
			var parameter = new NavigationParameters
			{
				{ "item", selected }
			};
			await NavigateToAsync("ClientPage", parameter);
		});

		public DelegateCommand _addCommand;
		public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(OnAdd));
		Action OnAdd => new Action(async () =>
		{
			await NavigateToAsync("ClientPage");
		});

	}
}
