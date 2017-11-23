using System;
using CoreGraphics;
using Foundation;
using MapKit;
using UIKit;

namespace Todo11App
{
    [Register("ClusterView")]
	public class ClusterView : MKAnnotationView
	{
        public static UIColor ClusterColor = UIColor.White; //UIColor.FromRGB(202, 150, 38);
	
        public ClusterView(IMKAnnotation annotation, string reuseIdentifier) : base(annotation, reuseIdentifier)
        {
            // Initialize
            DisplayPriority = MKFeatureDisplayPriority.DefaultHigh;
            CollisionMode = MKAnnotationViewCollisionMode.Circle;

            // Offset center point to animate better with marker annotations
            CenterOffset = new CoreGraphics.CGPoint(0, -10);
        }

		public override IMKAnnotation Annotation
		{
			get
			{
				return base.Annotation;
			}
			set
			{
				base.Annotation = value;

				// TODO: Workaround, the developer should be able to use
				// `value as MKClusterAnnotation` instead of the following 
				// extension method call:
				var cluster = MKAnnotationWrapperExtensions.UnwrapClusterAnnotation(value);
				if (cluster != null)
				{
					var renderer = new UIGraphicsImageRenderer(new CGSize(40, 40));
					var count = cluster.MemberAnnotations.Length;
                    var notDoneCount = CountBikeType(cluster.MemberAnnotations, MarkerType.NotDone);

					Image = renderer.CreateImage((context) => {
						// Fill full circle with DONE color
						BikeView.DoneColor.SetFill();
						UIBezierPath.FromOval(new CGRect(0, 0, 40, 40)).Fill();

						// Fill pie with NOT DONE color
						BikeView.NotDoneColor.SetFill();
						var piePath = new UIBezierPath();
						piePath.AddArc(new CGPoint(20,20), 20, 0, (nfloat)(Math.PI * 2.0 * notDoneCount / count), true);
						piePath.AddLineTo(new CGPoint(20, 20));
						piePath.ClosePath();
						piePath.Fill();

						// Fill inner circle with color
                        ClusterColor.SetFill();
						UIBezierPath.FromOval(new CGRect(8, 8, 24, 24)).Fill();

						// Finally draw count text vertically and horizontally centered
						var attributes = new UIStringAttributes() {
							ForegroundColor = UIColor.Black,
							Font = UIFont.BoldSystemFontOfSize(20)
						};
						var text = new NSString($"{count}");
						var size = text.GetSizeUsingAttributes(attributes);
						var rect = new CGRect(20 - size.Width / 2, 20 - size.Height / 2, size.Width, size.Height);
						text.DrawString(rect, attributes);
					});
				}
			}
		}
		
		public ClusterView()
		{
		}

		public ClusterView(NSCoder coder) : base(coder)
		{
		}

		public ClusterView(IntPtr handle) : base(handle)
		{
		}

		
		
		private nuint CountBikeType(IMKAnnotation[] members, MarkerType type) {
			nuint count = 0;

			foreach(TodoAnnotation member in members){
				if (member.Type == type) ++count;
			}

			return count;
		}

	}
}
