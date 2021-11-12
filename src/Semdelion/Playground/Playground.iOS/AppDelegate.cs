using System;
using Firebase.CloudMessaging;
using Firebase.Core;
using Firebase.InstanceID;
using Foundation;
using Semdelion.iOS;
using UIKit;
using UserNotifications;

namespace Playground.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : BaseAppDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
	{
		public event EventHandler<UserInfoEventArgs> MessageReceived;

		public override UIWindow Window
		{
			get;
			set;
		}

		public override BaseIosSetup MvxIosSetup()
			=> new Setup();

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Google.Maps.MapServices.ProvideApiKey("");//TODO add ApiKey

			App.Configure();


			// Register your app for remote notifications.
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				// For iOS 10 display notification (sent via APNS)
				UNUserNotificationCenter.Current.Delegate = this;

				var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
				UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
					Console.WriteLine(granted);
				});
			}
			else
			{
				// iOS 9 or before
				var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
				var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
				UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			}

			UIApplication.SharedApplication.RegisterForRemoteNotifications();

			Messaging.SharedInstance.Delegate = this;

			InstanceId.SharedInstance.GetInstanceId(InstanceIdResultHandler);

			return base.FinishedLaunching(application, launchOptions);
		}

		void InstanceIdResultHandler(InstanceIdResult result, NSError error)
		{
			if (error != null)
			{
				LogInformation(nameof(InstanceIdResultHandler), $"Error: {error.LocalizedDescription}");
				return;
			}

			LogInformation(nameof(InstanceIdResultHandler), $"Remote Instance Id token: {result.Token}");
		}

		[Export("messaging:didReceiveRegistrationToken:")]
		public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
		{
			// Monitor token generation: To be notified whenever the token is updated.
			Semdelion.Core.User.Settings.TokenFcm = fcmToken;
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			// Handle Notification messages in the background and foreground.
			HandleMessage(userInfo);

			// Print full message.
			LogInformation(nameof(DidReceiveRemoteNotification), userInfo);

			completionHandler(UIBackgroundFetchResult.NewData);
		}

		[Export("messaging:didReceiveMessage:")]
		public void DidReceiveMessage(Messaging messaging, RemoteMessage remoteMessage)
		{
			// Handle Data messages for iOS 10 and above.
			HandleMessage(remoteMessage.AppData);

			LogInformation(nameof(DidReceiveMessage), remoteMessage.AppData);
		}

		void HandleMessage(NSDictionary message)
		{
			if (MessageReceived == null)
				return;

			MessageType messageType;
			if (message.ContainsKey(new NSString("aps")))
				messageType = MessageType.Notification;
			else
				messageType = MessageType.Data;

			var e = new UserInfoEventArgs(message, messageType);
			MessageReceived(this, e);
		}

		public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Window = new UIWindow(UIScreen.MainScreen.Bounds);
			return true;
		}

		void LogInformation(string methodName, object information)
			=> Console.WriteLine($"\nMethod name: {methodName}\nInformation: {information}");
	}

	public class UserInfoEventArgs : EventArgs
	{
		public NSDictionary UserInfo { get; private set; }
		public MessageType MessageType { get; private set; }

		public UserInfoEventArgs(NSDictionary userInfo, MessageType messageType)
		{
			UserInfo = userInfo;
			MessageType = messageType;
		}
	}

	public enum MessageType
	{
		Notification,
		Data
	}
}