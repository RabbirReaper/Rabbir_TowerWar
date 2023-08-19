using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameSceneManager : MonoBehaviourPunCallbacks{
    public GameObject gameScene;
    private void Start() {
        if(PhotonNetwork.IsMasterClient){
            int t=1;
            foreach(var x in PhotonNetwork.PlayerList){
                print(t++);
            }
        }
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNumberAssigner : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // 確保 Photon Network 已連線並且已經準備好進行遊戲操作

            // 創建一個 Hashtable 用於設置玩家的自定義屬性
            ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();

            // 將玩家的編號（Actor Number）設定為自定義屬性 "PlayerNumber"
            playerCustomProperties["PlayerNumber"] = PhotonNetwork.LocalPlayer.ActorNumber;

            // 使用 SetCustomProperties 方法將自定義屬性設定到本地玩家（LocalPlayer）
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);
        }
    }

    // 假設你需要在遊戲中知道某個玩家的編號
    public int GetPlayerNumber(Player player)
    {
        object playerNumber;
        if (player.CustomProperties.TryGetValue("PlayerNumber", out playerNumber))
        {
            return (int)playerNumber;
        }
        return -1; // 如果找不到編號，返回 -1
    }
}
*/
