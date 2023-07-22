using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_first : MonoBehaviour
{
    Transform target;
    int WayPointidx=0;
    [SerializeField] float speed;
    private void Start() {
        target = LevelManager_script.main.WayPoints_list[0];
    }
    void Update(){
        transform.position=Vector2.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,target.position) < 0.01f){
            if(WayPointidx<LevelManager_script.main.WayPoints_list.Length-1){
                target=LevelManager_script.main.WayPoints_list[++WayPointidx];
            }else{
                WayPointidx=0;
                transform.position=LevelManager_script.main.WayPoints_list[0].position;
                target=LevelManager_script.main.WayPoints_list[WayPointidx];
            }
        }
    }
}
