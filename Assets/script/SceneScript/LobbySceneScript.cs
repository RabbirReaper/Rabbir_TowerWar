using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class LobbySceneScript : MonoBehaviourPunCallbacks{
    [SerializeField] TMP_Text offlineOrOnline;
    [SerializeField] TMP_Text connectPlayerCount;
    private void Awake() {
        if(!PhotonNetwork.IsConnected){
            offlineOrOnline.text = "Offline";
            offlineOrOnline.color = Color.red;
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    private void Start() {
        if(PhotonNetwork.CurrentLobby == null){
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnConnectedToMaster(){
        offlineOrOnline.text = "Online";
        offlineOrOnline.color = Color.green;
        PhotonNetwork.JoinLobby();
    }
    private void Update() {
        if(PhotonNetwork.IsConnected){
            offlineOrOnline.text = "Online";
            offlineOrOnline.color = Color.green;
            connectPlayerCount.text = PhotonNetwork.CountOfPlayers.ToString();
        }
    }
    public void OuitGame(){
        Application.Quit();
    }
    
}
