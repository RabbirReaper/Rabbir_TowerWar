using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
public class LevelManager_script : MonoBehaviourPunCallbacks{
    public static LevelManager_script main;
    public Transform[] WayPoints_list;
    public float Interval=10;
    public float Income=50;
    public float Gold=100;
    public float Next_Income=0;
    [SerializeField] TextMeshProUGUI teamColor;

    public int hp = 20;
    [SerializeField] TextMeshProUGUI[] tmp_text;
    private PhotonView hpPhotonView,_pV;
    public Hashtable actorNumberAndColor = new();

    private void Awake() {
        main = this;
        
    }
    private void Start() {
        // actorNumberAndColor.Add(1,"Blue");
        // actorNumberAndColor.Add(2,"Red");
        // actorNumberAndColor.Add(3,"Green");
        if(PhotonNetwork.LocalPlayer.ActorNumber == 1){
            teamColor.text = "Blue";
            teamColor.color = Color.blue;
        }else if(PhotonNetwork.LocalPlayer.ActorNumber == 2){
            teamColor.text = "Red";
            teamColor.color = Color.red;
        }
        // teamColor.text = actorNumberAndColor[PhotonNetwork.LocalPlayer.ActorNumber].ToString();
        tmp_text[PhotonNetwork.LocalPlayer.ActorNumber-1].text = hp.ToString();
        hpPhotonView = tmp_text[PhotonNetwork.LocalPlayer.ActorNumber-1].GetComponent<PhotonView>();
        _pV = this.GetComponent<PhotonView>();
    }
    private void Update() {
        Next_Income+=Time.deltaTime;
        if(Next_Income > Interval+0.1){
            Gold+=Income;
            Next_Income=0;
        }
    }
    public void IncreaseCurrency(float amount){
        Income+=amount;
    } 
    public bool SpendCurrency(float amount){
        if(amount <= Gold){
            Gold-=amount;
            return true;
        }else{
            Debug.Log("You don't have enough money!");
            return false;
        }
    }

    public void HpUpdate(int x,Player _ownPlayer){
        hp+=x;
        Hashtable table = new();
        table.Add("hp",hp);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
        _pV.RPC("RPCHpUpdate",_ownPlayer,-x);
        if(hp <= 0){
            
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer,Hashtable changedProps){
        tmp_text[targetPlayer.ActorNumber-1].text = changedProps["hp"].ToString();
    }
    
    [PunRPC]
    public void RPCHpUpdate(int x,PhotonMessageInfo Info){
        hp+=x;
        Hashtable table = new();
        table.Add("hp",hp);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
        // Debug.Log(x);
    }
    
}
