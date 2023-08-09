using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class LobbySceneScript : MonoBehaviourPunCallbacks{
    [SerializeField] TMP_Text offlineOrOnline;
    [SerializeField] TMP_Text connectPlayerCount;
    private void Awake() {
        offlineOrOnline.text = "Offline";
        offlineOrOnline.color = Color.red;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster(){
        offlineOrOnline.text = "Online";
        offlineOrOnline.color = Color.green;
    }
    private void Update() {
        if(PhotonNetwork.IsConnected){
            connectPlayerCount.text = PhotonNetwork.PlayerList.Length.ToString();
        }
    }
    
}
