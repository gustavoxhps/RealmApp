using Realms;

namespace RealmApp.Models.Common
{
	public interface IRealmEntity : IEntity
	{
		RealmInteger<int> Counter { get; set; }
	}
}
