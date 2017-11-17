using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using MapKit;
using CoreLocation;

namespace Todo11App
{
	[Register("BikeView")]
	public class BikeView : MKMarkerAnnotationView
	{
        public static UIColor NotDoneColor = UIColor.FromRGB(254, 122, 36);
		public static UIColor DoneColor = UIColor.FromRGB(153, 180, 44);
	
		public override IMKAnnotation Annotation
		{
			get
			{
				return base.Annotation;
			}
			set
			{
				base.Annotation = value;

				var bike = value as TodoAnnotation;
				if (bike != null){
					ClusteringIdentifier = "bike";
					switch(bike.Type){
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

		public BikeView()
		{
		}

		public BikeView(NSCoder coder) : base(coder)
		{
		}

		public BikeView(IntPtr handle) : base(handle)
		{
		}

        public BikeView(IMKAnnotation annotation, string identifier) : base(annotation, identifier)
        {

        }

	}
}
