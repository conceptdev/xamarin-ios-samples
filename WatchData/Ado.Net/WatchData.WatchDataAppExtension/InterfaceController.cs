using System;

using WatchKit;
using Foundation;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

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

			// https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_4_using_adonet/

			string dbPath = Path.Combine(
 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
 "adodemo.db3");

			SqliteConnection connection;
			bool exists = File.Exists(dbPath);

			if (!exists)
			{
 Console.WriteLine("Creating database");
 // Need to create the database before seeding it with some data
 Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);
 connection = new SqliteConnection("Data Source=" + dbPath);

 var commands = new[] {
 	"CREATE TABLE [Items] (_id ntext, Symbol ntext);",
 	"INSERT INTO [Items] ([_id], [Symbol]) VALUES ('1', 'AAPL')",
 	"INSERT INTO [Items] ([_id], [Symbol]) VALUES ('2', 'GOOG')",
 	"INSERT INTO [Items] ([_id], [Symbol]) VALUES ('3', 'MSFT')"
 };
 // Open the database connection and create table with data
 connection.Open();
 foreach (var command in commands)
 {
 	using (var c = connection.CreateCommand())
 	{
 		c.CommandText = command;
 		var rowcount = c.ExecuteNonQuery();
 		Console.WriteLine("\tExecuted " + command);
 	}
 }
			}
			else {
 Console.WriteLine("Database already exists");
 // Open connection to existing database file
 connection = new SqliteConnection("Data Source=" + dbPath);
 connection.Open();
			}

			// query the database to prove data was inserted!
			using (var contents = connection.CreateCommand())
			{
 contents.CommandText = "SELECT [_id], [Symbol] from [Items]";
 var r = contents.ExecuteReader();
 Console.WriteLine("Reading data");
 while (r.Read())
 {
 	Console.WriteLine("\tKey={0}; Value={1}",
  	  r["_id"].ToString(),
  	  r["Symbol"].ToString());
 	data.Add(new Stock { Id = r["_id"].ToString(), Symbol = r["Symbol"].ToString() });
 }
			}

			connection.Close();
		}

		public override void WillActivate()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine("{0} will activate", this);

			StockTable.SetNumberOfRows((nint)data.Count, "stockRow");
			for (var i = 0; i < data.Count; i++)
			{
 var elementRow = (StockRowController)StockTable.GetRowController(i);
 elementRow.Set(data[i].Symbol);
			}
		}

		public override void DidDeactivate()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine("{0} did deactivate", this);
		}
	}
}
