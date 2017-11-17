using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;

namespace Todo11App
{
    public partial class TableViewController : UITableViewController
    {
        public static TableViewController Current;
		/// <summary>List of items</summary>
        List<TodoItem> todoItems = new List<TodoItem>();
        UIButton MapButton;

        public TableViewController (IntPtr handle) : base (handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Things To Do", ""); 
            Current = this;
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            AddButton.Clicked += (sender, e) => {
                CreateTodo();
            };


            MapButton = UIButton.FromType(UIButtonType.Custom);
            MapButton.SetTitle("Map", UIControlState.Normal);
            MapButton.BackgroundColor = UIColor.Green;
            //mapButton.Bounds = new CGRect(0, 0, 50, 50);
            MapButton.SizeToFit();
            MapButton.TouchUpInside += (sender, e) => {
                Console.WriteLine("Show map");
            };
            NavigationController.View.AddSubview(MapButton);

            //var safeGuide = NavigationController.View.SafeAreaLayoutGuide;
            //var marginGuide = NavigationController.View.LayoutMarginsGuide;
            ////mapButton.Bounds = new CGRect(0,0 , 50, 50);

            //MapButton.LeftAnchor.ConstraintEqualTo(safeGuide.LeftAnchor, 10).Active = true;
            //MapButton.WidthAnchor.ConstraintEqualTo(100).Active = true;
            //MapButton.HeightAnchor.ConstraintEqualTo(50).Active = true;
            //safeGuide.BottomAnchor.ConstraintEqualTo(MapButton.BottomAnchor, -10).Active = true;
            //mapButton.LeadingAnchor.ConstraintEqualTo(safeGuide.LeadingAnchor).Active = true;
            //mapButton.TrailingAnchor.ConstraintEqualTo(safeGuide.TrailingAnchor).Active = true;
            //mapButton.BottomAnchor.ConstraintEqualTo(safeGuide.BottomAnchor).Active = true;



            // Impelement delegate and datasource for tableview to operate
            TableView.DataSource = this;
            TableView.Delegate = this;

			// for Drag and Drop
			TableView.DragDelegate = this;
			TableView.DropDelegate = this;

            // for 3DTouch
            if (TraitCollection.ForceTouchCapability == UIForceTouchCapability.Available)
            {
                RegisterForPreviewingWithDelegate(this, TableView);
            }

            // for Search
            var search = new UISearchController(searchResultsController: null)
            {
                DimsBackgroundDuringPresentation = false
            };
            search.SearchResultsUpdater = this;
            DefinesPresentationContext = true;
            NavigationItem.SearchController = search;
		}
        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();
            MapButton.Layer.CornerRadius = MapButton.Layer.Frame.Size.Width / 2;
            MapButton.BackgroundColor = UIColor.FromRGB(0x5A, 0x86, 0x22); // 5A8622 dark-green
            MapButton.ClipsToBounds = true;
            //MapButton.setImage(UIImage(named: "your-image"), for: .normal)
            MapButton.TranslatesAutoresizingMaskIntoConstraints = false;

            var safeGuide = NavigationController.View.SafeAreaLayoutGuide;
            NSLayoutConstraint.ActivateConstraints( new NSLayoutConstraint[] {
                MapButton.TrailingAnchor.ConstraintEqualTo(safeGuide.TrailingAnchor, -23),
                MapButton.BottomAnchor.ConstraintEqualTo(safeGuide.BottomAnchor, -13),
                MapButton.WidthAnchor.ConstraintEqualTo(60),
                MapButton.HeightAnchor.ConstraintEqualTo(60)
            });
        }
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

            if ((NavigationController as NavigationController).Authenticated)
            {
                todoItems = AppDelegate.Current.TodoMgr.GetOrderedTodos().ToList(); 

                // bind every time, to reflect deletion in the Detail view
                TableView.ReloadData();
            }
            else
            {
                Console.WriteLine("Should not be showing stuff (ie when app is first run)");
            }
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
        public void InsertTodo(TodoItem todo, int at)
        {
            todoItems.Insert(0, todo);
            AppDelegate.Current.TodoMgr.SaveTodo(todo);
            SpotlightHelper.Index(todo);
            MoveTodo(todo, 0, at);
        }
        public void ToggleTodoDone(TodoItem todo)
        {
            todo.Done = !todo.Done;
            AppDelegate.Current.TodoMgr.SaveTodo(todo);
            SpotlightHelper.Index(todo);
        }
		public void SaveTodo(TodoItem todo)
		{
			AppDelegate.Current.TodoMgr.SaveTodo(todo);
			SpotlightHelper.Index(todo);

		}
        public void MoveTodo (TodoItem todo, int from, int to)
        {
            todoItems.RemoveAt(from);
            todoItems.Insert(to, todo);
            AppDelegate.Current.TodoMgr.Reorder(todoItems); // HACK: brute force
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