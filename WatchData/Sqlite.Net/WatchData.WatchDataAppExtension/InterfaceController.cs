using System;

using WatchKit;
using Foundation;
using System.IO;
using System.Collections.Generic;
using SQLite;

namespace WatchData.WatchDataAppExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		List<Stock> data = new List<Stock>();

		protected InterfaceController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void Awake(NSObject context)
		{
			base.Awake(context);

			// Configure interface objects here.
			Console.WriteLine("{0} awake with context", this);

			// https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/

			//HACK: watchOS not starting up SQLite.NET-PCL :-(
			//SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_internal())
			//SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3);
			//SQLitePCL.Batteries.Init();

			string dbPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.Personal),
				"ormdemo.db3");
			var db = new SQLiteConnection(dbPath);
			db.CreateTable<Stock>();
			if (db.Table<Stock>().Count() == 0)
			{
				// only insert the data if it doesn't already exist
				var newStock = new Stock();
				newStock.Id = "1";
				newStock.Name = "Apple";
				db.Insert(newStock);
				newStock = new Stock();
				newStock.Id = "2";
				newStock.Name = "Google";
				db.Insert(newStock);
				newStock = new Stock();
				newStock.Id = "3";
				newStock.Name = "Microsoft";
				db.Insert(newStock);
			}

			Console.WriteLine("Reading data");

			var table = db.Table<Stock>();
			foreach (var s in table)
			{
				Console.WriteLine(s.Id + " " + s.Name);
				data.Add(s);
			}




		}

		public override void WillActivate()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine("{0} will activate", this);

			StockTable.SetNumberOfRows((nint)data.Count, "stockRow");
			for (var i = 0; i < data.Count; i++)
			{
				 var elementRow = (StockRowController)StockTable.GetRowController(i);
				 elementRow.Set(data[i].Name);
			}
		}

		public override void DidDeactivate()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine("{0} did deactivate", this);
		}
	}
}
