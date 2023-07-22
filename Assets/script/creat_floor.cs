using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creat_floor : MonoBehaviour
{
    [SerializeField] GameObject square_test;
    [SerializeField] float begin_x,begin_y,back_x,back_y,range_space_x,range_space_y;
    public void Spawn(){
        for(float i=begin_y;i>=back_y;i-=range_space_y){
            for(float j=begin_x;j<=back_x;j+=range_space_x){
                Instantiate(square_test,new Vector3(j,i,0),Quaternion.identity);
            }
        }
        Debug.Log("Creat");
    }
}
