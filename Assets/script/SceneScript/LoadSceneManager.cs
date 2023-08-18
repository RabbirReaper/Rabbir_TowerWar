using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class LoadSceneManager : MonoBehaviourPunCallbacks{
    [SerializeField] TMP_Text roomCurrentPlayerText;

    private void Start() {
        roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }
    public override void OnJoinedRoom(){
        roomCurrentPlayerText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

}
