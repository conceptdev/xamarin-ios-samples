TodoToday (iOS 8 Extension)
========

Xamarin.iOS Unified API (64-bit) example that implements an iOS 8 Today Extension.

![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-samples/master/TodoToday/Screenshots/today-screen-sml.png "Today extension")

SQLite database access is done with ADO.NET to avoid the need for a Unified version of any external components.

There are also Xamarin [docs for extensions](http://developer.xamarin.com/guides/ios/platform_features/introduction_to_extensions/).

App Groups
----------
The extension communicates with the main app via an **App Group**, which is shared area of storage on the phone. This requires some configuration in the Apple Developer Portal (App Id, App Group, Provisioning Profile).