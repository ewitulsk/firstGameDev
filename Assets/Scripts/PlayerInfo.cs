using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public GameObject enemy;

    public bool kill;
    public bool unKill;

    // Start is called before the first frame update
    void Start()
    {
        enemy.transform.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        if(kill){
            setColor(Color.red);
            kill = false;
        }
        if(unKill){
            setColor(Color.blue);
            unKill = false;
        }
    }

    void setColor(Color _color){
        var enemyRender = enemy.GetComponent<Renderer>();
        enemyRender.material.SetColor("_Color", _color);
    }

    public void hit(){
        kill = true;
    }


}
