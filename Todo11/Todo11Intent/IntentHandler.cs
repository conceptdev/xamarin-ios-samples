using System;
using System.Collections.Generic;

using Foundation;
using Intents;
using ObjCRuntime;

namespace To11oIntent
{
    // As an example, this class is set up to handle Message intents.
    // You will want to replace this or add other intents as appropriate.
    // The intents you wish to handle must be declared in the extension's Info.plist.

    // You can test your example integration by saying things to Siri like:
    // "Send a message using <myApp>"
    // "<myApp> John saying hello"
    // "Search for messages in <myApp>"
    [Register("IntentHandler")]
    public class IntentHandler : INExtension, IINNotebookDomainHandling
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


		public void HandleAddTasks(INAddTasksIntent intent, Action<INAddTasksIntentResponse> completion)
		{
			Console.WriteLine("Add a task");
			var userActivity = new NSUserActivity("INAddTasksIntent"); // https://developer.apple.com/documentation/sirikit/resolving_and_handling_intents/confirming_the_details_of_an_intent

			var title = "";
			if (intent.TargetTaskList != null)
			{
				Console.WriteLine(intent.TargetTaskList.Title);
				title = intent.TargetTaskList.Title?.ToString();
				foreach (var t in intent.TargetTaskList.Tasks)
				{
					Console.WriteLine("tasklist: " + t.Title + " " + t.Status);
					if (string.IsNullOrEmpty(title)) title = t.Title?.ToString();
				}
			}
			foreach (var t in intent.TaskTitles)
			{
				Console.WriteLine("tasktitle: " + t);
				if (string.IsNullOrEmpty(title)) title = t?.ToString();
			}
			if (intent.SpatialEventTrigger != null)
			{
				/*
                 spatialEventTrigger = <INSpatialEventTrigger: 0x1014cdeb0> {
        event = arrive;
        placemark = San Francisco, San Francisco, CA, United States @ <+37.77937220,-122.41842100> +/- 100.00m, region CLCircularRegion (identifier:'<+37.75882150,-122.44588850> radius 9379.49', center:<+37.75882150,-122.44588850>, radius:9379.49m);
    };
    temporalEventTrigger = <null>;
    targetTaskList = <null>;
    taskTitles = (
        WWDC,
    );
}}
}}*/
			}
			if (intent.TemporalEventTrigger != null)
			{
				/*temporalEventTrigger = <INTemporalEventTrigger: 0x1052aa0f0> {
        dateComponentsRange = <INDateComponentsRange: 0x1051a3800> {
            startDateComponents = <NSDateComponents: 0x1051a3e50>
                Calendar: <_NSCopyOnWriteCalendarWrapper: 0x1051a37a0>
                Calendar Year: 2017
                Month: 9
                Day: 6
                Hour: 17
                Minute: 0
                Second: 0
                Nanosecond: 0;
            endDateComponents = <NSDateComponents: 0x1051a3f00>
                Calendar: <_NSCopyOnWriteCalendarWrapper: 0x1051a37c0>
                Calendar Year: 2017
                Month: 9
                Day: 7
                Hour: 23
                Minute: 59
                Second: 0
                Nanosecond: 0
                Weekday: 5;
            recurrenceRule = <null>;
        };*/
			}
			userActivity.Title = "Received " + title;
			Console.WriteLine("Received " + title);
			var response = new INAddTasksIntentResponse(INAddTasksIntentResponseCode.Success, userActivity)
			{
				AddedTasks = new INTask[] { new INTask(new INSpeakableString(title), INTaskStatus.NotCompleted, INTaskType.Completable, null, null, null, null, "mytask") },
				ModifiedTaskList = new INTaskList(new INSpeakableString("list 1"),
					new INTask[] {
						new INTask(new INSpeakableString("task 1"), INTaskStatus.NotCompleted, INTaskType.Completable, null, null, null, null, "mytask"),
						new INTask(new INSpeakableString("task 2"), INTaskStatus.NotCompleted, INTaskType.Completable, null, null, null, null, "mytask")
				}, new INSpeakableString("group 3"), null, null, "mylist")

			};

			// handling
			//https://developer.apple.com/documentation/sirikit/resolving_and_handling_intents/handling_an_intent
			completion(response);
		}

        public void HandleAppendToNote(INAppendToNoteIntent intent, Action<INAppendToNoteIntentResponse> completion)
        {
            throw new NotImplementedException();
        }

        public void HandleCreateNote(INCreateNoteIntent intent, Action<INCreateNoteIntentResponse> completion)
        {
            throw new NotImplementedException();
        }

        public void HandleCreateTaskList(INCreateTaskListIntent intent, Action<INCreateTaskListIntentResponse> completion)
        {
            Console.WriteLine("Create a task list");
			var userActivity = new NSUserActivity("INCreateTaskListIntent");
            var response = new INCreateTaskListIntentResponse (INCreateTaskListIntentResponseCode.Success, userActivity);
			completion(response);
        }

        public void HandleSearchForNotebookItems(INSearchForNotebookItemsIntent intent, Action<INSearchForNotebookItemsIntentResponse> completion)
        {
            throw new NotImplementedException();
        }

        public void HandleSetTaskAttribute(INSetTaskAttributeIntent intent, Action<INSetTaskAttributeIntentResponse> completion)
        {
            Console.WriteLine("Set task attribute");
			var userActivity = new NSUserActivity("INSetTaskAttributeIntent");
            var response = new INSetTaskAttributeIntentResponse (INSetTaskAttributeIntentResponseCode.Success, userActivity);
			completion(response);
        }













        //// Implement resolution methods to provide additional information about your intent (optional).
        //[Export("resolveRecipientsForSearchForMessages:withCompletion:")]
        //public void ResolveRecipients(INSendMessageIntent intent, Action<INPersonResolutionResult[]> completion)
        //{
        //    var recipients = intent.Recipients;
        //    // If no recipients were provided we'll need to prompt for a value.
        //    if (recipients.Length == 0)
        //    {
        //        completion(new INPersonResolutionResult[] { INPersonResolutionResult.NeedsValue });
        //        return;
        //    }

        //    var resolutionResults = new List<INPersonResolutionResult>();

        //    foreach (var recipient in recipients)
        //    {
        //        var matchingContacts = new INPerson[] { recipient }; // Implement your contact matching logic here to create an array of matching contacts
        //        if (matchingContacts.Length > 1)
        //        {
        //            // We need Siri's help to ask user to pick one from the matches.
        //            resolutionResults.Add(INPersonResolutionResult.GetDisambiguation(matchingContacts));
        //        }
        //        else if (matchingContacts.Length == 1)
        //        {
        //            // We have exactly one matching contact
        //            resolutionResults.Add(INPersonResolutionResult.GetSuccess(recipient));
        //        }
        //        else
        //        {
        //            // We have no contacts matching the description provided
        //            resolutionResults.Add(INPersonResolutionResult.Unsupported);
        //        }
        //    }

        //    completion(resolutionResults.ToArray());
        //}

        //[Export("resolveContentForSendMessage:withCompletion:")]
        //public void ResolveContent(INSendMessageIntent intent, Action<INStringResolutionResult> completion)
        //{
        //    var text = intent.Content;
        //    if (!string.IsNullOrEmpty(text))
        //        completion(INStringResolutionResult.GetSuccess(text));
        //    else
        //        completion(INStringResolutionResult.NeedsValue);
        //}

        //// Once resolution is completed, perform validation on the intent and provide confirmation (optional).
        //[Export("confirmSendMessage:completion:")]
        //public void ConfirmSendMessage(INSendMessageIntent intent, Action<INSendMessageIntentResponse> completion)
        //{
        //    // Verify user is authenticated and your app is ready to send a message.

        //    var userActivity = new NSUserActivity("INSendMessageIntent");
        //    var response = new INSendMessageIntentResponse(INSendMessageIntentResponseCode.Ready, userActivity);
        //    completion(response);
        //}

        //// Handle the completed intent (required).
        //public void HandleSendMessage(INSendMessageIntent intent, Action<INSendMessageIntentResponse> completion)
        //{
        //    // Implement your application logic to send a message here.

        //    var userActivity = new NSUserActivity("INSendMessageIntent");
        //    var response = new INSendMessageIntentResponse(INSendMessageIntentResponseCode.Success, userActivity);
        //    completion(response);
        //}

        //// Implement handlers for each intent you wish to handle.
        //// As an example for messages, you may wish to add HandleSearchForMessages and HandleSetMessageAttribute.

        //public void HandleSearchForMessages(INSearchForMessagesIntent intent, Action<INSearchForMessagesIntentResponse> completion)
        //{
        //    // Implement your application logic to find a message that matches the information in the intent.

        //    var userActivity = new NSUserActivity("INSearchForMessagesIntent");
        //    var response = new INSearchForMessagesIntentResponse(INSearchForMessagesIntentResponseCode.Success, userActivity);

        //    // Initialize with found message's attributes
        //    var sender = new INPerson(new INPersonHandle("sarah@example.com", INPersonHandleType.EmailAddress), null, "Sarah", null, null, null);
        //    var recipient = new INPerson(new INPersonHandle("+1-415-555-5555", INPersonHandleType.PhoneNumber), null, "John", null, null, null);
        //    var message = new INMessage("identifier", "I am so excited about SiriKit!", NSDate.Now, sender, new INPerson[] { recipient });
        //    response.Messages = new INMessage[] { message };
        //    completion(response);
        //}

        //public void HandleSetMessageAttribute(INSetMessageAttributeIntent intent, Action<INSetMessageAttributeIntentResponse> completion)
        //{
        //    // Implement your application logic to set the message attribute here.

        //    var userActivity = new NSUserActivity("INSetMessageAttributeIntent");
        //    var response = new INSetMessageAttributeIntentResponse(INSetMessageAttributeIntentResponseCode.Success, userActivity);
        //    completion(response);
        //}
    }
}
