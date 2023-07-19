using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Assertions;

public class TrackEditorWindow : EditorWindow
{
    public string outputDirectoryPath;
    private bool useDefaultAssets = true;
    private TrackEditorData trackData;

    [MenuItem("Tools/Track Editor")]
    public static void ShowWindow()
    {
        TrackEditorWindow wnd = GetWindow<TrackEditorWindow>();
        wnd.titleContent = new GUIContent("Track Editor");
        wnd.Init();
    }

    private void Init() 
    {
        string[] editorDataGuid = AssetDatabase.FindAssets("t:TrackEditorData");
        Assert.IsTrue(editorDataGuid.Length == 1, "There should be exactly one TrackEditorData asset in the entire project, please delete all but one, or create one if one does not exist.");
        string trackDataPath = AssetDatabase.GUIDToAssetPath(editorDataGuid[0]);
        Assert.IsTrue(trackDataPath != null);
        trackData = AssetDatabase.LoadAssetAtPath<TrackEditorData>(trackDataPath);
        Assert.IsTrue(trackData != null);
        trackData.localPath = trackDataPath;
        trackData.PopulateSegments(useDefaultAssets);
    }

    public void CreateGUI()
    {
        rootVisualElement.Add(new TextField("Output Directory Path"));
    }

}
