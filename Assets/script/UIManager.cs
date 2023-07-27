using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour{
    public static UIManager main;
    bool  isHoveringUI=false;
    private void Awake() {
        main = this;
    }

    public void SetHoveringStatie(bool state){
        isHoveringUI = state;
    }

    public bool IsHoveringUI(){
        return isHoveringUI;
    }
}
