using System;
using Foundation;
using MapKit;
using UIKit;

namespace Todo11App
{
    [Register("TodoView")]
	public class TodoView : MKMarkerAnnotationView
	{
        public static UIColor NotDoneColor = UIColor.FromRGB(0xE2, 0x55, 0x3D); //F8C6BB light red; E2553D darkish red
        public static UIColor DoneColor = UIColor.FromRGB(0x6F, 0xA2, 0x2E); // CFEFA7 light-green; 6FA22E darkish green
	
		public override IMKAnnotation Annotation
		{
			get
			{
				return base.Annotation;
			}
			set
			{
				base.Annotation = value;

                var todo = value as TodoAnnotation;
				if (todo != null)
                {
					ClusteringIdentifier = "todo";
					switch(todo.Type)
                    {
						case MarkerType.NotDone:
							MarkerTintColor = NotDoneColor;
							GlyphImage = UIImage.FromBundle("box");
							DisplayPriority = MKFeatureDisplayPriority.DefaultLow;
							break;
						case MarkerType.Done:
							MarkerTintColor = DoneColor;
							GlyphImage = UIImage.FromBundle("checkbox");
							DisplayPriority = MKFeatureDisplayPriority.DefaultHigh;
							break;
					}
				}
			}
		}

		public TodoView()
		{
		}

		public TodoView(NSCoder coder) : base(coder)
		{
		}

		public TodoView(IntPtr handle) : base(handle)
		{
		}

        public TodoView(IMKAnnotation annotation, string identifier) : base(annotation, identifier)
        {

        }

	}
}
