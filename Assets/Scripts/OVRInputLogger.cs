using UnityEngine;

public class OVRInputLogger : MonoBehaviour
{
    void Update()
    {
        LogButtons();
        LogTriggers();
        LogThumbsticks();
    }

    // -------------------------
    // BOTONES (digital)
    // -------------------------
    void LogButtons()
    {
        CheckButton(OVRInput.Button.One, "A / X");
        CheckButton(OVRInput.Button.Two, "B / Y");
        CheckButton(OVRInput.Button.Three, "X / A");
        CheckButton(OVRInput.Button.Four, "Y / B");

        CheckButton(OVRInput.Button.Start, "Menu");
        CheckButton(OVRInput.Button.PrimaryThumbstick, "Thumbstick Click Izq");
        CheckButton(OVRInput.Button.SecondaryThumbstick, "Thumbstick Click Der");
    }

    void CheckButton(OVRInput.Button button, string name)
    {
        if (OVRInput.GetDown(button))
            Debug.Log($"[BOTÓN DOWN] {name}");

        if (OVRInput.GetUp(button))
            Debug.Log($"[BOTÓN UP] {name}");
    }

    // -------------------------
    // GATILLOS Y GRIPS (analógicos)
    // -------------------------
    void LogTriggers()
    {
        float indexLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float indexRight = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        float gripLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        float gripRight = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);

        LogAxis("Index Izq", indexLeft);
        LogAxis("Index Der", indexRight);
        LogAxis("Grip Izq", gripLeft);
        LogAxis("Grip Der", gripRight);
    }

    void LogAxis(string name, float value)
    {
        if (value > 0.01f) // evita spam
        {
            Debug.Log($"[ANALÓGICO] {name}: {value:F2}");
        }
    }

    // -------------------------
    // JOYSTICKS (vector 2D)
    // -------------------------
    void LogThumbsticks()
    {
        Vector2 leftStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 rightStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (leftStick.magnitude > 0.2f)
            Debug.Log($"[JOYSTICK IZQ] X:{leftStick.x:F2} Y:{leftStick.y:F2}");

        if (rightStick.magnitude > 0.2f)
            Debug.Log($"[JOYSTICK DER] X:{rightStick.x:F2} Y:{rightStick.y:F2}");
    }
}
