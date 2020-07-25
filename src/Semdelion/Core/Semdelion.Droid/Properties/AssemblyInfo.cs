using Android.App;

// Note: Shared assembly information is specified in SharedAssemblyInfo.cs

// We need to be able to download map tiles and access Google Play Services
[assembly: UsesPermission("android.permission.INTERNET")]

// Permission to receive remote notifications from Google Play Services.
// Notice here that we have the package name of our application as a prefix on the permissions.
// These are optional, but recommended.They will allow Maps to use the My Location provider.
[assembly: UsesPermission("android.permission.ACCESS_COARSE_LOCATION")]
[assembly: UsesPermission("android.permission.ACCESS_FINE_LOCATION")]
[assembly: UsesPermission("android.permission.CAMERA")]
[assembly: UsesPermission("android.permission.FLASHLIGHT")]
[assembly: Permission(Name = "android.permission.INTERACT_ACROSS_USERS_FULL", ProtectionLevel = Android.Content.PM.Protection.Signature)]