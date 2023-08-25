using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class EnemySpawn : MonoBehaviourPunCallbacks{
    [SerializeField] GameObject[] Enemy_list;
    [SerializeField] GameObject EnemyParent;
    private PhotonView _pV;
    int EnemiesAlive=0,EnemiesDied=0;
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
        EnemiesAlive--;
        EnemiesDied++;
    }
    public void SpawnEnemy(int x){
        _pV.RPC("RPCSpawnEnemy",RpcTarget.Others,x);
    }
    
    // public void SpawnEnemy(int x){ //test
    //     GameObject tempEnemy = Instantiate(Enemy_list[x],new Vector3(-22f,Random.Range(7f, 13f),0f),Quaternion.identity);
    //     EnemiesAlive++;
    // }
    
    [PunRPC]
    void RPCSpawnEnemy(int x,PhotonMessageInfo Info){
        // GameObject tempEnemy = Instantiate(Enemy_list[x],LevelManager_script.main.WayPoints_list[0].position,Quaternion.identity);
        GameObject tempEnemy = Instantiate(Enemy_list[x],new Vector3(-22f,Random.Range(7f, 13f),0f),Quaternion.identity);
        tempEnemy.transform.SetParent(EnemyParent.transform);
        // tempEnemy.GetComponent<Renderer>().material.color = color[Info.Sender.ActorNumber-1];
        tempEnemy.GetComponent<Enemy_Script>().ownPlayer = Info.Sender;
        EnemiesAlive++;
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
