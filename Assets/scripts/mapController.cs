using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour

{
    public GameObject BigMap;
    public GameObject smallMap;
    public GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnclickOpenMap(){
        BigMap.SetActive(true);
        smallMap.SetActive(false);
        bg.SetActive(false);
    }
    public void OnclickCloseMap(){
        BigMap.SetActive(false);
        smallMap.SetActive(true);
        bg.SetActive(true);
    }
}
