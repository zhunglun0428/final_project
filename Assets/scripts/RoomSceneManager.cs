using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using System.Text;
using TMPro;

public class RoomSceneManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_Text roomText;
    public TMP_Text playerListText;
    public GameObject p1;
    public GameObject p2;
    public TMP_Text p1_name;
    public TMP_Text p2_name;
    [SerializeField] Button StartGameButton;
    void Start()
    {
        if(PhotonNetwork.CurrentRoom == null){
            SceneManager.LoadScene("lobby");
        }else{
           
           roomText.text = PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }
        if(PhotonNetwork.IsMasterClient){
            p1.SetActive(true);
            p2.SetActive(false);
            p1_name.text = PhotonNetwork.LocalPlayer.NickName;
        }else{
            p1.SetActive(false);
            p2.SetActive(true);
            p2_name.text = PhotonNetwork.LocalPlayer.NickName;
        }
        StartGameButton.interactable = PhotonNetwork.IsMasterClient;
    }
    
    // Update is called once per frame
    public void UpdatePlayerList(){
        StringBuilder sb = new StringBuilder();
        foreach(var kvp in PhotonNetwork.CurrentRoom.Players){
            sb.AppendLine(kvp.Value.NickName);
        }
        
        playerListText.text = sb.ToString();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void OnClickLeft(){
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("lobby");
    }
    public void OnClickStartGame(){
        SceneManager.LoadScene("SampleScene");
    }
    
}
