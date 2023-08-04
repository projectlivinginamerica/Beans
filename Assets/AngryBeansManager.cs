using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AngryBeansManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackToTitleBtnPressed()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        Object.Destroy(gameObject);    
    }
}
