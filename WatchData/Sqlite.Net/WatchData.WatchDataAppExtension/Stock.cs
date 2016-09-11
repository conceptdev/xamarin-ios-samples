using System;
using SQLite;

namespace WatchData.WatchDataAppExtension
{
	public class Stock
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public string Id { get; set; }

		[MaxLength(8)]
		public string Symbol { get; set; }

		public string Name { get; set; }
	}
}
