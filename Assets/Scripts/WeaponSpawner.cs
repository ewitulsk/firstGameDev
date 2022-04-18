using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{

    public GameObjectManager objManager;
    public string spawnerName;
    public string weaponName;
    public Transform spawnLoc;
    public GameObject prefab;

    private int lastSpawnedId = -1;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnedId = objManager.spawnWeaponObj(weaponName, spawnLoc.position, prefab);
        Debug.Log(spawnerName+" Spawning First Weapon. Id: "+lastSpawnedId);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastSpawnedId != -1 && !objManager.objectActive(lastSpawnedId))
        {
            lastSpawnedId = objManager.spawnWeaponObj(weaponName, spawnLoc.position, prefab);
            Debug.Log(spawnerName+" Spawning New Weapon. Id: "+lastSpawnedId);
        }
    }


}
