using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    public whoTurn whoTurn_;
    public Transform Player1;
    public Transform Player2;
    public Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(whoTurn_.getWhoTurn()==1){
            transform.position = Player1.position + offset;
        }else{
            transform.position = Player2.position + offset;
        }
        
    }
}
