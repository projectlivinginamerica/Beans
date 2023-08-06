using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeaturedArtists : MonoBehaviour
{
    [SerializeField] private GameObject[] ArtistInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<AudioListener>() == null)
        {
            gameObject.AddComponent(typeof(AudioListener));
        }

        if (GameObject.FindObjectOfType<EventSystem>() == null)
        {
            gameObject.AddComponent(typeof(EventSystem));
            gameObject.AddComponent(typeof(StandaloneInputModule)); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnArtistSelected(int idx)
    {
        if (idx < 0 || idx >= ArtistInfo.Length)
        {
            return;
        }
        ArtistInfo[idx].SetActive(true);
    }

    public void OnOpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
