using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountCamera : NetworkBehaviour
{

    public GameObject CameraMountPoint;

    void Start()
    {
        if(isLocalPlayer)
        {
            Camera playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
            Transform cameraTransform = playerCamera.gameObject.transform; //Find the main camera for the scene
            cameraTransform.parent = CameraMountPoint.transform; //Make the camera a child of the mount point
            cameraTransform.position = CameraMountPoint.transform.position; //Set position/rotation same as the mount point
            cameraTransform.rotation = CameraMountPoint.transform.rotation;

        }        
    }
}
