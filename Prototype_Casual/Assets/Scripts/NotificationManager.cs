using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    private void Start()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }
    private void OnApplicationPause(bool pause)
    {
        SendNotificationSeeLater();
    }
    void SendNotificationSeeLater()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        //create the Android Notification to send messages through 
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.Default,
            Description = "Pause notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "See you later";
        notification.Text = "Your Text";
        notification.LargeIcon = "icon_0";
        notification.FireTime = System.DateTime.Now.AddSeconds(5);

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id)==NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
