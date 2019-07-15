using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;

namespace Todo11App
{
    /*
        remember RegisterForPreviewingWithDelegate() in ViewDidLoad
    */
    public partial class TableViewController : UITableViewController, IUIViewControllerPreviewingDelegate
    {

        public UIViewController GetViewControllerForPreview(IUIViewControllerPreviewing previewingContext, CGPoint location)
        {
            // Obtain the index path and the cell that was pressed.
            var indexPath = TableView.IndexPathForRowAtPoint(location);

            Console.WriteLine("ForPreview " + location.ToString() + " " + indexPath);

            if (indexPath == null)
                return null;

            var cell = TableView.CellAt(indexPath);

            if (cell == null)
                return null;
            
            // Create a detail view controller and set its properties.
            var peekViewController = (PeekViewController)Storyboard.InstantiateViewController("peekvc");
            if (peekViewController == null)
                return null;

            var peekAt = todoItems[indexPath.Row];
            peekViewController.SetTodo(peekAt);
            peekViewController.PreferredContentSize = new CGSize(0, 160);

            previewingContext.SourceRect = cell.Frame;

            return peekViewController;
        }

        public void CommitViewController(IUIViewControllerPreviewing previewingContext, UIViewController viewControllerToCommit)
        {
            Console.WriteLine("CommitViewContoller");

            var sv = (UITableView)previewingContext.SourceView;

            var x = previewingContext.SourceRect.X + (previewingContext.SourceRect.Width / 2);
            var y = previewingContext.SourceRect.Y + (previewingContext.SourceRect.Height / 2);

            var indexPath = TableView.IndexPathForRowAtPoint(new CGPoint(x, y));
            var popAt = todoItems[indexPath.Row];


            var detailViewController = (DetailViewController)Storyboard.InstantiateViewController("detailvc");
            if (detailViewController == null)
                return;

            detailViewController.SetTodo(popAt);

            ShowViewController(detailViewController, this);
        }
    }
}