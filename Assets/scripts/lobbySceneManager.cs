using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using System.Text;
using TMPro;
public class lobbySceneManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField]
    InputField inputRoomName;
    [SerializeField]
    InputField inputPlayerName;
    public TMP_Text roomListText;
    public GameObject img;
     
    void Start()
    {
        if(PhotonNetwork.CurrentLobby==null){
            PhotonNetwork.JoinLobby();
        }
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        print("join");
    }
    public string getRoomName(){
        string roomName = inputRoomName.text.Trim();
        return roomName;
    }
    public string getPlayerName(){
        string playerName = inputPlayerName.text.Trim();
        return playerName;
    }
    public void OnclickCreativeRoom(){
        string roomName = getRoomName();
        string playerName = getPlayerName();
        if(roomName.Length>0 && playerName.Length>0){
            PhotonNetwork.CreateRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
            //img.SetActive(true);
        }else{
            print("invalid room name!");
        }   
    }
    public void OnclickJoinRoom(){
        string roomName = getRoomName();
        string playerName = getPlayerName();
        if(roomName.Length>0 && playerName.Length>0){
            PhotonNetwork.JoinRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
            //img.SetActive(true);
        }else{
            print("invalid room name!");
        }   
    }
    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        
        print("roomjoin");
        SceneManager.LoadScene("Room");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        StringBuilder sb = new StringBuilder();
        foreach(RoomInfo roomInfo in roomList){
            if(roomInfo.PlayerCount>0){
                sb.AppendLine(roomInfo.Name);
            } 
        }
        roomListText.text = sb.ToString();
    }
}
