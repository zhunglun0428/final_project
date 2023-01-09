using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q)){
            QuestionDialogUI.Instance.ShowQuestion("確定要重新開始嗎?", () => {
            //P1_money = Convert.ToInt32(P1_moneyTxt.text);
                SceneManager.LoadScene("SampleScene");
            }, () => {
                
            });
        }
        
    }
}
