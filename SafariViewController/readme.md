SafariViewController (iOS 9)
====================

It's relatively simple to get the new iOS 9 `SFSafariViewController` working with Xamarin.

```
using SafariServices;
using Foundation;
```

```
openButton.TouchUpInside += (sender, e) => {
	var sfvc = new SFSafariViewController (new NSUrl("http://xamarin.com"),true);
	PresentViewController(sfvc, true, null);
};
```

and

```
[Foundation.Export ("safariViewControllerDidFinish:")]
public void DidFinish (SFSafariViewController controller)
{
	DismissViewController (true, null);
}
```

![](Screenshots/safariviewcontroller.png)

