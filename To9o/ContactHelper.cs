using System;
using ContactsUI;
using Contacts;
using Foundation;
using UIKit;

namespace StoryboardTables
{
	public class ContactHelper 
	{
		TodoItem current;
		public void SetSelectedName (string text) { 
			current.For = text;
		}
		public ContactHelper(TodoItem todo) {
			current = todo;
		}

		public CNContactPickerViewController GetPicker (){
			var picker = new CNContactPickerViewController();

			picker.DisplayedPropertyKeys = new NSString[] {CNContactKey.GivenName, CNContactKey.FamilyName};
			picker.PredicateForEnablingContact = NSPredicate.FromFormat("emailAddresses.@count > 0");

			// Respond to selection
			picker.Delegate = new ContactPickerDelegate(this);
			return picker;
		}
	}

	/// <summary>
	/// http://developer.xamarin.com/guides/ios/platform_features/introduction_to_ios9/contacts/#contactsui
	/// </summary>
	public class ContactPickerDelegate: CNContactPickerDelegate
	{
		ContactHelper helper;
		#region Constructors
		public ContactPickerDelegate (ContactHelper helper)
		{
			this.helper = helper;
		}

		public ContactPickerDelegate (IntPtr handle) : base (handle)
		{
		}
		#endregion

		#region Override Methods
		public override void ContactPickerDidCancel (CNContactPickerViewController picker)
		{
			Console.WriteLine ("User canceled picker");
		}

		public override void DidSelectContact (CNContactPickerViewController picker, CNContact contact)
		{
			Console.WriteLine ("Selected: {0}", contact);
			helper.SetSelectedName ($"{contact.GivenName} {contact.FamilyName}");
			Console.WriteLine ($"Selected {contact.GivenName} {contact.FamilyName}");
		}

		public override void DidSelectContactProperty (CNContactPickerViewController picker, CNContactProperty contactProperty)
		{
			Console.WriteLine ("Selected Property: {0}", contactProperty);
		}
		#endregion
	}
}

