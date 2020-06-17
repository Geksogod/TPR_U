using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTarget : MonoBehaviour
{
    [SerializeField]
    private string audioName;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = audioManager == null ? GameObject.FindObjectOfType<AudioManager>(): audioManager;
    }

    public void PlayAudio()
    {
        audioManager.PlayAudioSingle(audioName);
    }
}
