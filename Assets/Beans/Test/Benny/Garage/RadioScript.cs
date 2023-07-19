using UnityEngine;
using UnityEngine.UIElements;

public class RadioScript : MonoBehaviour
{
    private Button _button1;
    private Button _button2;


    public void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _button1 = uiDocument.rootVisualElement.Q("Forward") as Button;


        _button1.RegisterCallback<ClickEvent>(Button1Down);

     //   var _inputFields = uiDocument.rootVisualElement.Q("input-message");
      //  _inputFields.RegisterCallback<ChangeEvent<string>>(InputMessage);
    }

void Button1Down(ClickEvent evt)
{
Debug.Log("Here!");
    }
}