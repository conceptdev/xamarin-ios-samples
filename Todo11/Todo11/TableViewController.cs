using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Todo11App
{
    public partial class TableViewController : UITableViewController
    {
        public static TableViewController Current;
		/// <summary>List of items</summary>
		List<TodoItem> todoItems;

        public TableViewController (IntPtr handle) : base (handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Things To Do", ""); 
            Current = this;
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Specify the table as its own drag source and drop delegate
			this.TableView.DragDelegate = this;
			this.TableView.DropDelegate = this;

			// Impelement delegate and datasource for tableview to operate
			TableView.DataSource = this;
			TableView.Delegate = this;

            AddButton.Clicked += (sender, e) => {
                CreateTodo ();
            };
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			todoItems = AppDelegate.Current.TodoMgr.GetOrderedTodos().ToList(); //ordered for CollectionView

            // bind every time, to reflect deletion in the Detail view
            TableView.ReloadData();
		}

		/// <summary>
		/// Prepares for segue.
		/// </summary>
		/// <remarks>
		/// The prepareForSegue method is invoked whenever a segue is about to take place. 
		/// The new view controller has been loaded from the storyboard at this point but 
		/// it’s not visible yet, and we can use this opportunity to send data to it.
		/// </remarks>
		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "todosegue")
			{ // set in Storyboard
				var tvc = segue.DestinationViewController as DetailViewController;
				if (tvc != null)
				{
                    var source = TableView.DataSource as TableViewController;
                    var rowPath = TableView.IndexPathForCell(sender as UITableViewCell);
					var item = source.GetItem(rowPath.Row);
					tvc.Delegate = this;
					tvc.SetTodo(item);
				}
			}
		}

		public TodoItem GetItem(int id)
		{
			return todoItems[id];
		}


		#region CRUD
		public void CreateTodo()
		{
			// StackView
			var detail = Storyboard.InstantiateViewController("detailvc") as DetailViewController;
			detail.Delegate = this;
			detail.SetTodo(new TodoItem());
			NavigationController.PushViewController(detail, true);

			// Could to this instead of the above, but need to create 'new TodoItem()' in PrepareForSegue()
			//this.PerformSegue ("TodoSegue", this);
		}
		public void SaveTodo(TodoItem todo)
		{
			AppDelegate.Current.TodoMgr.SaveTodo(todo);
			SpotlightHelper.Index(todo);

		}
		public void DeleteTodo(TodoItem todo)
		{
			Console.WriteLine("Delete " + todo.Name);
			if (todo.Id >= 0)
			{
				AppDelegate.Current.TodoMgr.DeleteTodo(todo.Id);
				SpotlightHelper.Delete(todo);
			}
		}
		#endregion
    }
}