
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

    public void SetVehicle(GameObject inVehicle)
    {
        Vehicle = inVehicle;
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
