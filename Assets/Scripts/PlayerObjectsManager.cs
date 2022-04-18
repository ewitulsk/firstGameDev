using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectsManager : MonoBehaviour
{
    
    public Transform camLoc;
    public LayerMask lookingMask;
    int idObjectLookedAt;
    public float lookingDistance;
    public Transform gunMountPos;
    private GameObjectManager objMngr;

    private Weapon curWeapon;

    private List<Weapon> heldWeapons = new List<Weapon>();

    void Start()
    {
        
        GameObject _objMngr = GameObject.Find("ObjectManager");
        objMngr = (GameObjectManager) _objMngr.GetComponent(typeof(GameObjectManager));
        Debug.Log("Starting Objects Manager");
    }

    // Update is called once per frame
    void Update()
    {
        grabWeapon();
    }

    void switchWeapon()
    {
        
    }

    void grabWeapon()
    {
        idObjectLookedAt = lookingAtObject();

        if(Input.GetButtonDown("Grab") && isGrabbable(idObjectLookedAt))
        {
            Debug.Log("Grabbing: "+objMngr.getWeapon(idObjectLookedAt).name+" Id: "+idObjectLookedAt);
            Weapon grabbedWeapon = objMngr.deactivateWeapon(idObjectLookedAt);
            grabbedWeapon.gameObject.GetComponent<Rigidbody>().useGravity = false;
            grabbedWeapon.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            addWeaponToInv(grabbedWeapon);
            setActiveWeapon(grabbedWeapon);
        }
    }

    void fixRotation(Weapon weapon)
    {
        if(weapon.name == "Pistol")
        {
            weapon.gameObject.transform.Rotate(0, -90, 0);
        }
        if(weapon.name == "BigGun")
        {
            weapon.gameObject.transform.Rotate(0, 0, 0);
        }
    }

    void setActiveWeapon(Weapon weapon)
    {
        weapon.gameObject.transform.parent = gunMountPos;
        weapon.gameObject.transform.position = gunMountPos.position;
        weapon.gameObject.transform.rotation = gunMountPos.rotation;
        fixRotation(weapon);
    }

    void addWeaponToInv(Weapon weapon)
    {
        if(curWeapon != null)
        {
            curWeapon.gameObject.SetActive(false);
        }
        curWeapon = weapon;
        //heldWeapons.Add(weapon);
    }

    bool isGrabbable(int objectId)
    {
        if(objectId != -1)
        {
            return true;
        }
        return false;
    }

    int lookingAtObject()
    {
        RaycastHit hit;
        if(Physics.Raycast(camLoc.position, camLoc.TransformDirection(Vector3.forward), out hit, lookingDistance, lookingMask))
        {
            string tag = hit.transform.tag;
            
            if(tag == "Weapon"){
                WeaponInfo objScript = (WeaponInfo) hit.transform.gameObject.GetComponent(typeof(WeaponInfo));
                int id = objScript.getId();

                return id;
            }
        }
        return -1;
    }   
}
