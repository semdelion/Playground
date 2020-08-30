using Android.App;
using Android.Gms.Common;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Semdelion.Droid.Views;

namespace Playground.Droid.View
{
    [MvxFragmentPresentation(typeof(MainFragmentHostViewModel), Resource.Id.main_layoutContent, true)]
    [Register(nameof(FirebaseView))]
    public class FirebaseView : BaseFragment<FirebaseViewModel>
    {
        protected override int FragmentId => Resource.Layout.firebase_view;


        static readonly string TAG = "FirebaseView";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        TextView msgText ;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
         
            msgText = view.FindViewById<TextView>(Resource.Id.msgText);
            
            if (Activity.Intent.Extras != null)
            {
                foreach (var key in Activity.Intent.Extras.KeySet())
                {
                    var value = Activity.Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }

            IsPlayServicesAvailable();

            CreateNotificationChannel();

            var logTokenButton = view.FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Click += delegate {
                GetInstance();
            };

            return view;
        }

        public async void GetInstance()
        {
            var instanceId = await FirebaseInstanceId.Instance.GetInstanceId();
            var token = instanceId.Class.GetMethod("getToken").Invoke(instanceId).ToString();
            Log.Debug(TAG, "InstanceID token: " + token);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this.Context);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    //Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)Application.Context.GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}