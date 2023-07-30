using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintManager : MonoBehaviour
{
    [SerializeField] private Button AddLayerButton;
    [SerializeField] private Button DeleteLayerButton;

    [SerializeField] private MaterialLayer[] MaterialLayers;
    [SerializeField] private GameObject Vehicle;

    private int NumLayers = 0;
    private int MaxLayers = 3;

    private List<Material> VehicleMaterials;

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
            AddLayerButton.transform.position = MaterialLayers[MaxLayers].ShowButton.transform.position;
        }
        else
        {
            AddLayerButton.interactable = true;
            AddLayerButton.transform.position = MaterialLayers[NumLayers].ShowButton.transform.position;
        }

        if (NumLayers == 0)
        {
            DeleteLayerButton.interactable = false;
        }
        else
        {
            DeleteLayerButton.interactable = true;
        }

        DeleteLayerButton.transform.position = AddLayerButton.transform.position + new Vector3(40, 0, 0);

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

    public void OnWheelColorChanged(Color newColor)
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
}
