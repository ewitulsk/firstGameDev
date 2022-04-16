using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDisabled : MonoBehaviour
{
    
    public Camera c;

    // Start is called before the first frame update
    void Start()
    {
        c.enabled = false;
    }
}
