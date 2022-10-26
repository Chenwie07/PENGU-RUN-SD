using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public static PlaySFX instance;
    private AudioSource _audio;

    public AudioClip coinCollect;
    public AudioClip loseLevel;
    public AudioClip selectInterface;
    public AudioClip jump; 

    private void Awake()
    {
        instance = this;
        _audio = GetComponent<AudioSource>();
    }
    public void Jump()
    {
        _audio.PlayOneShot(jump); 
    }
    public void CoinCollect()
    {
        _audio.PlayOneShot(coinCollect); 
    }
    public void LoseLevelSound()
    {
        _audio.PlayOneShot(loseLevel); 
    }

    public void SelectUI()
    {
        _audio.PlayOneShot(selectInterface); 
    }



}
