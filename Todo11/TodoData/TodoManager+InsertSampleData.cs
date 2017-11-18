using System;
using System.Collections.Generic;
using SQLite;

namespace Todo11App
{
    public partial class TodoManager
    {
        public void InsertSampleData ()
        {
            if (GetTodos().Count == 0)
            {
                SaveTodo(new TodoItem { Name = "Space Needle", Latitude = 47.620422, Longitude = -122.349358 });
                SaveTodo(new TodoItem { Name = "Seattle Aquarium", Latitude = 47.607445, Longitude = -122.3429996 });
                SaveTodo(new TodoItem { Name = "Seattle Great Wheel", Latitude = 47.6061064, Longitude = -122.3437136 });
                SaveTodo(new TodoItem { Name = "Monorail", Latitude = 47.6212364, Longitude = -122.3587656 });
                SaveTodo(new TodoItem { Name = "Chihuly", Latitude = 47.6212308, Longitude = -122.3849658 });
                SaveTodo(new TodoItem { Name = "Olypmic Sculpture Park", Latitude = 47.6165958, Longitude = -122.3574934 });
                SaveTodo(new TodoItem { Name = "Pacific Science Center", Latitude = 47.6197393, Longitude = -122.3532757 });
                SaveTodo(new TodoItem { Name = "Seattle Children's Museum", Latitude = 47.621394, Longitude = -122.3530673 });
                SaveTodo(new TodoItem { Name = "Seattle Repertory Theatre", Latitude = 47.6239152, Longitude = -122.3558693 });
                SaveTodo(new TodoItem { Name = "Key Arena", Latitude = 47.6221, Longitude = -122.3561713 });
                SaveTodo(new TodoItem { Name = "Memorial Stadium", Latitude = 47.6228051, Longitude = -122.3517047 });
                SaveTodo(new TodoItem { Name = "Museum of Pop Culture", Latitude = 47.6214824, Longitude = -122.3503078 });
                SaveTodo(new TodoItem { Name = "Pike Place Fish Market", Latitude = 47.6089551, Longitude = -122.3427362 });
                SaveTodo(new TodoItem { Name = "Pike Place Starbucks", Latitude = 47.6100596, Longitude = -122.3447316 });
                SaveTodo(new TodoItem { Name = "Seattle Art Museum", Latitude = 47.607309, Longitude = -122.3403164 });
                SaveTodo(new TodoItem { Name = "Sky View Observatory", Latitude = 47.6047141, Longitude = -122.3326368 });
                SaveTodo(new TodoItem { Name = "Smith Tower Observation Deck", Latitude = 47.6019201, Longitude = -122.3339053 });
                SaveTodo(new TodoItem { Name = "CenturyLink Field", Latitude = 47.5951518, Longitude = -122.3338152 });
                SaveTodo(new TodoItem { Name = "Safeco Field", Latitude = 47.5913908, Longitude = -122.3345107 });
                SaveTodo(new TodoItem { Name = "Washington State Convention Center", Latitude = 47.6117273, Longitude = -122.3350973 });
                SaveTodo(new TodoItem { Name = "Westlake Center", Latitude = 47.6120485, Longitude = -122.3396158 });
                SaveTodo(new TodoItem { Name = "Kerry Park", Latitude = 47.6294692, Longitude = -122.3621057 });
                SaveTodo(new TodoItem { Name = "Gas Works Park", Latitude = 47.6456308, Longitude = -122.3365365 });
                SaveTodo(new TodoItem { Name = "Pike Place Market", Latitude = 47.6097197, Longitude = -122.3509262 });
                SaveTodo(new TodoItem { Name = "Waterfront Park", Latitude = 47.60704, Longitude = -122.3440109 });
                SaveTodo(new TodoItem { Name = "Seattle Public Library", Latitude = 47.6067006, Longitude = -122.3346842 });
                SaveTodo(new TodoItem { Name = "Occidental Square", Latitude = 47.600506, Longitude = -122.3354069 });
                SaveTodo(new TodoItem { Name = "McCaw Hall", Latitude = 47.6239126, Longitude = -122.3519183 });
                SaveTodo(new TodoItem { Name = "IMAX", Latitude = 47.6197697, Longitude = -122.3527042 });
                //SaveTodo(new TodoItem { Name = "", Latitude = , Longitude =  });
                //SaveTodo(new TodoItem { Name = "", Latitude = , Longitude =  });
                //SaveTodo(new TodoItem { Name = "", Latitude = , Longitude =  });
                //SaveTodo(new TodoItem { Name = "", Latitude = , Longitude =  });
            }
        }
    }
}