using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class StartPlayGame : MonoBehaviourPunCallbacks{
    [SerializeField] Button startGameButton;
    [SerializeField] byte maxPlayers;

    public void JoinRandRoom(){
        if(PhotonNetwork.IsConnectedAndReady){
            PhotonNetwork.JoinRandomRoom(null,maxPlayers);
        }
    }
    public void CreateRoom(){
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        CreateRoom();
    }

    public override void OnJoinedRoom(){
        print("success");
        SceneManager.LoadScene("LoadingScene");
    }
    
}
