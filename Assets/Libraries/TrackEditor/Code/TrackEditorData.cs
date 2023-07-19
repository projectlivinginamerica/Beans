#if UNITY_EDITOR
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;
using System.Linq;
using System;

// should not be making more of these, uncomment to be able to in case of emergency
//[CreateAssetMenu(menuName = "TrackEditorData")]
[ExecuteInEditMode]
public class TrackEditorData : ScriptableObject
{
    public List<GameObject> trackSegmentPrefabs = new List<GameObject>();
    
    [HideInInspector, SerializeField]
    public List<Track> tracks = new List<Track>();
    private int currentTrack = -1;

    public string localPath;
    public string relativeDefaultTrackPath;
    [HideInInspector, SerializeField]
    public List<string> customTrackSegmentsPaths = new List<string>();

    public void PopulateSegments(bool useDefaultAssets)
    {
        trackSegmentPrefabs.Clear();
        if(useDefaultAssets)
        {
            string[] defaultAssetGuids = UnityEditor.AssetDatabase.FindAssets("t:GameObject", new[] {localPath + "/" + relativeDefaultTrackPath});
            Assert.IsTrue(defaultAssetGuids != null);

            foreach(string guid in defaultAssetGuids)
            {
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
                trackSegmentPrefabs.Add(go);
            }
        }
        if(customTrackSegmentsPaths.Count > 0)
        {
            string[] customAssetGuids = UnityEditor.AssetDatabase.FindAssets("t:GameObject", customTrackSegmentsPaths.ToArray());
            Assert.IsTrue(customAssetGuids != null, "No assets found at any custom segment paths provided!");
            
            foreach(string guid in customAssetGuids)
            {
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
                trackSegmentPrefabs.Add(go);
            }
        }
    }

    public int CreateNewTrack()
    {
        Assert.IsTrue(currentTrack == -1);
        Track newTrack = new Track();
        tracks.Add(newTrack);
        currentTrack = tracks.Count - 1;
        return currentTrack;
    }

    public void EditTrack(int index)
    {
        Assert.IsTrue(currentTrack == -1);
        currentTrack = index;
        tracks[index].unsavedChanges = true;
    }

    public void DiscardChangesToCurrentTrack()
    {
        if(tracks[currentTrack].isNew)
        {
            tracks.RemoveAt(currentTrack);
        }
        else
        {

            tracks[currentTrack].unsavedChanges = false;
        }
    }

    public bool SaveCurrentTrack(string outputDirectory)
    {
        if(!Directory.Exists(outputDirectory))
        {
            Debug.Log("Output directory " + outputDirectory + " does not exist! Please create it or update the Output Directory Path and try again.");
            return false;
        }
         
        bool success = false;
        if(tracks[currentTrack].isNew)
        {
            Assert.IsTrue(tracks[currentTrack].prefabPath == null);
            if(tracks[currentTrack].parentObject != null)
            {
                Debug.Log("No track created! Can't save to disk.");
                return false;
            }
            string fileName = new string(tracks[currentTrack].name.Where(c => !Char.IsWhiteSpace(c)).ToArray()); // remove all whitespace from the name given
            tracks[currentTrack].prefabPath = outputDirectory + "/" + fileName + ".asset";
            tracks[currentTrack].prefabPath = AssetDatabase.GenerateUniqueAssetPath(tracks[currentTrack].prefabPath);
            PrefabUtility.SaveAsPrefabAssetAndConnect(tracks[currentTrack].parentObject, tracks[currentTrack].prefabPath, InteractionMode.UserAction, out success);
            if(success)
            {
                tracks[currentTrack].isNew = false;
                tracks[currentTrack].unsavedChanges = false;
            }
            else
            {
                tracks[currentTrack].prefabPath = null;
            }
        }
        else
        {
            Assert.IsTrue(tracks[currentTrack].prefabPath != null && tracks[currentTrack].parentObject != null);
            PrefabUtility.SavePrefabAsset(tracks[currentTrack].parentObject, out success);
            if(success)
            {
                tracks[currentTrack].unsavedChanges = false;
            }
        }
        
        return success;
    }
}

[System.Serializable]
public class Track
{
    public string name = "Unnamed Track";
    public GameObject parentObject = null;
    public List<GameObject> trackSegments = new List<GameObject>();
    public bool isNew = true;
    public bool unsavedChanges = false;
    public string prefabPath = null;
}

#endif