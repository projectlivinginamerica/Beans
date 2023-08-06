using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturedArtists : MonoBehaviour
{
    [SerializeField] private GameObject[] ArtistInfo;

    // Start is called before the first frame update
    void Start()
    {
        
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
