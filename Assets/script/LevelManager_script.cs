using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LevelManager_script : MonoBehaviourPunCallbacks{
    public static LevelManager_script main;
    public Transform[] WayPoints_list;
    public float Interval=10;
    public int Income=50;
    public int Gold=100;
    public float Next_Income=0;
    [SerializeField] TextMeshProUGUI teamColor;
    [SerializeField] GameObject lostUI;
    public int hp = 20;
    [SerializeField] TextMeshProUGUI[] tmp_text;
    private PhotonView hpPhotonView,_pV;
    public Hashtable actorNumberAndColor = new();
    [SerializeField] GameObject EnemyParent;
    [SerializeField] TMP_Text[] EnemyInStreetText;
    private int[] EnemyInStreetVal = new int[5];
    private int alivePLayer;
    public bool isEnd = false;

    private void Awake() {
        main = this;
        
    }
    private void Start() {
        PhotonNetwork.AutomaticallySyncScene = false;
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
        tmp_text[PhotonNetwork.LocalPlayer.ActorNumber-1].text = hp.ToString();
        hpPhotonView = tmp_text[PhotonNetwork.LocalPlayer.ActorNumber-1].GetComponent<PhotonView>();
        _pV = this.GetComponent<PhotonView>();
        for(int i=0;i<EnemyInStreetVal.Length;i++){
            EnemyInStreetVal[i] = 0;
        }
    }
    private void Update() {
        Next_Income+=Time.deltaTime;
        if(Next_Income > Interval+0.1){
            Gold+=Income;
            Next_Income=0;
        }
    }

    public void IncreaseGold(int amount){
        Gold+=amount;
    }
    public void IncreaseIncome(int amount){
        Income+=amount;
    } 
    public bool SpendCurrency(int amount){
        if(amount <= Gold){
            Gold-=amount;
            return true;
        }else{
            Debug.Log("You don't have enough money!");
            return false;
        }
    }


    public void YouLose(){
        isEnd = true;
        lostUI.SetActive(true);
        lostUI.transform.Find("You Lose").gameObject.SetActive(true);
        _pV.RPC("RPCalivePLayerUpdate",RpcTarget.Others);
        lostUI.transform.Find("Text").GetComponent<TMP_Text>().text = "Income: "+Income+"\nSummon: "+this.GetComponent<EnemySpawn>().Summon.ToString()+"\nKill enemy: "+this.GetComponent<EnemySpawn>().EnemiesDied.ToString()+"\nRank: "+alivePLayer.ToString();
        alivePLayer--;
        Destroy(EnemyParent);
        StopGame();
    }
    public void YouWin(){
        lostUI.SetActive(true);
        lostUI.transform.Find("You Win").gameObject.SetActive(true);
        Destroy(EnemyParent);
        StopGame();
        lostUI.transform.Find("Text").GetComponent<TMP_Text>().text = "Income: "+Income+"\nSummon: "+this.GetComponent<EnemySpawn>().Summon.ToString()+"\nKill enemy: "+this.GetComponent<EnemySpawn>().EnemiesDied.ToString()+"\nRank: "+alivePLayer.ToString();
    }
    public void StopGame(){
        Time.timeScale = 0.1f;
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
            isEnd = true;
            YouWin();
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer){
        alivePLayer--;
        if(alivePLayer == 1){
            isEnd = true;
            YouWin();
        }
        tmp_text[otherPlayer.ActorNumber-1].text = 0.ToString();
    }
    
    public void UpdateEnemyStreet(Player _player,int idx,int val){
        _pV.RPC("RPCUpdateEnemyStreet",_player,idx,val);
    }
    [PunRPC]
    private void RPCUpdateEnemyStreet(int idx,int val){
        EnemyInStreetVal[idx] += val;
        EnemyInStreetText[idx].text = EnemyInStreetVal[idx].ToString();
    }

    public void QuitGame(){
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("BeginScene");

        // StartCoroutine(WaitForLeaveAndLoadScene());
    }
    // public override void OnLeftRoom(){
    //     SceneManager.LoadScene("LobbyScene");
    // }

    // private IEnumerator WaitForLeaveAndLoadScene(){
    //     while (PhotonNetwork.InRoom){
    //         yield return null;
    //     }
        
    //     SceneManager.LoadScene("LobbyScene");
    // }
}
