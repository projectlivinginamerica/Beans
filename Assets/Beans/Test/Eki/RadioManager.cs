using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioManager : MonoBehaviour {

    public Button _prev;
    public Button _next;

    public int _startNumber = -5;
    public int _number;
    public int _endNumber = 5;
    
    public TextMeshProUGUI _textNumber;


    // Start is called before the first frame update
    void Start() {
          _number = _startNumber;
          _textNumber.text = _number.ToString();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ButtonNext() {
        if (_number < _endNumber) {
            _number++;
            _textNumber.text = _number.ToString();
        }
    }

    public void ButtonPrev() {
        if (_number > _startNumber) {
            _number--;
            _textNumber.text = _number.ToString();
        }
    }

    public void ButtonPrevNext(bool Next) { 
        
        if (Next) {
           
            _number++;

            _prev.interactable = true;

            if (_number < _endNumber) {
                _next.interactable = true;
            }
            else {
                _next.interactable = false;
            }
        }
        else {
            
            _number--;

            _next.interactable = true;

            if (_number > _startNumber) {
                _prev.interactable = true;
            }
            else {
                _prev.interactable = false;
            }
        }
        _textNumber.text = _number.ToString();
    }
}