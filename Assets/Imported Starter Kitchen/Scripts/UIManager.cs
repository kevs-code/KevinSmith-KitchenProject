using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Button[] _swatchButtons;
    private Dictionary<string, Material> _materialDictionary = new Dictionary<string, Material>();
    public static UIManager Singleton { get; private set; }
    public List<Material> materials = new List<Material>();
    public event Action<Material> OnMaterialChange;
    public AudioSource kitchenAmbience;
 
    private void Awake()
    {
        Singleton = this;
        _swatchButtons = GetComponentsInChildren<Button>(true);
    }

    private void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        foreach (Button button in _swatchButtons)
        {
            AddSwatchListeners(button);
        }
    }

    private void AddSwatchListeners(Button button)
    {
        string swatchName = button.image.sprite.name;

        if (swatchName.Contains("Check"))
        {
            AddHardwareListener(button);
        }

        foreach (Material material in materials)
        {
            if (material.name.Contains(swatchName) && !_materialDictionary.ContainsKey(swatchName))
            {
                _materialDictionary[swatchName] = material;

                button.onClick.AddListener(() =>
                {
                    OnMaterialChange?.Invoke(_materialDictionary[swatchName]);
                });
            }
        }
    }

    private void AddHardwareListener(Button button)
    {
        button.onClick.AddListener(() =>
        {
            Image tick = button.transform.Find("Tick").GetComponent<Image>();
            tick.enabled = !tick.enabled;

            if (kitchenAmbience == null) { return; }
            kitchenAmbience.enabled = !kitchenAmbience.enabled;
        });
    }
}
