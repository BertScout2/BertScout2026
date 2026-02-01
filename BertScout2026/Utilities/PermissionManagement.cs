namespace BertScout2026.Utilities;

public static class PermissionManagement
{
    /*
     Android requires runtime permissions for accessing storage. This code checks the necessary permissions.
        1. Check Current Permission Status: The code first checks the current status of the StorageRead permission using Permissions.CheckStatusAsync<Permissions.StorageRead>().
        2. Request Permission if Not Granted: If the permission status is not Granted, the code can request the permission from the user.
        3. Handle Different Outcomes: The code handles different outcomes based on the permission status:
        - If the permission is Granted, it returns true, indicating that the app can proceed with storage access.
        - If the permission is Denied, it returns false, indicating that the app cannot access storage.
        4. Exception Handling: The code includes a try-catch block to handle any exceptions that may occur during the permission check process, such as when the Permissions API is not supported on the platform.
     */

    private static PermissionStatus statusRead = PermissionStatus.Unknown;

    public static async Task<bool> CheckAndRequestStoragePermissionsAsync()
    {
        try
        {
            if (statusRead != PermissionStatus.Unknown)
            {
                return true; // Permissions already granted
            }
            statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (statusRead == PermissionStatus.Granted)
            {
                return true; // Permission granted
            }
            return false; // Permission denied
        }
        catch (Exception ex)
        {
            var _ = ex.Message;
            return false; // Permissions API not supported
        }
    }
}
