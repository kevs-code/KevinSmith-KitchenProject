using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Button[] swatchButtons;
    public static UIManager Singleton { get; private set; }
    public Dictionary<string, Material> materialDictionary = new Dictionary<string, Material>();
    public List<Material> materials = new List<Material>();
    public event Action<Material> OnMaterialChange;
    //Event Action 
    private void Awake()
    {
        Singleton = this;
        swatchButtons = GetComponentsInChildren<Button>(true);
    }

    private void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        foreach (Button button in swatchButtons)
        {
            string swatchName = button.image.sprite.name;

            foreach (Material material in materials)
            {
                if (material.name.Contains(swatchName) && !materialDictionary.ContainsKey(swatchName))
                {
                    materialDictionary[swatchName] = material;

                    button.onClick.AddListener(() =>
                    {
                        OnMaterialChange?.Invoke(materialDictionary[swatchName]);
                        Debug.Log(materialDictionary[swatchName].name);
                    });
                }
            }
        }
    }
}
