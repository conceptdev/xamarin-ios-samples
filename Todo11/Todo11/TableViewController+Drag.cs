using System;
using Foundation;
using MobileCoreServices;
using UIKit;

namespace Todo11App
{
    /// <summary>
    /// Implements the delegate methods for providing data for a drag interaction
    /// </summary>
    public partial class TableViewController : IUITableViewDragDelegate
    {
        /// <summary>
        /// Required for drag operations from a table
        /// </summary>
        public UIDragItem[] GetItemsForBeginningDragSession(UITableView tableView, IUIDragSession session, NSIndexPath indexPath)
        {
            var todo = todoItems[indexPath.Row];
            var stringToDrop = todo.Name;
            if (todo.Done)
                stringToDrop += " [" + NSBundle.MainBundle.LocalizedString("Done", "") + "]";

            var data = NSData.FromString(stringToDrop, NSStringEncoding.UTF8);

            var itemProvider = new NSItemProvider();
            itemProvider.RegisterDataRepresentation(UTType.PlainText,
                                                    NSItemProviderRepresentationVisibility.All,
                                                    (completion) =>
                                                    {
                                                        completion(data, null);
                                                        return null;
                                                    });

            return new UIDragItem[] { new UIDragItem(itemProvider) };
        }
    }
}
