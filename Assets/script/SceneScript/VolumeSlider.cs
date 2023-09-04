using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour{
    [SerializeField] Slider slider;
    [SerializeField] bool isMusicOREffect;
    // 保存Slider的值

    void Start(){
        if(isMusicOREffect){
            slider.value = SoundManager.main.GetMusicVolume();
            slider.onValueChanged.AddListener(val => SoundManager.main.ChangeMusicVolume(val));
        }else{
            slider.value = SoundManager.main.GetEffectVolum();
            slider.onValueChanged.AddListener(val => SoundManager.main.ChangeEffectVolume(val));
        }
        
    }

    
}
