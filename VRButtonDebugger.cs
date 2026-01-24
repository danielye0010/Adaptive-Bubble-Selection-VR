using UnityEngine;
using UnityEngine.InputSystem;

public class VRButtonDebugger : MonoBehaviour
{
    public InputActionProperty leftTrigger;
    public InputActionProperty rightTrigger;

    public InputActionProperty leftGrip;
    public InputActionProperty rightGrip;

    public InputActionProperty leftPrimaryButton;
    public InputActionProperty rightPrimaryButton;

    public InputActionProperty leftSecondaryButton;
    public InputActionProperty rightSecondaryButton;

    void Update()
    {
        if (leftTrigger.action != null && leftTrigger.action.WasPressedThisFrame())
            Debug.Log("LEFT TRIGGER");

        if (rightTrigger.action != null && rightTrigger.action.WasPressedThisFrame())
            Debug.Log("RIGHT TRIGGER");

        if (leftGrip.action != null && leftGrip.action.WasPressedThisFrame())
            Debug.Log("LEFT GRIP");

        if (rightGrip.action != null && rightGrip.action.WasPressedThisFrame())
            Debug.Log("RIGHT GRIP");

        if (leftPrimaryButton.action != null && leftPrimaryButton.action.WasPressedThisFrame())
            Debug.Log("LEFT PRIMARY BUTTON (X)");

        if (rightPrimaryButton.action != null && rightPrimaryButton.action.WasPressedThisFrame())
            Debug.Log("RIGHT PRIMARY BUTTON (A)");

        if (leftSecondaryButton.action != null && leftSecondaryButton.action.WasPressedThisFrame())
            Debug.Log("LEFT SECONDARY BUTTON (Y)");

        if (rightSecondaryButton.action != null && rightSecondaryButton.action.WasPressedThisFrame())
            Debug.Log("RIGHT SECONDARY BUTTON (B)");
    }
}
