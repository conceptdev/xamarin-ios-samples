namespace SafariViewDemoFS

open System
open CoreGraphics
open SafariServices
open Foundation
open UIKit

[<Register ("ViewController")>]
type ViewController () as x =
    inherit UIViewController () 

    let showSafari () =
        let sfvc = new SFSafariViewController (new NSUrl("http://xamarin.com"), true)
        x.PresentViewController (sfvc, true, null)

    // still trying to figure this bit out...
    [<Export("safariViewControllerDidFinish:")>]
    let DidFinish (controller: SFSafariViewController) =
        x.DismissViewController (true, null)

    interface ISFSafariViewControllerDelegate 

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        // Perform any additional setup after loading the view, typically from a nib.
        let b = UIButton.FromType(UIButtonType.System)
        b.Frame <- new CGRect (nfloat 10.0f, nfloat 40.0f, nfloat 150.0f, nfloat 40.0f) 
        b.SetTitle("Safari View Controller", UIControlState.Normal)
        b.TouchUpInside.Add (fun _ -> showSafari())
        x.View.Add b

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true
    