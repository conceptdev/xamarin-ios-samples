using System;
using System.IO;
using System.Collections.Generic;
using UIKit;
using SQLite;

namespace WatchData
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		List<Stock> data = new List<Stock>();

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.


			string dbPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.Personal),
				"ormdemo.db3");
			var db = new SQLiteConnection(dbPath);
			db.CreateTable<Stock>();
			if (db.Table<Stock>().Count() == 0)
			{
				// only insert the data if it doesn't already exist
				var newStock = new Stock();
				newStock.Name = "Apple";
				db.Insert(newStock);
				newStock = new Stock();
				newStock.Name = "Google";
				db.Insert(newStock);
				newStock = new Stock();
				newStock.Name = "Microsoft";
				db.Insert(newStock);
			}

			Console.WriteLine("Reading data");

			var table = db.Table<Stock>();
			foreach (var s in table)
			{
				Console.WriteLine("stock: " + s.Id + " " + s.Name);
				data.Add(s);
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
