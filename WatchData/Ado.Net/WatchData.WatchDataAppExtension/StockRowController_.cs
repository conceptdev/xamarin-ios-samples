using Foundation;
using System;
using UIKit;

namespace WatchData.WatchDataAppExtension
{
    public partial class StockRowController : NSObject
    {
        public StockRowController (IntPtr handle) : base (handle)
        {
        }

		public void Set(string symbol)
		{
			Console.WriteLine("set: name=" + symbol);

			// I don't even...
			// WTF: http://stackoverflow.com/questions/28031832/how-can-i-reload-the-data-in-a-watchkit-tableview
			StockSymbol.SetText(@""); // this fixes the issue I was having :-\
			StockSymbol.SetText(symbol);
		}
    }
}