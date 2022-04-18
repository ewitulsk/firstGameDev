using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon
{
    public int id;
    public string name;
    public Vector3 loc;
    public GameObject prefab;
    public bool active;

    public GameObject gameObject;

    public Weapon(int id, string name, Vector3 loc, GameObject prefab){
        this.id = id;
        this.name = name;
        this.loc = loc;
        this.prefab = prefab;
        this.active = true;
    }
}

public class GameObjectManager : MonoBehaviour
{
    public Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();
    public int weaponIdCtr = 2;

    public int spawnWeaponObj(string name, Vector3 loc, GameObject prefab)
    {
        
        Weapon newWeapon = new Weapon(this.weaponIdCtr, name, loc, prefab);
        weaponIdCtr += 1;
        weapons.Add(newWeapon.id, newWeapon);

        GameObject weaponObj = Instantiate(prefab, loc, Quaternion.identity);
        ((WeaponInfo) weaponObj.GetComponent(typeof(WeaponInfo))).setId(newWeapon.id);
        weapons[newWeapon.id].gameObject = weaponObj;

        return newWeapon.id;
    }

    public Weapon destroyWeapon(int id)
    {
        weapons[id].active = false;
        Destroy(weapons[id].gameObject);
        return weapons[id];
    }

    public Weapon deactivateWeapon(int id)
    {
        weapons[id].active = false;
        return weapons[id];
    }

    public Weapon getWeapon(int id){
        return weapons[id];
    }

    public bool objectActive(int id)
    {
        return weapons[id].active;
    }
}
