using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whoTurn : MonoBehaviour
{
    // Start is called before the first frame update
    public int whoturn=1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void turnPlayer(){
        whoturn*=-1;
        return;
    }
    public int getWhoTurn(){
        return whoturn;
    }
}
