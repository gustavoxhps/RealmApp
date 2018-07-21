using AutoMapper;
using RealmApp.Models.Entity;
using RealmApp.Models.ViewModels;

namespace RealmApp.Infrastructure.Infra
{
	internal class AutoMapperConfig
	{
		internal static void Initialize()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<ClientVM, Client>();
				cfg.CreateMap<Client, ClientVM>();
			});
		}
	}
}
