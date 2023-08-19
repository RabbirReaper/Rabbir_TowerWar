using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartPlayGame : MonoBehaviourPunCallbacks{
    [SerializeField] Button startGameButton;
    [SerializeField] byte expectedMaxPlayers;
    RoomOptions roomOptions = new();
    private void Start() {
        roomOptions.MaxPlayers = expectedMaxPlayers;
        roomOptions.IsVisible = true;
    }

    // public void JoinLoadingRoom(){
    //     if(PhotonNetwork.IsConnected){
    //        PhotonNetwork.JoinOrCreateRoom(null,roomOptions,null,null);
    //     }
    // }
    public void JoinLoadingRoom(){
        if (PhotonNetwork.IsConnected){
            // string roomName = GenerateRandomRoomName(); // 自行生成隨機房間名稱
            // PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null, null);
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message){
        print("Failed");
    }

    public override void OnJoinedRoom(){
        print("success");
        SceneManager.LoadScene("LoadingScene");
    }
    
}
