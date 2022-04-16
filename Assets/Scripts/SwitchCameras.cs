using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        Camera.main.enabled = false;
        playerCamera.enabled = true;

    }
}
