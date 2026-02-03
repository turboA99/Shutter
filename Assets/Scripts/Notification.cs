using System;

public struct Notification : IEquatable<Notification>
{
    public string Message;
    public float NotificationDuration;

    public static bool operator ==(Notification notification1, Notification notification2)
    {
        return notification1.Equals(notification2);
    }

    public static bool operator !=(Notification notification1, Notification notification2)
    {
        return !notification1.Equals(notification2);
    }

    public bool Equals(Notification other)
    {
        return Message == other.Message;
    }

    public override bool Equals(object obj)
    {
        return obj is Notification other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Message != null ? Message.GetHashCode() : 0;
    }

    public Notification(string message, float durationOnScreen)
    {
        Message = message;
        NotificationDuration = durationOnScreen;
    }
}
