using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class EnemySpawn : MonoBehaviourPunCallbacks{
    [SerializeField] GameObject[] Enemy_list;
    public GameObject EnemyParent;
    private PhotonView _pV;
    public int Summon=0,EnemiesDied=0;
    Color[] color = new Color[6];
    public static UnityEvent onEnemyDestory = new();
    private void Awake() {
        onEnemyDestory.AddListener(EnemyDestoryed);
        
    }
    private void Start() {
        color[0]=new Color(0.1f,0.05f,0.8f,1); //Blue
        color[1]=new Color(0.8f,0.05f,0.1f,1); //Red
        color[2]=new Color(0.1f,0.8f,0.1f,1); //Green
        _pV = this.GetComponent<PhotonView>();
    }

    void EnemyDestoryed(){
        EnemiesDied++;
    }
    public void SpawnEnemy(int x){
        if(LevelManager_script.main.isEnd) return;
        if(Enemy_list[x].GetComponent<Enemy_Script>().cost > LevelManager_script.main.Gold) return;
        int temp = LevelManager_script.main.GetEnemyId();
        if(temp == -1) return; // enemySpawnLimit <= 0
        LevelManager_script.main.SpendCurrency(Enemy_list[x].GetComponent<Enemy_Script>().cost);
        LevelManager_script.main.IncreaseIncome(Enemy_list[x].GetComponent<Enemy_Script>().currencyWorth);
        Summon++;
        _pV.RPC("RPCSpawnEnemy",RpcTarget.Others,x,temp);
    }
    
    // public void SpawnEnemy(int x){ //test
    //     GameObject tempEnemy = Instantiate(Enemy_list[x],new Vector3(-22f,Random.Range(7f, 13f),0f),Quaternion.identity);
    //     EnemiesAlive++;
    // }
    
    [PunRPC]
    void RPCSpawnEnemy(int x,int id,PhotonMessageInfo Info){
        if(LevelManager_script.main.isEnd) return;
        // GameObject tempEnemy = Instantiate(Enemy_list[x],LevelManager_script.main.WayPoints_list[0].position,Quaternion.identity);
        GameObject tempEnemy = Instantiate(Enemy_list[x],new Vector3(-22f,Random.Range(7f, 13f),0f),Quaternion.identity);
        tempEnemy.transform.SetParent(EnemyParent.transform);
        tempEnemy.GetComponent<Enemy_Script>().ownPlayer = Info.Sender;
        tempEnemy.GetComponent<Enemy_Script>().SetEnemyId(id);
        tempEnemy.GetComponent<Enemy_Script>().hpUI.color = color[Info.Sender.ActorNumber-1];
        LevelManager_script.main.UpdateEnemyStreet(Info.Sender,0,1);
    }



    // public void HpUpdate(int x,Player _ownPlayer){
    //     _pV.RPC("RPCHpUpdate",_ownPlayer,-x);
    // }

    // [PunRPC]
    // public void RPCHpUpdate(int x,PhotonMessageInfo Info){
    //     // hp+=x;
    //     // Hashtable table = new();
    //     // table.Add("hp",hp);
    //     // PhotonNetwork.LocalPlayer.SetCustomProperties(table);
    //     Debug.Log(x);
    // }
}
