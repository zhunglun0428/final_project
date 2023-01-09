
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class buyOrPay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gamecontrol;
    public Color[] color_list;
    int color_index; 
    //public Transform[] _property;
    public Transform[] property;
    public GameObject _property;
    public GameObject _road;
    public Transform[] road;
    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        //diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        road = _road.GetComponentsInChildren<Transform>(true);
        property = _property.GetComponentsInChildren<Transform>(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
}
