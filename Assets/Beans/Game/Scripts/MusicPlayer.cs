using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image MusicPlayerImage;
    [SerializeField] private AudioClip[] SongList;
    [SerializeField] private AudioSource MusicPlayerSource;

    [SerializeField]private int CurrentSongIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (SongList.Length == 0)
        {
            return;
        }
        MusicPlayerSource.clip = SongList[Random.Range(0, SongList.Length - 1)];
        MusicPlayerSource.Play();
    }
}
