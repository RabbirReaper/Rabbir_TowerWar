using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour{
    [SerializeField] AudioClip audioClip;
    private void Start() {
        SoundManager.main.PlaySound(audioClip);
    }
}
