using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI;
using UnityEngine;

namespace Managers
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField] const int maxNotificationsShown = 1;
        [SerializeField] GameObject notificationPrefab;
        [SerializeField] RectTransform notificationsParent;
        [SerializeField] float durationPerWord = .2f;
        [SerializeField] float notificationCooldown = 1f;

        Notification[] _notifications;
        Queue<Notification> _notificationQueue = new();
        bool[] _isFreeSpots;
        float _notificationHeight;

        public static NotificationManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            _notifications = new Notification[maxNotificationsShown];
            _isFreeSpots = new bool[maxNotificationsShown];
            for (var i = 0; i < maxNotificationsShown; i++)
            {
                _isFreeSpots[i] = true;
            }
        }

        void Start()
        {
            _notificationHeight = notificationPrefab.GetComponent<RectTransform>().rect.height;
        }

        bool TryGetFreeSpot(out int index)
        {
            index = 0;
            for (int i = 0; i < maxNotificationsShown; i++)
            {
                if (_isFreeSpots[i])
                {
                    index = i;
                    _isFreeSpots[i] = false;
                    return true;
                }
            }
            return false;
        }

        Vector2 GetNotificationPositionOnIndex(int index)
        {
            var position = Vector2.zero;
            position.y -= index * _notificationHeight + _notificationHeight / 2f;
            return position;
        }

        bool TryAddNotification(Notification notification)
        {
            if (!TryGetFreeSpot(out var spot))
                return false;

            var notificationInstance = Instantiate(notificationPrefab, notificationsParent);

            notificationInstance.GetComponent<RectTransform>().anchoredPosition = GetNotificationPositionOnIndex(spot);
            var inGameNotification = notificationInstance.GetComponent<InGameNotification>();
            if (!inGameNotification) throw new UnityException("The notification prefab must have the InGameNotification component!");
            inGameNotification.Initialize(notification,
                () =>
                {
                    StartCoroutine(OnNotificationDestroy(spot));
                });
            inGameNotification.enabled = true;
            return true;
        }

        IEnumerator OnNotificationDestroy(int spot)
        {
            _isFreeSpots[spot] = true;
            yield return new WaitForSeconds(notificationCooldown);
            if (_notificationQueue.Count > 0 && TryAddNotification(_notificationQueue.Peek())) _notificationQueue.Dequeue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool TryBroadcast(Notification notification)
        {
            foreach (var notificationShown in _notifications)
            {
                if (notification == notificationShown) return false;
            }
            if (!TryAddNotification(notification)) _notificationQueue.Enqueue(notification);
                
            return true;
        }

        public void Broadcast(string message)
        {
            var tmpText = FindAnyObjectByType<TextMeshProUGUI>();
            var oldText = tmpText.text;
            var textInfo = tmpText.GetTextInfo(message);
            tmpText.text = oldText;
            var notification = new Notification(
                message,
                durationPerWord * textInfo.wordCount);

            TryBroadcast(notification);
        }
    }
}
