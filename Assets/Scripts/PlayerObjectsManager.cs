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
    public static int maxWeapons = 2;
    private GameObjectManager objMngr;
    private int curWeaponIndex = 0;
    private Weapon curWeapon;
    private Weapon[] heldWeapons = new Weapon[maxWeapons];

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
        switchWeapon();
        checkDropWeapon();
        shootWeapon();
    }

    void shootWeapon()
    {
        if(curWeapon != null && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Bang!");
            WeaponInfo weaponFuncs = (WeaponInfo) curWeapon.gameObject.GetComponent(typeof(WeaponInfo));
            weaponFuncs.shoot();
        }
    }

    void switchWeapon()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //Debug.Log("Switching Weapons Forward!");
            deactivateWeapon();
            curWeaponIndex++;
            if(curWeaponIndex >= heldWeapons.Length)
            {
                curWeaponIndex = 0;
            }
            //Debug.Log("Attempting Weapon Switch to Index: "+curWeaponIndex+" With heldWeapons length of: "+heldWeapons.Length);
            quickActivateWeapon();
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //Debug.Log("Switching Weapons Backward!");
            deactivateWeapon();
            curWeaponIndex--;
            if(curWeaponIndex < 0){
                curWeaponIndex = heldWeapons.Length-1;
            }
            //Debug.Log("Attempting Weapon Switch to Index: "+curWeaponIndex+" With heldWeapons length of: "+heldWeapons.Length);
            quickActivateWeapon();
        }
        
    }

    void checkDropWeapon()
    {
        if(Input.GetButtonDown("Drop"))
        {
            dropWeapon(curWeaponIndex);
        }
    }

    void dropWeapon(int weaponIndex){
        Weapon weapon = heldWeapons[weaponIndex];
        if(weapon != null)
        {
            weapon.gameObject.transform.parent = null;
            weapon.gameObject.GetComponent<Rigidbody>().useGravity = true;
            weapon.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            heldWeapons[weaponIndex] = null;
        }
    }

    void grabWeapon()
    {
        idObjectLookedAt = lookingAtObject();

        if(Input.GetButtonDown("Grab") && isGrabbable(idObjectLookedAt))
        {
            Debug.Log("Grabbing: "+objMngr.getWeapon(idObjectLookedAt).name+" Id: "+idObjectLookedAt);
            Weapon grabbedWeapon = objMngr.deactivateWeapon(idObjectLookedAt);
            addWeaponToInv(grabbedWeapon);
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

    //Deactivate the current weapon
    void deactivateWeapon()
    {
        Weapon weapon = heldWeapons[curWeaponIndex];
        if(weapon != null)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    void quickActivateWeapon()
    {
        Weapon weapon = heldWeapons[curWeaponIndex];
        if(weapon != null)
        {
            weapon.gameObject.SetActive(true);
        }
    }

    void removeWeaponPhysics(Weapon weapon)
    {        
        Debug.Log("Removing Weapon Physics");
        weapon.gameObject.GetComponent<Rigidbody>().useGravity = false;
        weapon.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        weapon.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        weapon.gameObject.transform.parent = gunMountPos;
        weapon.gameObject.transform.position = gunMountPos.position;
        weapon.gameObject.transform.rotation = gunMountPos.rotation;
        fixRotation(weapon);
        curWeapon = weapon;
    }

    void addWeaponToInv(Weapon weapon)
    {
        removeWeaponPhysics(weapon);
       for(int i=0; i<heldWeapons.Length; i++)
       {
           if(heldWeapons[i] == null)
           {
               deactivateWeapon();
               Debug.Log("Weapon == null");
               heldWeapons[i] = weapon;
               curWeaponIndex=i;
               quickActivateWeapon();
               return;
           }
       }
       //Drop current weapon if the array is full
       dropWeapon(curWeaponIndex);
       heldWeapons[curWeaponIndex] = weapon;
       return;
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
