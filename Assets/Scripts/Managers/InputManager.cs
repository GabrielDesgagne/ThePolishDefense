using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : Flow
{
    #region Singleton
    static private InputManager instance = null;

    static public InputManager Instance
    {
        get
        {
            return instance ?? (instance = new InputManager());
        }
    }
    #endregion

    public InputPkg inputs;

    override public void PreInitialize()
    {
        inputs = new InputPkg();
    }

    override public void Initialize()
    {
    }

    override public void Refresh()
    {
        // returns true if the primary button (typically “A”) is currently pressed.
        inputs.Touch[OVRInput.Controller.LTouch].ButtonOne = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].ButtonOne = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.LTouch].ButtonTwo = OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].ButtonTwo = OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch);

        // returns a Vector2 of the primary (typically the Left) thumbstick’s current state.
        // (X/Y range of -1.0f to 1.0f)
        inputs.Touch[OVRInput.Controller.LTouch].Joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].Joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

        // returns true if the primary thumbstick is currently pressed (clicked as a button)
        inputs.Touch[OVRInput.Controller.LTouch].JoystickPressed = OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].JoystickPressed = OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch);

        // returns true if the primary thumbstick has been moved upwards more than halfway.
        inputs.Touch[OVRInput.Controller.LTouch].JoystickUp = OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].JoystickUp = OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.LTouch].JoystickDown = OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].JoystickDown = OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.LTouch].JoystickLeft = OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].JoystickLeft = OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch);
        

        inputs.Touch[OVRInput.Controller.LTouch].JoystickRight = OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].JoystickRight = OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch);

        // returns a float of the left index finger trigger’s current state.
        // (range of 0.0f to 1.0f)
        inputs.Touch[OVRInput.Controller.LTouch].IndexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].IndexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.LTouch].HandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].HandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.LTouch].Position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].Position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.LTouch].Rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].Rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.RTouch].Start = OVRInput.Get(OVRInput.Button.Start, OVRInput.Controller.LTouch);

        inputs.Touch[OVRInput.Controller.LTouch].NearButtons = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].NearButtons = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.RTouch);

        inputs.Touch[OVRInput.Controller.LTouch].NearPrimaryIndexTrigger = OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        inputs.Touch[OVRInput.Controller.RTouch].NearPrimaryIndexTrigger = OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

    }

    override public void PhysicsRefresh()
    {
       
    }

    override public void EndFlow()
    {
        Debug.Log("EndFlow");
    }

    public class InputPkg
    {
        
        public Dictionary<OVRInput.Controller, TouchController> Touch { get; set; }

        public InputPkg()
        {
            Touch = new Dictionary<OVRInput.Controller, TouchController>();
            Touch.Add(OVRInput.Controller.LTouch, new TouchController());
            Touch.Add(OVRInput.Controller.RTouch, new TouchController());
        }

    }
}

public class TouchController // TODO Check why can't use struct
{
    public Vector2 Joystick { get; set; }
    public bool JoystickPressed { get; set; }
    public bool NearPrimaryIndexTrigger { get; set; }
    public bool NearButtons { get; set; }
    public bool JoystickUp { get; set; }
    public bool JoystickDown { get; set; }
    public bool JoystickLeft { get; set; }
    public bool JoystickRight { get; set; }
    public bool ButtonOne { get; set; }
    public bool ButtonTwo { get; set; }
    public bool Start { get; set; }
    public float HandTrigger { get; set; }
    public float IndexTrigger { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
}

