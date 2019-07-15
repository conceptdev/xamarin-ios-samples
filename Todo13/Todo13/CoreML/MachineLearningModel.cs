using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreML;
using Foundation;
using UIKit;

namespace Todo11App
{
    class ImageDescriptionPrediction
    {
        public string ModelName = "VGG16";
        public List<Tuple<double, string>> predictions;
    }

    class MachineLearningModel
    {
        public event EventHandler<EventArgsT<ImageDescriptionPrediction>> PredictionsUpdated = delegate { };
        public event EventHandler<EventArgsT<String>> ErrorOccurred = delegate { };
        public event EventHandler<EventArgsT<String>> MessageUpdated = delegate { };

        MLModel currentModel;
        CGSize sizeForModel;

        internal MachineLearningModel()
        {
            currentModel = LoadModel("VGG16");
            sizeForModel = new CGSize(224, 224);
        }

        MLModel LoadModel(string modelName)
        {
            var assetPath = NSBundle.MainBundle.GetUrlForResource(modelName, "mlmodelc");
            MLModel mdl = null;
            try
            {
                NSError err;
                mdl = MLModel.Create(assetPath, out err);
                if (err != null)
                {
                    ErrorOccurred(this, new EventArgsT<string>(err.ToString()));
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("*** VGG16 model probably hasn't been downloaded, built, and added to the project's Resources. Refer to the README for instructions. Error: " + ane.Message);
            }
            return mdl;
        }

        internal void Classify(UIImage source)
        {
            var pixelBuffer = source.Scale(sizeForModel).ToCVPixelBuffer();
            var imageValue = MLFeatureValue.Create(pixelBuffer);

            var inputs = new NSDictionary<NSString, NSObject>(new NSString("image"), imageValue);

            NSError error, error2;
            var inputFp = new MLDictionaryFeatureProvider(inputs, out error);
            if (error != null)
            {
                ErrorOccurred(this, new EventArgsT<string>(error.ToString()));
                return;
            }
            var outFeatures = currentModel.GetPrediction(inputFp, out error2);
            if (error2 != null)
            {
                ErrorOccurred(this, new EventArgsT<string>(error2.ToString()));
                return;
            }

            var predictionsDictionary = outFeatures.GetFeatureValue("classLabelProbs").DictionaryValue;
            var byProbability = new List<Tuple<double, string>>();
            foreach (var key in predictionsDictionary.Keys)
            {
                var description = (string)(NSString)key;
                var prob = (double)predictionsDictionary[key];
                byProbability.Add(new Tuple<double, string>(prob, description));
            }
            //Sort descending
            byProbability.Sort((t1, t2) => t1.Item1.CompareTo(t2.Item1) * -1);

            var prediction = new ImageDescriptionPrediction();
            prediction.ModelName = "VGG16";
            prediction.predictions = byProbability;

            PredictionsUpdated(this, new EventArgsT<ImageDescriptionPrediction>(prediction));
        }
    }
}
