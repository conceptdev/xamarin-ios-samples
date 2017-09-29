using System;
using SceneKit;
using UIKit;
using ARKit;
using CoreGraphics;

namespace ARKitEarth
{
	public partial class GameViewController : UIViewController, IARSCNViewDelegate
	{
		#region Computed Properties
		public ARSCNView SceneView {
			get { return View as ARSCNView; }
		}

		public float AmbientIntensity {
			get {
				// Get the current frame
				var frame = SceneView.Session.CurrentFrame;
				if (frame == null) return 1000;

				// Return ambient intensity
				if (frame.LightEstimate == null) {
					return 1000;
				} else {
					return (float)frame.LightEstimate.AmbientIntensity;
				}
			}
		}
		#endregion

		#region Constructors
		protected GameViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		#endregion

		#region Override Methods
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Set self as the Scene View's delegate
			SceneView.Delegate = this;

			// Track changes to the session
			SceneView.Session.Delegate = new SessionDelegate();

			// Create a new, empty scene
            SceneView.Scene = new SCNScene();

			// Add a tap gesture recognizer
			var tapGesture = new UITapGestureRecognizer(HandleTap);
			View.AddGestureRecognizer(tapGesture);

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			// Create a session configuration
			var configuration = new ARWorldTrackingConfiguration {
				PlaneDetection = ARPlaneDetection.Horizontal,
				LightEstimationEnabled = true
			};

			// Run the view's session
			SceneView.Session.Run(configuration, ARSessionRunOptions.ResetTracking);

            // randomly choose a point to place the Earth
            var pos = new SCNVector3 (-2f, 0f, -2f);


			// earth r=0.2 
			var globe = SCNSphere.Create(0.2f);
			var globeNode = new SCNNode { Position = pos, Geometry = globe };
			globeNode.Geometry.Materials = LoadMaterials();
			//globeNode.Transform = SCNMatrix4.CreateRotationX(0.4101524f); // 23.5 degrees
			SceneView.Scene.RootNode.AddChildNode(globeNode);

			globeNode.RunAction(SCNAction.RepeatActionForever(SCNAction.RotateBy(0, 1, 0, 3)));


			//moon r=0.08, orbit=0.6
			var pivotNode = new SCNNode { Position = new SCNVector3(0, 0, 0) };
			pivotNode.RunAction(SCNAction.RepeatActionForever(SCNAction.RotateBy(0, 1, 0, 5)));

			var moon = SCNSphere.Create(0.08f);
			var moonNode = new SCNNode { Geometry = moon };
			//moonNode.Position = new SCNVector3(pos.X - 0.6f, pos.Y + 0.1f, pos.Z);
            moonNode.Position = new SCNVector3(0.6f, 0.1f, pos.Z);
			moonNode.Geometry.Materials = LoadMoonMaterials();
			pivotNode.AddChildNode(moonNode);

			globeNode.AddChildNode(pivotNode);

		}



		Func<string, SCNMaterial> LoadMaterial = fname =>
		{
			var mat = new SCNMaterial();
			mat.Diffuse.Contents = UIImage.FromFile(fname);
			mat.LocksAmbientWithDiffuse = true;
			return mat;
		};
		private SCNMaterial[] LoadMaterials()
		{
			var a = LoadMaterial("earth.jpg");

			return new[] { a, a, a, a, a, a };
		}
		private SCNMaterial[] LoadMoonMaterials()
		{
			var a = LoadMaterial("moon.jpg");

			return new[] { a, a, a, a, a, a };
		}


		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			// Pause the view's session
			SceneView.Session.Pause();
		}

		public override bool ShouldAutorotate()
		{
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}
		#endregion

		#region Private Methods
		private void HandleTap(UIGestureRecognizer gestureRecognize)
		{
			// Get current frame
			var currentFrame = SceneView.Session.CurrentFrame;
			if (currentFrame == null) return;

			// Create an image plane using a snapshot of the view
			var imagePlane = SCNPlane.Create(SceneView.Bounds.Width / 6000, SceneView.Bounds.Height / 6000);
			imagePlane.FirstMaterial.Diffuse.Contents = SceneView.Snapshot();
			imagePlane.FirstMaterial.LightingModelName = SCNLightingModel.Constant;

			// Create a plane node and add it to the scene
			var planeNode = SCNNode.FromGeometry(imagePlane);
			SceneView.Scene.RootNode.AddChildNode(planeNode);

			// Set transform of node to be 10cm in front of the camera
			var translation = SCNMatrix4.CreateTranslation(0, 0, 0.1f);
			var cameraTranslation = currentFrame.Camera.Transform.ToSCNMatrix4();
			planeNode.Transform = SCNMatrix4.Mult(cameraTranslation, translation);
		}

		private void AddAnchorToScene() {

			// Get the current frame
			var frame = SceneView.Session.CurrentFrame;
			if (frame == null) return;

			// Create a ray to test from
			var point = new CGPoint(0.5, 0.5);

			// Preform hit testing on frame
			var results = frame.HitTest(point, ARHitTestResultType.ExistingPlane | ARHitTestResultType.EstimatedHorizontalPlane);

			// Use the first result
			if (results.Length >0) {
				var result = results[0];

				// Create an anchor for it
				var anchor = new ARAnchor(result.WorldTransform);

				// Add anchor to session
				SceneView.Session.AddAnchor(anchor);
			}

		}
		#endregion
	}
}
