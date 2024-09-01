using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDisplay : MonoBehaviour
{
    [Header("---- Text Info----")]
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI power;
    [SerializeField] private TextMeshProUGUI defense;
    
    [Header("---- Button ----")]
    [SerializeField] private Button selectButton;
    private Equipment storedEquipment;

    public event Action<EquipmentDisplay> OnEquipmentSelected;

    private void Awake()
    {
        selectButton.onClick.AddListener(InvokeOnEquipmentSelected);
    }
    
    private void InvokeOnEquipmentSelected()
    {
        OnEquipmentSelected?.Invoke(this);
    }

    public void PopulateDisplay(Equipment equipment)
    {
        name.text = equipment.Name;
        power.text = $"Força: {equipment.Power}";
        defense.text = $"Defesa: {equipment.Defense}";
        storedEquipment = equipment;
    }

    public Equipment GetEquipment()
    {
        return storedEquipment;
    }
}
