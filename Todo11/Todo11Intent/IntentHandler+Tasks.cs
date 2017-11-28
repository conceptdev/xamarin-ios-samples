using System;
using Foundation;
using Intents;

// DEMO: 1. Info.plist
// DEMO: 2. Entitlements.plist
//          Privacy, NSUserActivityTypes, INAlternativeAppNames
// DEMO: 3. Provisioning

namespace Todo11Intent
{
    public partial class IntentHandler : IINAddTasksIntentHandling, IINCreateTaskListIntentHandling, IINSetTaskAttributeIntentHandling
    {
        /*
             https://developer.apple.com/documentation/sirikit/resolving_and_handling_intents/handling_an_intent
         */
        /// <summary>
        /// Handles the simple phrase of adding a task (no 'list' needs to be specified)
        /// </summary>
        /// <remarks>
        /// For example:
        /// "[app] add [task]"
        /// "Todo11 add Boeing factory tour"
        /// </remarks>
        public void HandleAddTasks(INAddTasksIntent intent, Action<INAddTasksIntentResponse> completion)
        {
            // 1. get the parsed data from Siri
            var item = intent.TaskTitles[0].ToString();

            // 2. have to create the tasks... in your app data store
            SaveToApp(item);

            // 3. after adding to the datastore, respond to the user with an answer/card
            var userActivity = new NSUserActivity("INAddTasksIntent"); // https://developer.apple.com/documentation/sirikit/resolving_and_handling_intents/confirming_the_details_of_an_intent
            var response = new INAddTasksIntentResponse(INAddTasksIntentResponseCode.Success, userActivity)
            {   // tells the user what we did
                AddedTasks = TaskList.FromIntent(intent).Tasks,
            };
            completion(response);
        }

        private static void SaveToApp(string firstTask)
        {
            Console.WriteLine($"'{firstTask}' was requested");

            var FileManager = new NSFileManager();
            var appGroupContainer = FileManager.GetContainerUrl("group.co.conceptdev.todo");
            if (appGroupContainer == null)
            {
                Console.WriteLine("APP GROUP ERROR, probably bug #59379");
            }
            else
            {
                var appGroupContainerPath = appGroupContainer.Path;
                Console.WriteLine("agcpath: " + appGroupContainerPath);
                var path = System.IO.Path.Combine(appGroupContainerPath, "siri.txt");
                Console.WriteLine("path: " + path);
                System.IO.File.WriteAllText(path, firstTask);
            }
        }

        /// <summary>
        /// Handles the create task list.
        /// </summary>
        /// <remarks>
        /// "Make a grocery list with apples, bananas, and pears in Todo11"
        /// </remarks>
        public void HandleCreateTaskList(INCreateTaskListIntent intent, Action<INCreateTaskListIntentResponse> completion)
        {
            Console.WriteLine("Create a task list");
            var userActivity = new NSUserActivity("INCreateTaskListIntent");
            var list = TaskList.FromIntent(intent);
            // TODO: have to create the list and tasks... in your app data store
            var response = new INCreateTaskListIntentResponse(INCreateTaskListIntentResponseCode.Success, userActivity)
            {
                CreatedTaskList = list
            };
            completion(response);
        }


        /// <summary>
        /// Handles the set task attribute.
        /// </summary>
        /// <remarks>
        /// "Mark buy iPhone as completed in Todo11"
        /// </remarks>
        public void HandleSetTaskAttribute(INSetTaskAttributeIntent intent, Action<INSetTaskAttributeIntentResponse> completion)
        {
            Console.WriteLine("Set task attribute");
            var userActivity = new NSUserActivity("INSetTaskAttributeIntent");

            var task = Task.FromIntent(intent);
            // TODO: have to actually confirm the task exists and then change its status... in your app data store
            task.Status = intent.Status;

            var response = new INSetTaskAttributeIntentResponse(INSetTaskAttributeIntentResponseCode.Success, userActivity)
            {
                ModifiedTask = task.ForResponse()
            };
            completion(response);
        }
    }
}
