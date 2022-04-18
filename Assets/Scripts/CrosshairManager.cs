using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    //Recticles created using this tutorial https://www.youtube.com/watch?v=-7DIdKTNjfQ
    public GameObject gunReticle;
    public GameObject dotReticle;

    // Start is called before the first frame update
    void Start()
    {
        disableGunReticle();
    }

    // Update is called once per frame
    void Update()
    {

    }   

    void disableGunReticle()
    {
        gunReticle.SetActive(false);
        dotReticle.SetActive(true);
    }
}
