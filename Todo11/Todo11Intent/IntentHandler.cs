using System;
using System.Collections.Generic;

using Foundation;
using Intents;

namespace Todo11Intent
{
	// As an example, this class is set up to handle Message intents.
	// You will want to replace this or add other intents as appropriate.
	// The intents you wish to handle must be declared in the extension's Info.plist.

	// You can test your example integration by saying things to Siri like:
	// "Send a message using <myApp>"
	// "<myApp> John saying hello"
	// "Search for messages in <myApp>"
	[Register("IntentHandler")]
	public partial class IntentHandler : INExtension
	{
		protected IntentHandler(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override NSObject GetHandler(INIntent intent)
		{
			// This is the default implementation.  If you want different objects to handle different intents,
			// you can override this and return the handler you want for that particular intent.
			Console.WriteLine("get the intent handler");
			return this;
		}


//		public void HandleAddTasks(INAddTasksIntent intent, Action<INAddTasksIntentResponse> completion)
//		{
//			Console.WriteLine("Add a task");
//			var userActivity = new NSUserActivity("INAddTasksIntent"); // https://developer.apple.com/documentation/sirikit/resolving_and_handling_intents/confirming_the_details_of_an_intent

//			var title = "";
//			if (intent.TargetTaskList != null)
//			{
//				Console.WriteLine(intent.TargetTaskList.Title);
//				title = intent.TargetTaskList.Title?.ToString();
//				foreach (var t in intent.TargetTaskList.Tasks)
//				{
//					Console.WriteLine("tasklist: " + t.Title + " " + t.Status);
//					if (string.IsNullOrEmpty(title)) title = t.Title?.ToString();
//				}
//			}
//			foreach (var t in intent.TaskTitles)
//			{
//				Console.WriteLine("tasktitle: " + t);
//				if (string.IsNullOrEmpty(title)) title = t?.ToString();
//			}
//			if (intent.SpatialEventTrigger != null)
//			{
//				/*
//                 spatialEventTrigger = <INSpatialEventTrigger: 0x1014cdeb0> {
//        event = arrive;
//        placemark = San Francisco, San Francisco, CA, United States @ <+37.77937220,-122.41842100> +/- 100.00m, region CLCircularRegion (identifier:'<+37.75882150,-122.44588850> radius 9379.49', center:<+37.75882150,-122.44588850>, radius:9379.49m);
//    };
//    temporalEventTrigger = <null>;
//    targetTaskList = <null>;
//    taskTitles = (
//        WWDC,
//    );
//}}
//}}*/
		//	}
		//	if (intent.TemporalEventTrigger != null)
		//	{
		//		/*temporalEventTrigger = <INTemporalEventTrigger: 0x1052aa0f0> {
  //      dateComponentsRange = <INDateComponentsRange: 0x1051a3800> {
  //          startDateComponents = <NSDateComponents: 0x1051a3e50>
  //              Calendar: <_NSCopyOnWriteCalendarWrapper: 0x1051a37a0>
  //              Calendar Year: 2017
  //              Month: 9
  //              Day: 6
  //              Hour: 17
  //              Minute: 0
  //              Second: 0
  //              Nanosecond: 0;
  //          endDateComponents = <NSDateComponents: 0x1051a3f00>
  //              Calendar: <_NSCopyOnWriteCalendarWrapper: 0x1051a37c0>
  //              Calendar Year: 2017
  //              Month: 9
  //              Day: 7
  //              Hour: 23
  //              Minute: 59
  //              Second: 0
  //              Nanosecond: 0
  //              Weekday: 5;
  //          recurrenceRule = <null>;
  //      };*/
		//	}
		//	userActivity.Title = "Received " + title;
		//	Console.WriteLine("Received " + title);
		//	var response = new INAddTasksIntentResponse(INAddTasksIntentResponseCode.Success, userActivity)
		//	{
		//		AddedTasks = new INTask[] { new INTask(new INSpeakableString(title), INTaskStatus.NotCompleted, INTaskType.Completable, null, null, null, null, "mytask") },
		//		ModifiedTaskList = new INTaskList(new INSpeakableString("list 1"),
		//			new INTask[] {
		//				new INTask(new INSpeakableString("task 1"), INTaskStatus.NotCompleted, INTaskType.Completable, null, null, null, null, "mytask"),
		//				new INTask(new INSpeakableString("task 2"), INTaskStatus.NotCompleted, INTaskType.Completable, null, null, null, null, "mytask")
		//		}, new INSpeakableString("group 3"), null, null, "mylist")

		//	};

		//	// handling
		//	//https://developer.apple.com/documentation/sirikit/resolving_and_handling_intents/handling_an_intent
		//	completion(response);
		//}

		//public void HandleAppendToNote(INAppendToNoteIntent intent, Action<INAppendToNoteIntentResponse> completion)
		//{
		//	throw new NotImplementedException();
		//}

		//public void HandleCreateNote(INCreateNoteIntent intent, Action<INCreateNoteIntentResponse> completion)
		//{
		//	throw new NotImplementedException();
		//}

		//public void HandleCreateTaskList(INCreateTaskListIntent intent, Action<INCreateTaskListIntentResponse> completion)
		//{
		//	Console.WriteLine("Create a task list");
		//	var userActivity = new NSUserActivity("INCreateTaskListIntent");

		//	var tasks = new List<INTask>();
		//	if (intent.TaskTitles != null)
		//	{
		//		foreach (var t in intent.TaskTitles)
		//		{
  //                  var ta = new INTask(t, INTaskStatus.NotCompleted, INTaskType.Completable, null, null, null, null, "mytask");
  //                  tasks.Add(ta);
		//		}
		//	}

  //          var response = new INCreateTaskListIntentResponse(INCreateTaskListIntentResponseCode.Success, userActivity)
  //          {
  //              CreatedTaskList = new INTaskList(intent.Title, tasks.ToArray(), intent.GroupName, null, null, "mylist")
		//	};
		//	completion(response);
		//}

		//public void HandleSearchForNotebookItems(INSearchForNotebookItemsIntent intent, Action<INSearchForNotebookItemsIntentResponse> completion)
		//{
		//	throw new NotImplementedException();
		//}

		//public void HandleSetTaskAttribute(INSetTaskAttributeIntent intent, Action<INSetTaskAttributeIntentResponse> completion)
		//{
		//	Console.WriteLine("Set task attribute");
		//	var userActivity = new NSUserActivity("INSetTaskAttributeIntent");
		//	var response = new INSetTaskAttributeIntentResponse(INSetTaskAttributeIntentResponseCode.Success, userActivity);
		//	completion(response);
		//}
    }
}
