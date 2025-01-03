using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum LookMode { LookAt, LookAtInverted,CameraFoward, CameraForwardInverted}

    [SerializeField] LookMode lookMode;

    private void LateUpdate()
    {
        switch (lookMode) 
        {
            case LookMode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case LookMode.LookAtInverted:
                Vector3 DirToCam =  transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + DirToCam); 
                break;
            case LookMode.CameraFoward:
                transform.forward = Camera.main.transform.forward;
                break;
            case LookMode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
