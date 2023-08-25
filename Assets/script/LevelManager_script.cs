using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.UI;

public class LevelManager_script : MonoBehaviourPunCallbacks{
    public static LevelManager_script main;
    public Transform[] WayPoints_list;
    public float Interval=10;
    public float Income=50;
    public float Gold=100;
    public float Next_Income=0;
    [SerializeField] TextMeshProUGUI teamColor;
    [SerializeField] GameObject lostUI;
    public int hp = 20;
    [SerializeField] TextMeshProUGUI[] tmp_text;
    private PhotonView hpPhotonView,_pV;
    public Hashtable actorNumberAndColor = new();
    [SerializeField] GameObject EnemyParent;
    private int alivePLayer;
    public bool isLose = false;

    private void Awake() {
        main = this;
        
    }
    private void Start() {
        // actorNumberAndColor.Add(1,"Blue");
        // actorNumberAndColor.Add(2,"Red");
        // actorNumberAndColor.Add(3,"Green");
        alivePLayer = PhotonNetwork.CurrentRoom.PlayerCount;
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


    public void YouLose(){
        lostUI.SetActive(true);
        isLose = true;
        lostUI.transform.Find("You Lose").gameObject.SetActive(true);
        _pV.RPC("RPCalivePLayerUpdate",RpcTarget.Others);
        lostUI.transform.Find("Text").GetComponent<TMP_Text>().text = "Income: "+Income+"\nSummon: "+this.GetComponent<EnemySpawn>().Summon.ToString()+"\nKill enemy: "+this.GetComponent<EnemySpawn>().EnemiesDied.ToString()+"\nRank: "+alivePLayer.ToString();
        alivePLayer--;
        Destroy(EnemyParent);
        StartCoroutine(StopGame());
    }
    public IEnumerator StopGame(){
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }

    public void HpUpdate(int x,Player _ownPlayer){
        if(hp <= 0 || alivePLayer == 1) return;
        hp+=x;
        Hashtable table = new();
        table.Add("hp",hp);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
        _pV.RPC("RPCHpUpdate",_ownPlayer,-x);
        if(hp <= 0){
            tmp_text[PhotonNetwork.LocalPlayer.ActorNumber-1].text = "0";
            YouLose();
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
    
    [PunRPC]
    public void RPCalivePLayerUpdate(){
        alivePLayer--;
        if(alivePLayer == 1){
            isLose = true;
            lostUI.SetActive(true);
            lostUI.transform.Find("You Win").gameObject.SetActive(true);
            Destroy(EnemyParent);
            StartCoroutine(StopGame());
            lostUI.transform.Find("Text").GetComponent<TMP_Text>().text = "Income: "+Income+"\nSummon: "+this.GetComponent<EnemySpawn>().Summon.ToString()+"\nKill enemy: "+this.GetComponent<EnemySpawn>().EnemiesDied.ToString()+"\nRank: "+alivePLayer.ToString();
        }
    }
    
    
}
