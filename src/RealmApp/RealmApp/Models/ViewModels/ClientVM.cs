using RealmApp.Models.Common;

namespace RealmApp.Models.ViewModels
{
	public class ClientVM : BaseEntity
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Department { get; set; }
	}
}
