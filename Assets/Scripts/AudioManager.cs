using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("AudioManager is NULL!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private AudioClip _shopOpen, _pickUpSword;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL!");
        }
    }

    public void OpenShopSound()
    {
        _audioSource.clip = _shopOpen;
        _audioSource.Play();
    }

    public void GetSwordSound()
    {
        _audioSource.clip = _pickUpSword;
        _audioSource.Play();
    }    
}
