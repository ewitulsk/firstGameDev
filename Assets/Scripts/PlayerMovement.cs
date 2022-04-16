using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float mouseSensitivity = 200f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    //LayerMask controls what objects we want to check for
    public LayerMask groundMask;

    public Transform playerBody;

    Vector3 velocity;
    bool isGrounded;
    
   
    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            //casts an invisible sphere around the groundCheck object (defined in the Player in editor)
            //sphere size is groundDistance var
            //groundMask is what it is looking for a collision with
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            //If player is grounded, and the velocity is still moving downward
            if(isGrounded && velocity.y < 0)
            {
                //isGrounded may trigger before the player is actually on the ground, thus, giving him a small but constant velocity downward is necessary
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            playerBody.Rotate(Vector3.up * mouseX);

            //This creates a movement that is relative to the direction the player is facing.
            Vector3 move = transform.right * x + transform.forward * z;

            //Time.deltaTime makes move speed dependant on frame rate.
            controller.Move(move * speed * Time.deltaTime);

            //v needed to jump h = sqrt(h*-2*g)
            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            
            velocity.y += gravity * Time.deltaTime;
            //v = 1/2 g * t^2... so we multiply by time once more, ignore the 1/2 tho lol
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
