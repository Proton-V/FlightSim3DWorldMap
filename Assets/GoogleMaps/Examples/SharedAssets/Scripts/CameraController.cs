using System;
using UnityEngine;
using UnityEngine.Events;


  [RequireComponent(typeof(Camera))]
  public class CameraController : MonoBehaviour {

    public bool IsControll = false;
    [Range(0.5f,5f)]
    public float mouseSensitivity = 0.5f;
    [HideInInspector]
    public float rotX, rotY, xVelocity, yVelocity;

    private void Update()
    {
        if(IsControll)
            LookAtMouse();
    }

    private void LookAtMouse()
    {
        rotX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        xVelocity = Mathf.Lerp(xVelocity, rotX, 5f * Time.deltaTime);
        yVelocity = Mathf.Lerp(yVelocity, rotY, 5f * Time.deltaTime);

        rotY = Mathf.Clamp(rotY, -90, 90);
        transform.localRotation = Quaternion.Euler(yVelocity, transform.localEulerAngles.y, 0);
        transform.transform.localEulerAngles += new Vector3(0, rotX, 0);
    }

}

