using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbySceneScript : MonoBehaviourPunCallbacks{
    [SerializeField] TMP_Text offlineOrOnline;
    [SerializeField] TMP_Text connectPlayerCount;
    
    private void Start() {
        if(PhotonNetwork.IsConnected == false){
            Debug.Log("You not Connection");
            SceneManager.LoadScene("BeginScene");
        }else{
            if(PhotonNetwork.CurrentLobby == null) PhotonNetwork.JoinLobby();
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
        // if(PhotonNetwork.IsConnectedAndReady) Debug.Log("I am ready");
        // else if(PhotonNetwork.IsConnected) Debug.Log("I am not ready");
        // else Debug.Log("I am not connect");
    }
    public void OuitGame(){
        Application.Quit();
    }
    
}
