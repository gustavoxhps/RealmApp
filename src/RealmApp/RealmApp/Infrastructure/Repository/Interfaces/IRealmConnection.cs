using Realms;

namespace RealmApp.Infrastructure.Repository
{
	public interface IRealmConnection
	{
		Realm Connection { get; }
		void Destroy();
	}
}
