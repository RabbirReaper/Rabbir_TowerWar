using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class LoadSceneManager : MonoBehaviourPunCallbacks{
    [SerializeField] TMP_Text roomCurrentPlayerText;

    private void Start() {
        roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Update() {
        roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        if(PhotonNetwork.IsMasterClient  && PhotonNetwork.CurrentRoom.PlayerCount == 2){// isbug
            SceneManager.LoadScene("SampleScene");
        }
        
    }
    // public override void OnJoinedRoom(){
    //     roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    //     if(2 == PhotonNetwork.CurrentRoom.PlayerCount){

    //     }
    // }
    // public override void OnLeftRoom(){
    //     roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    // }

}
