using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource music;
    
    public void StopMusic()
    {
        if (music.isPlaying)
        {
            music.Stop();
        }
    }
}
