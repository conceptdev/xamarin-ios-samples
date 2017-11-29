// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Todo11App
{
	[Register ("MapViewController")]
	partial class MapViewController
	{
		[Outlet]
		MapKit.MKMapView Map { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Map != null) {
				Map.Dispose ();
				Map = null;
			}
		}
	}
}
