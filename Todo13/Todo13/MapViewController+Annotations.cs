﻿using System;
using CoreLocation;
using Foundation;
using MapKit;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Todo11App
{
    public partial class MapViewController : UIViewController
    {
        // called in ViewDidLoad
        void SetUpAnnotations()
        {
            Map.Register(typeof(TodoView), MKMapViewDefault.AnnotationViewReuseIdentifier);
            Map.Register(typeof(ClusterView), MKMapViewDefault.ClusterAnnotationViewReuseIdentifier);

            Map.GetViewForAnnotation = HandleMKMapViewAnnotation;
        }

        void LoadData()
        {
#if DEBUG
            // DEMO: in Seattle
            var coordinate = new CLLocationCoordinate2D(47.620422, -122.349358);
            var span = new MKCoordinateSpan(0.00978871051851371, 0.00816739331921212);
            Map.Region = new MKCoordinateRegion(coordinate, span);
#endif
            var todos = AppDelegate.Current.TodoMgr.GetOrderedTodos().ToList(); 
            if (todos != null)
            {
                Map.AddAnnotations(TodoAnnotation.FromList(todos));
            }
        }
        void LoadData(TodoItem todo)
        {
            var list = new List<TodoItem> { Todo };
            Map.AddAnnotations(TodoAnnotation.FromList(list));
        }

        MKAnnotationView HandleMKMapViewAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is TodoAnnotation)
            {
                var marker = annotation as TodoAnnotation;

                var view = mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier) as TodoView;
                if (view == null)
                {
                    view = new TodoView(marker, MKMapViewDefault.AnnotationViewReuseIdentifier);
                }
                return view;
            }
            else if (annotation is MKClusterAnnotation)
            {
                var cluster = annotation as MKClusterAnnotation;

                var view = mapView.DequeueReusableAnnotation(MKMapViewDefault.ClusterAnnotationViewReuseIdentifier) as ClusterView;
                if (view == null)
                {
                    view = new ClusterView(cluster, MKMapViewDefault.ClusterAnnotationViewReuseIdentifier);
                }
                return view;
            }
            else if (annotation != null)
            {
                var unwrappedAnnotation = MKAnnotationWrapperExtensions.UnwrapClusterAnnotation(annotation);

                return HandleMKMapViewAnnotation(mapView, unwrappedAnnotation);
            }
            return null;
        }
    }
}