using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BeginSceneManager : MonoBehaviourPunCallbacks{
    private void Awake() {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster(){
        SceneManager.LoadScene("LobbyScene");
    }
    public void ReConnect(){
        PhotonNetwork.ConnectUsingSettings();
    }
}
