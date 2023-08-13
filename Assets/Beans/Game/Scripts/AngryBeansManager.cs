using UnityEngine;
using UnityEngine.SceneManagement;
using KartGame.KartSystems;
using TMPro;
using UnityEngine.EventSystems;

public class AngryBeansManager : MonoBehaviour
{
    [SerializeField] private GameObject BoostUI;
    [SerializeField] private Transform MainCamera;
    [SerializeField] private Transform GameCamTransform;
    [SerializeField] private Transform PickBoostTransform;
    [SerializeField] private float LerpSpeed;
    [SerializeField] private ArcadeKart Kart;
    [SerializeField] private GameObject Score;

    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject RestartButton;

    private float T = 1.0f;
    private bool GasDisabled = false;

    enum eAngryBeansState
    {
        TitleScreen,
        PickBoost,
        Countdown,
        Run,
        Score,
    };

    eAngryBeansState AngryBeansState = eAngryBeansState.TitleScreen;

    // Start is called before the first frame update
    void Start()
    {
        Kart.SetCanAccelerate(false);

        if (GameObject.FindObjectOfType<AudioListener>() == null)
        {
            gameObject.AddComponent(typeof(AudioListener));
        }

        if (GameObject.FindObjectOfType<EventSystem>() == null)
        {
            gameObject.AddComponent(typeof(EventSystem));
            gameObject.AddComponent(typeof(StandaloneInputModule)); 
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch(AngryBeansState)
        {
            case eAngryBeansState.PickBoost : UpdatePickBoost(); break;
            case eAngryBeansState.Countdown : UpdateCountdown(); break;
            case eAngryBeansState.Run : UpdateRun(); break;
        }
     
        if (GasDisabled == false && Kart.transform.position.z > 35.0f)
        {
            GasDisabled = true;
            Kart.SetCanAccelerate(false);
        }
    }

    private bool UpdateCameraLerp()
    {
        bool bJustFinished = false;
        if (T < 1.0f)
        {
            T += Time.deltaTime * LerpSpeed;
            if (T >= 1.0f)
            {
                T = 1.0f;
                bJustFinished = true;
            }

            if (AngryBeansState == eAngryBeansState.PickBoost)
            {
                MainCamera.transform.position = Vector3.Slerp(GameCamTransform.position, PickBoostTransform.position, T);
                MainCamera.transform.rotation = Quaternion.Slerp(GameCamTransform.rotation, PickBoostTransform.rotation, T);
            }
            else
            {
                MainCamera.transform.position = Vector3.Slerp(PickBoostTransform.position, GameCamTransform.position, T);
                MainCamera.transform.rotation = Quaternion.Slerp(PickBoostTransform.rotation, GameCamTransform.rotation, T);
            }
        }
        return bJustFinished;
    }

    private void UpdatePickBoost()
    {
        if (UpdateCameraLerp())
        {
            BoostUI.SetActive(true);
        }
    }

    private void UpdateCountdown()
    {
        AngryBeansState = eAngryBeansState.Run;
        Kart.SetCanAccelerate(true);
    }

    private void UpdateRun()
    {
        UpdateCameraLerp();

        if (Kart.transform.position.z > 35.0f)
        {
            if (Kart.Rigidbody.velocity.magnitude < 1.0f)
            {
                AngryBeansState = eAngryBeansState.Score;
                Score.SetActive(true);
            }
        }
    }

    public void OnQuitGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("AngryBeans");
    }

    public void OnStartGame()
    {
        AngryBeansState = eAngryBeansState.PickBoost;
        T = 0;

        StartButton.SetActive(false);
        RestartButton.SetActive(true);
    }
    
    public void OnStartRun()
    {
        AngryBeansState = eAngryBeansState.Countdown;
        BeansAnimator BoostUIAnimator = BoostUI.GetComponent<BeansAnimator>();
        if (BoostUIAnimator != null)
        {
            BoostUIAnimator.enabled = true;
        }
        T = 0;
    }

    public void OnRestart()
    {
        SceneManager.UnloadSceneAsync("AngryBeans");
        SceneManager.LoadScene("AngryBeans", LoadSceneMode.Additive);
//        Object.Destroy(gameObject);    
    }
}
