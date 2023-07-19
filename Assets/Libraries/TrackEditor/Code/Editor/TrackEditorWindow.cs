using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Assertions;
using UnityEditor.SceneManagement;

public class TrackEditorWindow : EditorWindow
{
    public string outputDirectoryPath;
    private bool useDefaultAssets = true;
    private TrackEditorData trackData;

    private int selectedIndex;

    private string pathToEditorWindowLibraryRoot;
    private static readonly string relativePathToEditorScene = "/Scenes/TrackEditor.unity";
    [MenuItem("Tools/Track Editor")]
    public static void ShowWindow()
    {
        TrackEditorWindow wnd = GetWindow<TrackEditorWindow>();
        wnd.titleContent = new GUIContent("Track Editor");
        bool success = wnd.Init();
        if(!success) wnd.Close();
        else wnd.CreateGUI();
    }

    private bool Init() 
    {
        if(!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            return false;
        }

        string[] editorDataGuid = AssetDatabase.FindAssets("t:TrackEditorData");
        Assert.IsTrue(editorDataGuid.Length == 1, "There should be exactly one TrackEditorData asset in the entire project, please delete all but one, or create one if one does not exist.");
        string trackDataPath = AssetDatabase.GUIDToAssetPath(editorDataGuid[0]);
        Assert.IsTrue(trackDataPath != null);
        trackData = AssetDatabase.LoadAssetAtPath<TrackEditorData>(trackDataPath);
        Assert.IsTrue(trackData != null);
        string[] directories = trackDataPath.Split("/");
        trackDataPath = "";
        pathToEditorWindowLibraryRoot = "";
        for(int i = 0; i < directories.Length - 1; i++)
        {
            trackDataPath += directories[i] + "/";
            if(i < directories.Length - 2)
                pathToEditorWindowLibraryRoot += directories[i] + "/";
        }
        trackData.localPath = trackDataPath;
        trackData.PopulateSegments(useDefaultAssets);

        EditorSceneManager.OpenScene(pathToEditorWindowLibraryRoot + relativePathToEditorScene);
        return true;
    }

    public void CreateGUI()
    {
        if(trackData == null) return;
        rootVisualElement.Add(new TextField("Output Directory Path"));

        Button createTrackButton = new Button();
        createTrackButton.clicked += createNewTrack;
        createTrackButton.text = "Create New Track";

        rootVisualElement.Add(createTrackButton);

        ListView trackList = new ListView();
        trackList.makeItem = () => new Label();
        trackList.bindItem = (item, index) => { (item as Label).text = trackData.tracks[index].name; };
        trackList.reorderable = false;
        trackList.itemsSource = trackData.tracks;
        trackList.onSelectionChange += OnListSelectionChange;

        rootVisualElement.Add(trackList);

        Button editTrackButton = new Button();
        editTrackButton.clicked += editTrack;
        editTrackButton.text = "Edit Track";
        editTrackButton.SetEnabled(trackList.selectedItem != null);

        rootVisualElement.Add(editTrackButton);
    }

    private void createNewTrack()
    {
        int index = trackData.CreateNewTrack();
        GameObject parent = new GameObject("NewTrack");
        trackData.tracks[index].parentObject = parent;
        editTrack(index);
    }

    private void OnListSelectionChange(IEnumerable<object> value)
    {
        CreateGUI();
    }

    private void editTrack()
    {
    
    }

    private void editTrack(int index)
    {

    }


}
