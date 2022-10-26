using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>(); 
    }

    private void Start()
    {
        _source.Play(); 
        _source.loop = true; 
    }
}
