using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
   void HandleMovement()
   {
       float movementSpeed = .1f;
       if(isLocalPlayer)
       {
           float moveHorizontal = Input.GetAxis("Horizontal");
           float moveVertical = Input.GetAxis("Vertical");
           Vector3 movement = new Vector3(moveHorizontal*movementSpeed, moveVertical*movementSpeed, 0);
           transform.position = transform.position + movement;
       }
   }

   void Update()
   {
       HandleMovement();
   }
}
