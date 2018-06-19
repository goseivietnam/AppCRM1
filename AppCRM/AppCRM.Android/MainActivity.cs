using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using AppCRM.Controls;
using CarouselView.FormsPlugin.Android;
using System.IO;
using System.Threading.Tasks;

namespace AppCRM.Droid
{
    [Activity(Label = "AppCRM", Icon = "@mipmap/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<SJFileStream> PickImageTaskCompletionSource { set; get; }
        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            Rg.Plugins.Popup.Popup.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CarouselViewRenderer.Init();
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);
                    string fileName = GetFileName(uri);

                    // Set the SJFileStream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(new SJFileStream { Stream = stream, FileName = fileName });
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        private string GetFileName(Android.Net.Uri uri)
        {

            string[] projection = { MediaStore.MediaColumns.DisplayName };

            var cr = Application.Context.ContentResolver;
            var name = "";
            var metaCursor = cr.Query(uri, projection, null, null, null);

            if (metaCursor != null)
            {
                try
                {
                    if (metaCursor.MoveToFirst())
                    {
                        name = metaCursor.GetString(0);
                    }
                }
                finally
                {
                    metaCursor.Close();
                }
            }
            return name;
        }

    }
}

