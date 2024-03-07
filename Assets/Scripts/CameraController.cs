using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        MouseCameraControl();
    }

    void MouseCameraControl()
    {
        // Rotate Camera Around Center Point Using Right Click
        if (Input.GetMouseButton(1)) // Right Click
        {
            transform.RotateAround(Vector3.zero, Vector3.up, Input.GetAxis("Mouse X") * 10);
        }

        // Reset Camera Position
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = new Vector3(30, 15, 0);
            transform.rotation = Quaternion.Euler(25, -90, 0);
            Camera.main.fieldOfView = 60;
        }

        // Zoom In and Out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - (scroll * 30), 30, 90);
        }
    }

}
