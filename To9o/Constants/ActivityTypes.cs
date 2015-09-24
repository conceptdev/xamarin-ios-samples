using System;
using Foundation;

namespace StoryboardTables
{
	/// <summary>
	/// Constants for the NSUserActivity
	/// </summary>
	public static class ActivityTypes
	{
		public const string Add = "co.conceptdev.todo.activity.add";
		public const string Detail = "co.conceptdev.todo.activity.detail";
	}

	/// <summary>
	/// Extension methods for NSUserActivity
	/// </summary>
	public static class ActivityKeys {
		public static NSString Id = new NSString ("id");
		public static NSString Name = new NSString ("name");


		public static bool IsIndexable (this Task current){
			return (current != null && current.Name != null && current.Notes != null);
		}
		public static NSDictionary NameToDictionary (this Task current){
			return NSDictionary.FromObjectAndKey (new NSString (current.Name), ActivityKeys.Name);
		}
		public static NSDictionary IdToDictionary (this Task current){
			return NSDictionary.FromObjectAndKey (new NSString (current.Id.ToString ()), ActivityKeys.Id);
		}
	}
}

