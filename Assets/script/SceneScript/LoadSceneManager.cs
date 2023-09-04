using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
public class LoadSceneManager : MonoBehaviourPunCallbacks{
    [SerializeField] TMP_Text roomCurrentPlayerText;
    [SerializeField] AudioClip buttonClip;

    private void Start() {
        roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Update() {
        if(PhotonNetwork.IsConnected) roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        if(PhotonNetwork.IsMasterClient  && PhotonNetwork.CurrentRoom.PlayerCount == 3){// isbug
            PhotonNetwork.CurrentRoom.IsOpen = false;
            SceneManager.LoadScene("SampleScene");
        }
        
    }
    public void QuitGame(){
         SoundManager.main.PlaySound(buttonClip);
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
