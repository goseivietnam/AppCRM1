using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AppCRM.Controls;
using AppCRM.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePickerImplementation))]
namespace AppCRM.Droid
{
    class FilePickerImplementation : IFilePicker
    {
        public Task<SJFileStream> GetFileStreamAsync(string[] mimeTypes)
        {
            // Define the Intent for getting files
            Intent intent = new Intent();
            intent.SetType("file/*");
            intent.PutExtra(Intent.ExtraMimeTypes, mimeTypes);
            intent.SetAction(Intent.ActionGetContent);

            // Start the file-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Files"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<SJFileStream>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }

        public Task<SJFileStream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<SJFileStream>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}