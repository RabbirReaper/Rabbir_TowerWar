using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

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
    [SerializeField] TextMeshProUGUI[] team_text;
    private PhotonView hpPhotonView,_pV;
    public Hashtable actorNumberAndColor = new();
    [SerializeField] GameObject EnemyParent;
    [SerializeField] TMP_Text[] EnemyInStreetText;
    private int[] EnemyInStreetVal = new int[5];
    private int alivePLayer;
    public bool isEnd = false;
    [SerializeField] int towerCountLimit;
    [SerializeField] TextMeshProUGUI towerLimitUI;
    public int enemySpawnLimit;
    [SerializeField] TMP_Text enemySpawnLimitUI;
    private Stack<int> enemyIdStack = new();
    private int[] enemyIdArray = new int[2000];
    private int[] _enemyIdArray = new int[2000];
    [SerializeField] TextMeshProUGUI[]  opponentBuildCountText;
    
    private int[] opponentBuildCount = new int[6];

    private void Awake() {
        main = this;
        
    }
    private void Start() {
        _enemyIdArray[0] = 10;
        enemyIdArray[0] = -1;
        for(int i=1999;i>=1;i--){
            enemyIdStack.Push(i);
        }
        enemySpawnLimitUI.text = enemySpawnLimit.ToString();
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
        }else if(PhotonNetwork.LocalPlayer.ActorNumber == 3){
            teamColor.text = "Yellow";
            teamColor.color = Color.yellow;
        }
        team_text[PhotonNetwork.LocalPlayer.ActorNumber-1].text = hp.ToString();
        hpPhotonView = team_text[PhotonNetwork.LocalPlayer.ActorNumber-1].GetComponent<PhotonView>();
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
        lostUI.SetActive(true);
        lostUI.transform.Find("You Lose").gameObject.SetActive(true);
        // _pV.RPC("RPCalivePLayerUpdate",RpcTarget.Others);
        lostUI.transform.Find("Text").GetComponent<TMP_Text>().text = "Income: "+Income+"\nSummon: "+this.GetComponent<EnemySpawn>().Summon.ToString()+"\nKill enemy: "+this.GetComponent<EnemySpawn>().EnemiesDied.ToString()+"\nRank: "+alivePLayer.ToString();
        // alivePLayer--;
        // Destroy(EnemyParent);
        StartCoroutine(DisGame());
    }
    public void Surrender(){
        isEnd = true;
        YouLose();
    }
    public void QuitGame(){
        StartCoroutine(LeavingGame());
    }
    private IEnumerator LeavingGame(){
        if(PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();;
        while(PhotonNetwork.IsConnected){
            yield return null;
        }
        SceneManager.LoadScene("BeginScene");
    }
    private IEnumerator DisGame(){
        isEnd = true;
        foreach (Transform item in EnemyParent.transform){
            if(!item.GetComponent<Enemy_Script>().isDestory)
                item.GetComponent<Enemy_Script>().CorrectDied();
        }
        // yield return new WaitForSeconds(1f);
        // foreach (Transform item in EnemyParent.transform){
        //     item.GetComponent<Enemy_Script>().CorrectDied();
        // }
        // yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(0.3f);
        PhotonNetwork.Disconnect();
    }
    public void YouWin(){
        isEnd = true;
        lostUI.SetActive(true);
        lostUI.transform.Find("You Win").gameObject.SetActive(true);
        // Destroy(EnemyParent);
        StartCoroutine(DisGame());
        lostUI.transform.Find("Text").GetComponent<TMP_Text>().text = "Income: "+Income+"\nSummon: "+this.GetComponent<EnemySpawn>().Summon.ToString()+"\nKill enemy: "+this.GetComponent<EnemySpawn>().EnemiesDied.ToString()+"\nRank: "+alivePLayer.ToString();
    }
    
    public void HpUpdate(int x,Player _ownPlayer){
        if(hp <= 0 || alivePLayer == 1) return;
        hp+=x;
        Hashtable table = new();
        table.Add("hp",hp);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
        _pV.RPC("RPCHpUpdate",_ownPlayer,-x);
        if(hp <= 0){
            team_text[PhotonNetwork.LocalPlayer.ActorNumber-1].text = "0";
            isEnd = true;
            YouLose();
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer,Hashtable changedProps){
        team_text[targetPlayer.ActorNumber-1].text = changedProps["hp"].ToString();
    }
    
    [PunRPC]
    public void RPCHpUpdate(int x,PhotonMessageInfo Info){
        hp+=x;
        Hashtable table = new();
        table.Add("hp",hp);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
        // Debug.Log(x);
    }
    
    
    public override void OnPlayerLeftRoom(Player otherPlayer){
        alivePLayer--;
        Player[] playersInRoom = PhotonNetwork.PlayerList;
        foreach (Transform item in EnemyParent.transform){
            if(!Array.Exists(playersInRoom, player => player == item.GetComponent<Enemy_Script>().ownPlayer)){
                item.GetComponent<Enemy_Script>().CorrectDied();
            }
        }
        if(alivePLayer == 1){
            isEnd = true;
            YouWin();
        }
        team_text[otherPlayer.ActorNumber-1].text = 0.ToString();
    }
    
    public void UpdateEnemyStreet(Player _player,int idx,int val){
        _pV.RPC("RPCUpdateEnemyStreet",_player,idx,val);
    }
    [PunRPC]
    private void RPCUpdateEnemyStreet(int idx,int val){
        EnemyInStreetVal[idx] += val;
        EnemyInStreetText[idx].text = EnemyInStreetVal[idx].ToString();
    }

    

    public int GetEnemyId(){
        int temp = enemyIdStack.Pop();
        if(enemySpawnLimit <= 0){
            enemyIdStack.Push(temp);
            return -1;
        }
        _enemyIdArray[temp] = alivePLayer;
        enemySpawnLimit--;
        enemyIdArray[temp] = alivePLayer-1;
        enemySpawnLimitUI.text = enemySpawnLimit.ToString();
        return temp;
    }
    public void PushEnemyId(int id){
        enemyIdStack.Push(id);
    }

    public void EnemyIsDied(Player player,int _enemyId){
        _pV.RPC("RPCEnemyIsDied",player,_enemyId);
    }


    [PunRPC]
    private void RPCEnemyIsDied(int _enemyId){
        enemyIdArray[_enemyId]--;
        if(enemyIdArray[_enemyId] == _enemyIdArray[_enemyId]-2){
            enemySpawnLimit++;
            enemySpawnLimitUI.text = enemySpawnLimit.ToString();
        }
        if(enemyIdArray[_enemyId] == 0){
            enemyIdStack.Push(_enemyId);
        } 
    }

    public void TowerLimitAdd(int x){
        towerCountLimit += x;
        towerLimitUI.text = towerCountLimit.ToString();
    }
    public int GetTowerLimit(){
        return towerCountLimit;
    }



    public void UpdateOpponentBuildCount(int idx,int x){
        _pV.RPC("RPCUpdateOpponentBuildCount",RpcTarget.Others,idx,x);
    }
    [PunRPC]
    private void RPCUpdateOpponentBuildCount(int idx,int x){
        opponentBuildCount[idx] += x;
        opponentBuildCountText[idx].text = opponentBuildCount[idx].ToString();
    }

    // public void UpdateEnemyStreet(Player _player,int idx,int val){
    //     _pV.RPC("RPCUpdateEnemyStreet",_player,idx,val);
    // }
    // [PunRPC]
    // private void RPCUpdateEnemyStreet(int idx,int val){
    //     EnemyInStreetVal[idx] += val;
    //     EnemyInStreetText[idx].text = EnemyInStreetVal[idx].ToString();
    // }




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
