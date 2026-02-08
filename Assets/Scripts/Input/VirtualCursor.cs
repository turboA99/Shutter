using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VirtualCursorState : IInputStateTypeInfo
    {
        public static FourCC Format => new('V', 'C', 'U', 'R');

#if UNITY_EDITOR
        [InputControl(layout = "Vector2", displayName = "Position", usage = "Point", processors = "AutoWindowSpace", dontReset = true)]
#else
        [InputControl(layout = "Vector2", displayName = "Position", usage = "Point", dontReset = true)]
#endif
        public Vector2 position;

        [InputControl(usage = "Secondary2DMotion", layout = "Delta")]
        public Vector2 delta;

        [InputControl(displayName = "Scroll", layout = "Delta")]
        [InputControl(name = "scroll/x", aliases = new[] { "horizontal" }, usage = "ScrollHorizontal", displayName = "Left/Right")]
        [InputControl(name = "scroll/y", aliases = new[] { "vertical" }, usage = "ScrollVertical", displayName = "Up/Down")]
        public Vector2 scroll;

        [InputControl(name = "press", displayName = "Press", layout = "Button", format = "BIT", bit = 0)]
        public ushort buttons;

        public VirtualCursorState WithPress(bool state)
        {
            var bit = 0U;
            if (state)
                buttons |= (ushort)bit;
            else
                buttons &= (ushort)~bit;
            return this;
        }

        public FourCC format => Format;
    }

    [InputControlLayout(stateType = typeof(VirtualCursorState), isGenericTypeOfDevice = true)]
    public class VirtualCursor : Pointer, IInputStateCallbackReceiver
    {
        bool _confineToViewport = true;

        public DeltaControl scroll { get; protected set; }

        public new static VirtualCursor current { get; protected set; }

        public bool ConfineToViewport
        {
            get => _confineToViewport;
            set
            {
                _confineToViewport = value;
                if (_confineToViewport)
                    ConfineWithinViewport();
            }
        }

        void IInputStateCallbackReceiver.OnNextUpdate()
        {
            OnNextUpdate();
        }

        void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
        {
            OnStateEvent(eventPtr);
        }

        public override void MakeCurrent()
        {
            base.MakeCurrent();
            current = this;
        }

        void ConfineWithinViewport()
        {
            Vector2 pos = position.ReadValue();
            if (!Screen.safeArea.Contains(pos))
            {
                pos.x = Mathf.Clamp(pos.x, Screen.safeArea.xMin, Screen.safeArea.xMax);
                pos.y = Mathf.Clamp(pos.y, Screen.safeArea.yMin, Screen.safeArea.yMax);
                MoveTo(pos);
            }
        }

        public void MoveTo(Vector2 newPosition)
        {
            if (_confineToViewport && !Screen.safeArea.Contains(newPosition)) return;
            InputSystem.QueueDeltaStateEvent(delta, newPosition - position.value);
            InputSystem.QueueDeltaStateEvent(position, newPosition);
        }

        public void SetPressed(bool pressed)
        {
            InputSystem.QueueDeltaStateEvent(press, pressed);
        }

        protected override void FinishSetup()
        {
            scroll = GetChildControl<DeltaControl>("scroll");
            base.FinishSetup();
        }

        protected new void OnNextUpdate()
        {
            base.OnNextUpdate();
            InputState.Change(scroll, Vector2.zero);
        }

        protected new void OnStateEvent(InputEventPtr eventPtr)
        {
            base.OnStateEvent(eventPtr);
        }
    }
}
