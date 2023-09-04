using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounceOnClick : MonoBehaviour{
    [SerializeField] AudioClip audioClip;
    public void Onclick() {
        SoundManager.main.PlaySound(audioClip);
    }
}
