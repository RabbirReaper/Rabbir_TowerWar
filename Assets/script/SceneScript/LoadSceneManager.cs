using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
public class LoadSceneManager : MonoBehaviourPunCallbacks{
    [SerializeField] TMP_Text roomCurrentPlayerText;

    private void Start() {
        roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Update() {
        roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        if(PhotonNetwork.IsMasterClient  && PhotonNetwork.CurrentRoom.PlayerCount == 2){// isbug
            PhotonNetwork.CurrentRoom.IsOpen = false;
            SceneManager.LoadScene("SampleScene");
        }
        
    }
    public void QuitGame(){
        PhotonNetwork.Disconnect();
        

        // StartCoroutine(WaitForLeaveAndLoadScene());
    }
    public override void OnDisconnected(DisconnectCause cause){
        SceneManager.LoadScene("BeginScene");
    }
    // public override void OnJoinedRoom(){
    //     roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    //
    // }
    // public override void OnLeftRoom(){
    //     roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    // }

}
