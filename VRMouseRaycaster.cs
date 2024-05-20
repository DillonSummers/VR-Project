using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Xml.Linq;

public class VRMouseRaycaster : MonoBehaviour
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

    private Camera mainCamera;
    public float offsetHeight = 0.0f;  // Offset in meters


    void Start()
    {
        mainCamera = Camera.main; // Ensure your VR camera is tagged as Main Camera
    }

    void Update()
    {
        Vector3 controllerPosition = transform.position;
        Quaternion controllerRotation = transform.rotation;

        // Offset the start position upward by the specified height
        Vector3 startPosition = controllerPosition;

        // Adjust the ray to project from the correct axis
        Ray ray = new Ray(startPosition, controllerRotation * Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "UWindowCapture")
            {
                // Extract UV coordinates of the hit point
                Vector2 uv = hit.textureCoord;
                MapToScreenCoordinates(uv, hit.collider.gameObject);
            }
        }

        // Debug.DrawLine(ray.origin, hit.point, Color.red); // Show the ray in red


        if (OVRInput.GetDown(OVRInput.Button.One)) {
            Debug.Log("Primary index triggered");
            StartLeftMouseClick();
        }

        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            EndLeftMouseClick();
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            RightMouseClick();
        }
    }

    void MapToScreenCoordinates(Vector2 uv, GameObject hitObject)
    {

        Debug.Log("Mapping to screen coords");
        // Assuming the object has a texture that matches the screen dimensions
        Renderer rend = hitObject.GetComponent<Renderer>();
        Texture tex = rend.material.mainTexture;
        
        int x = (int)(uv.x * tex.width);
        int y = (int)(uv.y * tex.height);

        SetCursorPos(x, y);
    }

    // Method to start left mouse click (hold down)
    public static void StartLeftMouseClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Debug.Log("Left mouse button held down.");
    }

    // Method to end left mouse click (release)
    public static void EndLeftMouseClick()
    {
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        Debug.Log("Left mouse button released.");
    }

    // Method for right mouse click
    public static void RightMouseClick()
    {
        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
    }

}
