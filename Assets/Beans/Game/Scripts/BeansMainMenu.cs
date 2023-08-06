using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeansMainMenu : MonoBehaviour
{
    [SerializeField] GameObject TitleGameObject;
    [SerializeField] GameObject MainMenuGameObject;

    enum eMenuState
    {
        TitleScreen,
        MainMenu,
    };
    eMenuState CurMenuState;

    // Start is called before the first frame update
    void Start()
    {
        CurMenuState = eMenuState.TitleScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToMainMenuBtnPressed()
    {
        if (CurMenuState != eMenuState.TitleScreen)
        {
            return;
        }
     
        TitleGameObject.SetActive(false);
        MainMenuGameObject.SetActive(true);
    }

    public void OnBackToTitleBtnPressed()
    {
        if (CurMenuState != eMenuState.MainMenu)
        {
            return;
        }
     
        TitleGameObject.SetActive(false);
        MainMenuGameObject.SetActive(true);
    }

    public void OnLoadGarage()
    {
        if (CurMenuState != eMenuState.TitleScreen)
        {
            return;
        }

        SceneManager.LoadScene("GarageScene", LoadSceneMode.Additive);
        Object.Destroy(gameObject);
    }

    public void OnLoadAngryBeans()
    {
        if (CurMenuState != eMenuState.TitleScreen)
        {
            return;
        }

        SceneManager.LoadScene("AngryBeans", LoadSceneMode.Additive);
        Object.Destroy(gameObject);
    }

    public void OnLoadFeaturedArtists()
    {
        if (CurMenuState != eMenuState.TitleScreen)
        {
            return;
        }

        SceneManager.LoadScene("FeaturedArtists", LoadSceneMode.Additive);
        Object.Destroy(gameObject);
    }
}
