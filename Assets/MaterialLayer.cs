
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaterialLayer : MonoBehaviour
{
    public UnityEngine.UI.Toggle ShowButton;
    public FlexibleColorPicker ColorPicker;

    private GameObject Vehicle;
    private List<Material> VehicleMaterials;

    private Color MaterialColor;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnColorWheelButton()
    {
        Debug.Log("OnColorWheelButton");
        ColorPicker.gameObject.SetActive(true);
        ColorPicker.SetColor(MaterialColor);
    }

    public void OnTextureButton()
    {
        Debug.Log("OnTexturePressed()");
    }

    public void OnMaterialButton()
    {
        Debug.Log("OnMaterialPressed()");
    }

    public void OnColorWheelClosed(bool bCanceled)
    {
        if (bCanceled)
        {
            ColorPicker.gameObject.SetActive(false);
            return;
        }

        SetVehicleColor(ColorPicker.GetColor());
    }

    public void OnColorWheelUpdate(Color newColor)
    {
        SetVehicleColor(newColor);
    }

    public void SetVehicle(GameObject inVehicle)
    {
        Vehicle = inVehicle;
        MeshRenderer[] Meshes = Vehicle.GetComponentsInChildren<MeshRenderer>();
        if (Meshes == null)
        {
            return;
        }

        VehicleMaterials = new List<Material>();
        for (int i = 0; i < Meshes.Length; i++)
        {
            MeshRenderer meshRenderer = Meshes[i];
            var curList = new List<Material>();
            meshRenderer.GetMaterials(curList);
            VehicleMaterials.AddRange(curList);
        }
    }

    private void SetVehicleColor(Color newColor)
    {
        foreach(Material mat in VehicleMaterials)
        {
            mat.SetColor("_BaseColor", newColor);
        }
    }
}
