using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Assertions;
using UnityEditor.SceneManagement;

public class TrackEditorWindow : EditorWindow
{
    private bool useDefaultAssets = true;
    private TrackEditorData trackData;

    private int selectedIndex;

    private string pathToEditorWindowLibraryRoot;
    private static readonly string relativePathToEditorScene = "/Scenes/TrackEditor.unity";

    bool editMode = false;

#region Editor Elements
    TextField outputDirectoryPathTextField;

    VisualElement trackListView;
    Button createTrackButton;
    Button editTrackButton;
    ListView trackList;

    VisualElement editTrackView;
    TextField trackName;
    Button saveTrackChanges;
    Button discardTrackChanges;
#endregion

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

        trackListView = new VisualElement();

        outputDirectoryPathTextField = new TextField("Output Directory Path");
        outputDirectoryPathTextField.SetValueWithoutNotify(trackData.outputDirectoryPath);

        trackListView.Add(outputDirectoryPathTextField);

        createTrackButton = new Button();
        createTrackButton.clicked += createNewTrack;
        createTrackButton.text = "Create New Track";

        trackListView.Add(createTrackButton);

        trackList = new ListView();
        trackList.makeItem = () => new Label();
        trackList.bindItem = (item, index) => { (item as Label).text = trackData.tracks[index].name; };
        trackList.reorderable = false;
        trackList.itemsSource = trackData.tracks;

        trackListView.Add(trackList);

        editTrackButton = new Button();
        editTrackButton.clicked += editTrack;
        editTrackButton.text = "Edit Track";
        editTrackButton.SetEnabled(trackList.selectedItem != null);

        trackListView.Add(editTrackButton);

        rootVisualElement.Add(trackListView);

        editTrackView = new VisualElement();
        
        trackName = new TextField("Track Name");
        editTrackView.Add(trackName);
        
        saveTrackChanges = new Button();
        saveTrackChanges.text = "Save Changes to Track";
        saveTrackChanges.clicked += saveTrack;
        editTrackView.Add(saveTrackChanges);

        discardTrackChanges = new Button();
        discardTrackChanges.text = "Discard Changes to Track";
        discardTrackChanges.clicked += discardChanges;
        editTrackView.Add(discardTrackChanges);

        editTrackView.SetEnabled(false);
        editTrackView.visible = false;

        rootVisualElement.Add(editTrackView);
    }


    void Update()
    {
        if(trackData != null && trackData.outputDirectoryPath != outputDirectoryPathTextField.text)
        {
            trackData.outputDirectoryPath = outputDirectoryPathTextField.text;
        }

        if(editMode)
        {   
            if(trackData.tracks[trackData.currentTrack].name != trackName.text)
                trackData.tracks[trackData.currentTrack].name = trackName.text;
        }
        else
        {
            editTrackButton.SetEnabled(trackList.selectedItem != null);
        }
    }

    private void createNewTrack()
    {
        int index = trackData.CreateNewTrack();
        GameObject parent = new GameObject("NewTrack");
        trackData.tracks[index].parentObject = parent;
        editTrack(index);
    }

    private void editTrack()
    {
        editTrack(trackList.selectedIndex);
    }

    private void editTrack(int index)
    {
        editMode = true;

        trackListView.visible = false;
        trackListView.SetEnabled(false);

        editTrackView.SetEnabled(true);
        editTrackView.visible = true;

        trackData.EditTrack(index);
    }

    private void saveTrack()
    {
        bool success = trackData.SaveCurrentTrack();
        if(success)
        {
            showTrackList();
        }
        else
        {

        }
    }

    private void showTrackList()
    {
        editMode = false;

        trackListView.visible = true;
        trackListView.SetEnabled(true);

        editTrackView.SetEnabled(false);
        editTrackView.visible = false;
    }

    private void discardChanges()
    {
        trackData.DiscardChangesToCurrentTrack();
        showTrackList();
    }


}
