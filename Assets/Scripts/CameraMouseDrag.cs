using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMouseDrag : MonoBehaviour
{
    void Start()
    {
        CinemachineCore.GetInputAxis = CameraAxis;
    }
    public float CameraAxis(string axis)
    {
        if (axis == "Mouse X")
        {
            if (Input.GetMouseButton(1))
            {
                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        }
        else if (axis == "Mouse Y")
        {
            if (Input.GetMouseButton(1))
            {
                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }
        return UnityEngine.Input.GetAxis(axis);
    }
}
