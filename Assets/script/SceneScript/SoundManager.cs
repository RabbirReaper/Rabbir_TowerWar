using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static SoundManager main;
    [SerializeField] private AudioSource musicSource,effectSorce;
    private void Awake() {
        if(main == null){
            main = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(AudioClip clip){
        effectSorce.PlayOneShot(clip);
    }

    public void ChangeMusicVolume(float value){
        musicSource.volume = value;
    }

    public void ChangeEffectVolume(float value){
        effectSorce.volume = value;
    }

    public float GetMusicVolume(){
        return musicSource.volume;
    }
    public float GetEffectVolum(){
        return effectSorce.volume;
    }

    
}
