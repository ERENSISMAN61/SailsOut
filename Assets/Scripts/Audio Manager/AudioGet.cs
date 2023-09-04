using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGet : MonoBehaviour
{


    AudioSource sourceAudio;

    public AudioClip shotAudio;


    void Start()
    {
        sourceAudio = GetComponent<AudioSource>();



    }

    public void PlayShotAudio()
    {
        sourceAudio.PlayOneShot(shotAudio);
    }






}
