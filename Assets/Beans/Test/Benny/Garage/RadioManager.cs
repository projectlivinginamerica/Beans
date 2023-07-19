using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class RadioManager : EditorWindow
{
    private Button _button1;
    private Button _button2;

    [MenuItem("Window/UI Toolkit/RadioManager")]
    public static void ShowExample()
    {
        RadioManager wnd = GetWindow<RadioManager>();
        wnd.titleContent = new GUIContent("RadioManager");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Beans/Test/Benny/Garage/RadioManager.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
 //var uiDocument = GetComponent<UIDocument>();
_button1 = rootVisualElement.Q("Button") as Button;
_button1.RegisterCallback<ClickEvent>(Button1Down);
    }

void Button1Down(ClickEvent evt)
{
Debug.Log("Here!");
    }
}