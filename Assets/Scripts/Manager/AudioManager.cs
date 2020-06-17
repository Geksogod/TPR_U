using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioItem[] audioItems = new AudioItem[] { };
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioItem startMusic;

    private void Awake()
    {
        if(audioSource==null)
            audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        //startMusic.Play(audioSource);
    }
    public void SetAudioVolue(float volume)
    {
        audioSource.volume = Mathf.Clamp(volume, 0,1);
    }

    public void PlayAudioSingle(string audioName)
    {
        foreach (AudioItem audioItem in audioItems)
        {
            if (string.Equals(audioItem.GetName(), audioName))
                audioItem.PlaySingle(audioSource);
        }
    }
}

[System.Serializable]
public class AudioItem
{
    [SerializeField]
    private string audioName;
    [SerializeField]
    private AudioClip audioClip;

    public void PlaySingle(AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void Play(AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public string GetName()
    {
        return audioName;
    }
}
