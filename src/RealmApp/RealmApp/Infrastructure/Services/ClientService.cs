using RealmApp.Infrastructure.Repository;
using RealmApp.Models.Entity;
using RealmApp.Models.ViewModels;

namespace RealmApp.Infrastructure.Services
{
	public class ClientService : BaseServices<Client, ClientVM>, IClientService
	{
		public ClientService(IBaseRepository baseRepository)
			: base(baseRepository)
		{
		}
	}
}
