using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Todo11App
{
    public partial class TableViewController: IUISearchResultsUpdating
    {
        // called in ViewDidLod
        void ConfigureSearch()
        {
            var search = new UISearchController(searchResultsController: null)
            {
                DimsBackgroundDuringPresentation = false
            };
            search.SearchResultsUpdater = this; // implemented the interface above
            DefinesPresentationContext = true;  // IMPORTANT: ensures segue works
            NavigationItem.SearchController = search;
        }

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
