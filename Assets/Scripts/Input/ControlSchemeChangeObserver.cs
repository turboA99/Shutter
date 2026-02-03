using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Input
{
    public static class ControlSchemeChangeObserver
    {
        static ControlSchemeChangeObserver()
        {
            InputUser.onChange += OnUserChange;
        }

        public static event Action<string> OnControlSchemeChangedEvent;

        public static string GetCurrentControlScheme()
        {
            return InputUser.all.First().controlScheme.GetValueOrDefault().name;
        }

        static void OnUserChange(InputUser user, InputUserChange change, InputDevice device)
        {
            if (change == InputUserChange.ControlSchemeChanged)
            {
                ControlSchemeChange(user.controlScheme.GetValueOrDefault().name);
            }
        }

        static void ControlSchemeChange(string controlScheme)
        {
            OnControlSchemeChangedEvent?.Invoke(controlScheme);
        }
    }
}
