using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public ControlSettings controlSettings;
    public TextMeshProUGUI Left, Right, Grab, Use, Jump;

    // Start is called before the first frame update
    void Start()
    {
        controlSettings = GameObject.Find("ControlSettings").GetComponent<ControlSettings>();
    }

    private void Update()
    {
        Left.text = controlSettings.Left.GetBindingDisplayString(0);
        Right.text = controlSettings.Right.GetBindingDisplayString(0);
        Grab.text = controlSettings.Pick.GetBindingDisplayString(0);
        Use.text = controlSettings.Light.GetBindingDisplayString(0);
        Jump.text = controlSettings.Jump.GetBindingDisplayString(0);
    }

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    public void RemapLeft()
    {
        controlSettings.Left.Disable();
        if(rebindOperation != null)
        {
            rebindOperation.Reset();
        }
        rebindOperation = controlSettings.Left.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        controlSettings.Left.Enable();
    }

    public void RemapRight()
    {
        controlSettings.Right.Disable();
        if (rebindOperation != null)
        {
            rebindOperation.Reset();
        }
        rebindOperation = controlSettings.Right.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        controlSettings.Right.Enable();
    }

    public void RemapJump()
    {
        controlSettings.Jump.Disable();
        if (rebindOperation != null)
        {
            rebindOperation.Reset();
        }
        rebindOperation = controlSettings.Jump.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        controlSettings.Jump.Enable();
    }

    public void RemapGrab()
    {
        controlSettings.Pick.Disable();
        if (rebindOperation != null)
        {
            rebindOperation.Reset();
        }
        rebindOperation = controlSettings.Pick.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        controlSettings.Pick.Enable();
    }

    public void RemapUse()
    {
        controlSettings.Light.Disable();
        if (rebindOperation != null)
        {
            rebindOperation.Reset();
        }
        rebindOperation = controlSettings.Light.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        controlSettings.Light.Enable();
    }
}
