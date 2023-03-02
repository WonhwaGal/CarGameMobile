using System;
using Tool.PushNotifications.Settings;

#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine;
#endif

namespace Tool.PushNotifications
{
    internal class AndroidNotificationScheduler : INotificationScheduler
    {
        public void RegisterChannel(ChannelSettings channelSettings)
        {
#if UNITY_ANDROID
            var androidNotificationChannel = new AndroidNotificationChannel
            (
                channelSettings.Id,
                channelSettings.Name,
                channelSettings.Description,
                channelSettings.Importance
            );

            AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
#endif
        }

        public void ScheduleNotification(NotificationData notificationData)
        {
#if UNITY_ANDROID
            AndroidNotification androidNotification = CreateAndroidNotification(notificationData);
            AndroidNotificationCenter.SendNotification(androidNotification, notificationData.Id);
#endif
        }

#if UNITY_ANDROID
        private AndroidNotification CreateAndroidNotification(NotificationData notificationData)
        {
            DateTime currentTime = DateTime.Now;
            DateTime fireTime = currentTime;

            if (notificationData.WaitBeforeFire > 0)
            {
                fireTime = currentTime + TimeSpan.FromSeconds(notificationData.WaitBeforeFire);
            }

            switch (notificationData.RepeatType)
            {
                case NotificationRepeat.OnceFixed:
                    return new AndroidNotification(
                           notificationData.Title,
                           notificationData.Text,
                           notificationData.FireTime);
                case NotificationRepeat.OnceFromNow:
                    var notification = new AndroidNotification(
                           notificationData.Title,
                           notificationData.Text,
                           fireTime);
                    if (notificationData.SmallIcon != null)
                           notification.SmallIcon = notificationData.SmallIcon;
                    return notification;
                case NotificationRepeat.RepeatableFixed:
                    return new AndroidNotification(
                           notificationData.Title,
                           notificationData.Text,
                           notificationData.FireTime,
                           notificationData.RepeatInterval);
                case NotificationRepeat.RepeatableFromNow:
                    return new AndroidNotification(
                           notificationData.Title,
                           notificationData.Text,
                           fireTime,
                           notificationData.RepeatInterval);
                default:
                    throw new ArgumentOutOfRangeException(nameof(notificationData.RepeatType));
            };
        }
#endif
    }
}
