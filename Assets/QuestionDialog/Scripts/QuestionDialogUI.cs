using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionDialogUI : MonoBehaviour {

    public static QuestionDialogUI Instance { get; private set; }

    public whoTurn whoTurn_;

    private TextMeshProUGUI textMeshPro;
    private Button yesBtn;
    private Button noBtn;

    
   
    
    public void Awake() {
        Instance = this;

        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();
        

        Hide();
    }

    public void ShowQuestion(string questionText, Action yesAction, Action noAction) {
        
        gameObject.SetActive(true);
        //Debug.Log("test");
        textMeshPro.text = questionText;
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            Hide();
            yesAction();
            //whoTurn_.turnPlayer();
            Debug.Log("call");
            
        });
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => {
            Hide();
            noAction();
            
            Debug.Log("call");
            whoTurn_.turnPlayer();
        });
    }
    
    private void Hide() {
        gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
    private void Destroy(){
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}