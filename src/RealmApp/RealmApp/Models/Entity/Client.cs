﻿using System;
using RealmApp.Models.Common;
using Realms;

namespace RealmApp.Models.Entity
{
	public class Client : RealmObject, IRealmEntity
	{
		[Indexed]
		[PrimaryKey]
		public long Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Department { get; set; }

		public DateTimeOffset? LastSynchronizationDate { get; set; }
		public DateTimeOffset? SysLastChangeDate { get; set; }
		public DateTimeOffset? SysDeletedDate { get; set; }
		public RealmInteger<int> Counter { get; set; }
	}
}
