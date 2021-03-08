using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSettings : MonoBehaviour
{
    public InputAction Right;
    public InputAction Left;
    public InputAction Jump;
    public InputAction Pick;
    public InputAction Light;
    public InputAction Flame;

    private void OnEnable()
    {
        Right.Enable();
        Left.Enable();
        Jump.Enable();
        Pick.Enable();
        Light.Enable();
        Flame.Enable();
    }

    private void OnDisable()
    {
        Right.Disable();
        Left.Disable();
        Jump.Disable();
        Pick.Disable();
        Light.Disable();
        Flame.Disable();
    }
}
