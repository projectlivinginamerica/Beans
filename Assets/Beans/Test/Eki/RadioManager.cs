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
          _number = Random.Range(_startNumber, _endNumber);
          _textNumber.text = _number.ToString();

        if (SongList.Length > 0)
        {
            MusicSource.clip = SongList[_number];
            MusicSource.Play();
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public AudioClip[] SongList;

    public AudioSource MusicSource;

    public void ButtonPrevNext(bool Next) {
        
        if (Next) {
           
            _number++;

            if (_number > _endNumber) {
                _number = _startNumber;
            }

            _prev.interactable = true;
        }
        else {
            
            _number--;

            if (_number < _startNumber) {
                _number = _endNumber;
            }

            _next.interactable = true;
        }
        MusicSource.Stop();
        MusicSource.clip = SongList[_number];
        MusicSource.Play();
        _textNumber.text = _number.ToString();
        
    }
}
