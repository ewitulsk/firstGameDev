using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public LayerMask hitable;
    public int id;
    public GameObject playerCamera;

    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GameObject.Find("PlayerCamera");
        //Debug.Log(playerCamera.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getId(){
        return this.id;
    }

    public void setId(int id){
        this.id = id;
    }

    public void shoot(){
        Debug.Log("Bang! From WeaponInfo");

        if(playerCamera == null){
            Debug.Log("Reset Player Cam");
            playerCamera = GameObject.Find("PlayerCamera");
        }

        RaycastHit hit;
        
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), Color.blue, 20, true);
        Debug.Log(playerCamera.transform.position);
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, hitable))
        {

            Debug.DrawLine(playerCamera.transform.position, hit.transform.position, Color.red, 20);

            if(hit.transform.tag == "Enemy")
            {
                Debug.Log("Got A Hit!");
                PlayerInfo player = (PlayerInfo) hit.transform.GetComponent(typeof(PlayerInfo));
                player.hit();
            }
        }
    }
}
