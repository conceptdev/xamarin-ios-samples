using System;
using Foundation;

namespace Todo11App
{
	/// <summary>
	/// Constants for the NSUserActivity, also embedded in Info.plist
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
		public static NSString Notes = new NSString ("notes");


		public static bool IsIndexable (this TodoItem current){
			return (current != null && current.Name != null && current.Notes != null);
		}
		public static NSDictionary NotesToDictionary (this TodoItem current){
			return NSDictionary.FromObjectAndKey (new NSString (current.Notes??""), ActivityKeys.Notes);
		}
		public static NSDictionary NameToDictionary (this TodoItem current){
			return NSDictionary.FromObjectAndKey (new NSString (current.Name??""), ActivityKeys.Name);
		}
		public static NSDictionary IdToDictionary (this TodoItem current){
			return NSDictionary.FromObjectAndKey (new NSString (current.Id.ToString ()), ActivityKeys.Id);
		}
	}
}

