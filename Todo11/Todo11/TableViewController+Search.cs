using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Todo11App
{
    /*
    remember to configure in ViewDidLoad
    */
    public partial class TableViewController: IUISearchResultsUpdating
    {

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            var find = searchController.SearchBar.Text;
            if (!String.IsNullOrEmpty(find))
            {
                todoItems = todoItems.Where(t => t.Name.ToLower().Contains(find.ToLower())).Select(p => p).ToList();
            }
            else
            {
                todoItems = AppDelegate.Current.TodoMgr.GetOrderedTodos().ToList(); 
            }
            TableView.ReloadData();
        }
    }
}
