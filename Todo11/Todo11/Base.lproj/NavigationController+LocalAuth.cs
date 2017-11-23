using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Intents;

namespace Todo11App
{
    public partial class NavigationController
    {
        /// <summary>
        /// Central storage for whether the user has been authenticated
        /// </summary>
        public bool Authenticated { get; set; } = false;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (!Authenticated)
            {
                Authenticate();
            }
        }
        public void Authenticate()
        {
            var popover = Storyboard.InstantiateViewController("localauth");
            (popover as LocalAuthViewController).Nav = this;

            PresentViewController(popover, true, null);

            // Configure the popover for the iPad, the popover displays as a modal view on the iPhone
            UIPopoverPresentationController presentationPopover = popover.PopoverPresentationController;
            if (presentationPopover != null)
            {
                presentationPopover.SourceView = this.View;
                presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                presentationPopover.SourceRect = View.Frame;
            }
        }
    }
}
