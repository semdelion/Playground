
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;

namespace Semdelion.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMsgService : FirebaseMessagingService
    {
        const string TAG = "FirebaseMsgService";

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            Log.Debug(TAG, "Refreshed token: " + token);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
        }
    }
}