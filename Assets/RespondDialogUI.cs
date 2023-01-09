using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RespondDialogUI : MonoBehaviour {

    public static RespondDialogUI Instance { get; private set; }



    public whoTurn whoTurn_;

    private TextMeshProUGUI textMeshProForRes;
    private Button okBtn;
    private Button noBtn;
    private void Awake() {
        Instance = this;

        textMeshProForRes = transform.Find("ResText").GetComponent<TextMeshProUGUI>();
        okBtn = transform.Find("OkBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();

        Hide();
    }

    
    public void ShowRespond(string respondText, Action okAction, Action noAction) {
        gameObject.SetActive(true);
        
        textMeshProForRes.text = respondText;
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(() => {
            Hide();
            okAction();
            Debug.Log("ok");
            whoTurn_.turnPlayer();
        });
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => {
            Hide();
            noAction();
            whoTurn_.turnPlayer();
        });
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
    private void Destroy(){
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
    
}
