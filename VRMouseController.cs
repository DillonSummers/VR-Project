using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class VRMouseController : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll", EntryPoint = "mouse_event")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;

    // void Start()
    // {
    //     mainCamera = Camera.main; // Ensure your VR camera is tagged as Main Camera
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    //     Vector3 screenPosition = mainCamera.WorldToScreenPoint(controllerPosition);

    //     SetCursorPos((int)screenPosition.x, (int)Screen.height - (int)screenPosition.y);

    //     if (OVRInput.GetDown(OVRInput.Button.One)) {
    //         Debug.Log("Primary index triggered");
    //         LeftMouseClick();
    //     }
    // }

    public static void LeftMouseClick() {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }
}
