namespace SafariViewDemoFS

open System
open UIKit
open Foundation

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit UIApplicationDelegate ()

    let mutable window = null

    // This method is invoked when the application is ready to run.
    override this.FinishedLaunching (app, options) =
        window <- new UIWindow (UIScreen.MainScreen.Bounds)
        window.BackgroundColor <- UIColor.White
        window.RootViewController <- new ViewController()
        window.MakeKeyAndVisible ()
        true
