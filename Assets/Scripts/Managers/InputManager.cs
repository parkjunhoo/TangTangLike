using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action JoystickAction = null;
    FloatingJoystick joy;
    public float joyX
    {
        get
        {
            return joy.Horizontal;
        }
    }
    public float joyY
    {
        get
        {
            return joy.Vertical;
        }
    }
    public void OnUpdate()
    {
        if (joy == null) joy = GameObject.Find("@Joystick").GetComponent<FloatingJoystick>();

        if (JoystickAction != null)
        {
            JoystickAction.Invoke();
        }
    }
}

