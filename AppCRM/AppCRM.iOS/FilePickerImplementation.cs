using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using AppCRM.iOS;
using UIKit;
using Xamarin.Forms;
using AppCRM.Controls;

[assembly: Dependency(typeof(FilePickerImplementation))]
namespace AppCRM.iOS
{
    class FilePickerImplementation : IFilePicker
    {
        TaskCompletionSource<SJFileStream> taskCompletionSource;
        UIImagePickerController imagePicker;

        public Task<SJFileStream> GetImageStreamAsync()
        {
            // Create and define UIImagePickerController
            imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            // Set event handlers
            imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            imagePicker.Canceled += OnImagePickerCancelled;

            // Present UIImagePickerController;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(imagePicker, true);

            // Return Task object
            taskCompletionSource = new TaskCompletionSource<SJFileStream>();
            return taskCompletionSource.Task;
        }

        public Task<SJFileStream> GetFileStreamAsync(string[] mimeTypes)
        {
            throw new NotImplementedException();
        }

        void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            UIImage image = args.EditedImage ?? args.OriginalImage;

            if (image != null)
            {
                // Convert UIImage to .NET Stream object
                NSData data = image.AsJPEG(1);
                Stream stream = data.AsStream();
                var url = (NSUrl)args.Info.ValueForKey(new NSString("UIImagePickerControllerImageURL"));
                string fileName = url.Path;

                // Set the SJFileStream as the completion of the Task
                taskCompletionSource.SetResult(new SJFileStream { Stream = stream, FileName = fileName });
            }
            else
            {
                taskCompletionSource.SetResult(null);
            }
            imagePicker.DismissModalViewController(true);
        }

        void OnImagePickerCancelled(object sender, EventArgs args)
        {
            taskCompletionSource.SetResult(null);
            imagePicker.DismissModalViewController(true);
        }
    }
}