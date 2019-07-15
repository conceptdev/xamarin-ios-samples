﻿using System.Collections.Generic;
using CoreLocation;
using Foundation;
using MapKit;

namespace Todo11App
{
    public enum MarkerType
    {
        NotDone,
        Done
    }

	public class TodoAnnotation : MKPointAnnotation
	{
        public TodoAnnotation()
        {
        }

        public MarkerType Type { get; set; } = MarkerType.Done;

		public TodoAnnotation(double lat, double lng, bool done)
		{
			// Initialize
            Coordinate = new CLLocationCoordinate2D(new NSNumber(lat).NFloatValue, new NSNumber(lng).NFloatValue);

            if (done)
                Type = MarkerType.Done;
            else
                Type = MarkerType.NotDone;
		}

        // Helper method
        public static TodoAnnotation[] FromList(List<TodoItem> todoList)
        {
            var todos = new List<TodoAnnotation>();

            for (int n = 0; n < todoList.Count; ++n)
            {
                todos.Add(new TodoAnnotation(todoList[n].Latitude, todoList[n].Longitude, todoList[n].Done));
            }

            return todos.ToArray();
        }
	}
}
