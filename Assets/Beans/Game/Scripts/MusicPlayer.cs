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
        PlayRandomSong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayRandomSong()
    {
        MusicPlayerSource.Stop();
        if (SongList.Length == 0)
        {
            return;
        }
        MusicPlayerSource.clip = SongList[Random.Range(0, SongList.Length - 1)];
        MusicPlayerSource.Play();
    }

    public void PlayNextSong()
    {
        MusicPlayerSource.Stop();
        if (SongList.Length == 0)
        {
            return;
        }

        CurrentSongIndex = CurrentSongIndex + 1;
        if (CurrentSongIndex >= SongList.Length)
        {
            CurrentSongIndex = 0;
        }

        MusicPlayerSource.clip = SongList[CurrentSongIndex];
        MusicPlayerSource.Play();
    }

    public void PlayPrevSong()
    {
        MusicPlayerSource.Stop();
        if (SongList.Length == 0)
        {
            return;
        }

        CurrentSongIndex = CurrentSongIndex - 1;
        if (CurrentSongIndex <= 0)
        {
            CurrentSongIndex = SongList.Length - 1;
        }
        MusicPlayerSource.clip = SongList[CurrentSongIndex];
        MusicPlayerSource.Play();
    }

    public void TestButton()    
    {
            MusicPlayerImage.transform.position += Random.insideUnitSphere * 2;
    }
}
