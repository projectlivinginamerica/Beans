using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageManager : MonoBehaviour
{
    [SerializeField] private Button AddLayerButton;
    [SerializeField] private Button DeleteLayerButton;

    [SerializeField] private MaterialLayer[] MaterialLayers;
    [SerializeField] private GameObject Vehicle;

    private int NumLayers = 0;
    private int MaxLayers = 3;

    private List<Material> VehicleMaterials;
    private MaterialLayer ActiveLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (Vehicle != null)
        {
            SkinnedMeshRenderer[] Meshes = Vehicle.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (Meshes == null)
            {
                return;
            }

            VehicleMaterials = new List<Material>();
            for (int i = 0; i < Meshes.Length; i++)
            {
                SkinnedMeshRenderer meshRenderer = Meshes[i];
                var curList = new List<Material>();
                meshRenderer.GetMaterials(curList);
                VehicleMaterials.AddRange(curList);
            }
        }

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
        
    }

   
    void PopulatePaintFields()
    {
        if (NumLayers == 3)
        {
            AddLayerButton.interactable = false;
      //      AddLayerButton.transform.position = MaterialLayers[MaxLayers].ShowButton.transform.position;
        }
        else
        {
            AddLayerButton.interactable = true;
            //AddLayerButton.transform.position = MaterialLayers[NumLayers].ShowButton.transform.position + new Vector3(0, 0, 0);
        }

        if (NumLayers == 0)
        {
            DeleteLayerButton.interactable = false;
        }
        else
        {
            DeleteLayerButton.interactable = true;
        }

       // DeleteLayerButton.transform.position = AddLayerButton.transform.position + new Vector3(40, 0, 0);

        int i = 0;
        for (i = 0; i < NumLayers; i++)
        {
            MaterialLayers[i].gameObject.SetActive(true);
            MaterialLayers[i].SetVehicle(Vehicle);
        }
        for (; i < MaxLayers; i++)
        {
            MaterialLayers[i].gameObject.SetActive(false);
            MaterialLayers[i].SetVehicle(Vehicle);
        }
    }

    public void OnAddLayer()
    {
        if (NumLayers == MaxLayers)
        {
            return;
        }
        NumLayers++;
        PopulatePaintFields();

        if (ActiveLayer == null)
        {
            ActiveLayer = MaterialLayers[0];
        }
    }

    public void OnRemoveLayer()
    {
        if (NumLayers == 0)
        {
            return;
        }
        NumLayers--;
        PopulatePaintFields();
    }
/*
    */

    public void OnPickerColorChanged(Color newColor, int mode)
    {
        if (mode == 0)
        {
            SetVehicleColor(newColor);
        }
        else if (mode == 1)
        {
            SetWheelColor(newColor);
        }
    }

    private void SetVehicleColor(Color newColor)
    {
        if (VehicleMaterials == null)
        {
            return;
        }

        foreach(Material mat in VehicleMaterials)
        {
            mat.SetColor("_BaseColor", newColor);
        }
    }

    public void SetWheelColor(Color newColor)
    {
        if (VehicleMaterials == null)
        {
            return;
        }

        foreach(Material mat in VehicleMaterials)
        {
            mat.SetColor("_WheelColor", newColor);
        }
    }

    public void OnBackToMainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("GarageScene");
    }
}
