using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ProceduralTrackGenerator : MonoBehaviour
{
    [Serializable]
    class TrackPrefabInfo
    {
        public GameObject Prefab;
    }

    [SerializeField] TrackPrefabInfo[] PrefabList;
    [SerializeField] bool GenerateTrackNow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValidate()
    {
        if (GenerateTrackNow == true)
        {
            GenerateTrack();
        }
    }

    private void GenerateTrack()
    {
        Debug.Log("GenerateTrack Now!");
        GenerateTrackNow = false;

        GameObject.Instantiate(PrefabList[0].Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
        GameObject.Instantiate(PrefabList[0].Prefab, new Vector3(0, 0, 100), new Quaternion(0, 0, 0, 1));
    }
}
