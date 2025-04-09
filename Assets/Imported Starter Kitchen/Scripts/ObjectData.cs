using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;
    public List<Material> materials = new List<Material>();

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
    }
    private void OnEnable()
    {
        UIManager.Singleton.OnMaterialChange += HandleMaterialChange;
    }

    private void HandleMaterialChange(Material material)
    {
        foreach (MeshRenderer renderer in meshRenderers)
        {
            materials = new List<Material>();

            string rendererParentTag = renderer.transform.parent.tag;
            if (material.name.Contains("Wood"))
            {
                if (renderer.tag == "Base" | rendererParentTag == "Base" | rendererParentTag == "Drawers" | rendererParentTag == "Doors") // ... wood!
                {
                    renderer.material = material;
                }
            }
            else if (material.name.Contains("Granite"))
            {
                if (renderer.tag == "CounterTop" | rendererParentTag == "CounterTop") // ... marble!
                {
                    renderer.material = material;
                }
            }
            else
            {
                if (rendererParentTag == "Pulls" | rendererParentTag == "Knobs") // ... metal!
                {
                    renderer.material = material;
                }
            }
            materials.Add(renderer.material);
        }
    }

    private void OnDisable()
    {
        UIManager.Singleton.OnMaterialChange -= HandleMaterialChange;
    }
}
