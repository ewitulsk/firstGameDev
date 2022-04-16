using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitivity = 200f;

    public Transform playerHead;
    
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Locks the cursor... very nice
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            //Time.deltaTime is so that our looking is never faster then our frame rate.
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //Clamping means limiting, this limits the rotation of the mouseY so you cant look too far above your head
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //Quaternions are used for rotations, idk anything more
            Quaternion rotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerHead.localRotation = rotation;
        }
    }
}
