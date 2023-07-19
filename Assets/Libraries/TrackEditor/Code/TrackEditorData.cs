#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;

// should not be making more of these, uncomment to be able to in case of emergency
// [CreateAssetMenu(menuName = "TrackEditorData")]
[ExecuteInEditMode]
public class TrackEditorData : ScriptableObject
{
    public List<GameObject> trackSegmentPrefabs = new List<GameObject>();
    public string localPath;
    public string relativeDefaultTrackPath;
    public List<string> customTrackSegmentsPaths = new List<string>();

    [HideInInspector]
    public List<GameObject> tracks;

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
                Assert.IsTrue(go.GetComponent<TrackSegment>() != null);
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
                Assert.IsTrue(go.GetComponent<TrackSegment>() != null, "Track segment " + go.name + "is not properly formatted to be used as a segment! Ensure you are using the TrackSegment, TrackConnection, and TrackConnectionLocator components correctly.");
                trackSegmentPrefabs.Add(go);
            }
        }
    }

    public void CreateNewTrack()
    {

    }
}
#endif