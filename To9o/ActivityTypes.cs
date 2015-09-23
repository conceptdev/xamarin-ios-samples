using System;
using Foundation;

namespace StoryboardTables
{
	public static class ActivityTypes
	{
		public const string Add = "co.conceptdev.todo.activity.add";
		public const string Detail = "co.conceptdev.todo.activity.detail";
	}

	public static class ActivityKeys {
		public static NSString Id = new NSString ("id");
		public static NSString Name = new NSString ("name");
	}
}

