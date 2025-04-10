// using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    private MeshRenderer[] _meshRenderers;
    // public List<Material> materials = new List<Material>();

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
    }
    private void Start() // Slight Race Condition in OnEnable
    {
        UIManager.Singleton.OnMaterialChange += HandleMaterialChange;
    }

    private void HandleMaterialChange(Material material)
    {
        foreach (MeshRenderer renderer in _meshRenderers)
        {
            // materials = new List<Material>();

            string rendererParentTag = renderer.transform.parent.tag;
            if (material.name.Contains("Wood"))
            {
                if (renderer.tag == "Base" || rendererParentTag == "Base" || rendererParentTag == "Drawers" || rendererParentTag == "Doors") // Wood
                {
                    renderer.material = material;
                }
            }
            else if (material.name.Contains("Granite"))
            {
                if (renderer.tag == "CounterTop" || rendererParentTag == "CounterTop") // Marble
                {
                    renderer.material = material;
                }
            }
            else
            {
                if (rendererParentTag == "Pulls" || rendererParentTag == "Knobs") // Metal
                {
                    renderer.material = material;
                }
            }
            // materials.Add(renderer.material);
        }
    }

    private void OnDisable()
    {
        UIManager.Singleton.OnMaterialChange -= HandleMaterialChange;
    }
}
