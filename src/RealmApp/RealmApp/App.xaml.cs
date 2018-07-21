using Prism;
using Prism.Ioc;
using RealmApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using RealmApp.Infrastructure.Services;
using RealmApp.Infrastructure.Repository;
using RealmApp.Infrastructure.Infra;
using RealmApp.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RealmApp
{
	public partial class App : PrismApplication
	{
		public App() : this(null) { }

		public App(IPlatformInitializer initializer) : base(initializer)
		{
			LiveReload.Init();
		}

		protected override async void OnInitialized()
		{
			InitializeComponent();
			AutoMapperConfig.Initialize();

			await NavigationService.NavigateAsync("NavigationPage/MainPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
			containerRegistry.RegisterForNavigation<ClientPage, ClientPageViewModel>();

			containerRegistry.RegisterSingleton(typeof(IRealmConnection), typeof(RealmConnection));
			containerRegistry.RegisterSingleton(typeof(IBaseRepository), typeof(BaseRepository));

			containerRegistry.RegisterSingleton(typeof(IBaseServices<,>), typeof(BaseServices<,>));

			containerRegistry.RegisterSingleton(typeof(IClientService), typeof(ClientService));
		}
	}
}
