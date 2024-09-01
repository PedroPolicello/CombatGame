using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    [Header("---- Equipment Info ----")]
    [SerializeField] private EquipmentDisplay equipmentPrefab;
    [SerializeField] private Equipment[] equipments;

    public Equipment playerSelectedEquipment;
    private Equipment enemySelectedEquipment;

    private void Awake()
    {
        Instance = this;
        DisplayEquipments();
    }

    void DisplayEquipments()
    {
        foreach (Equipment equipment in equipments)
        {
            EquipmentDisplay equipmentDisplay = Instantiate(equipmentPrefab, this.transform);
            equipmentDisplay.PopulateDisplay(equipment);
            equipmentDisplay.OnEquipmentSelected += PlayerSelectedEquipment;
        }
    }

    void PlayerSelectedEquipment(EquipmentDisplay selectedDisplay)
    {
        GameManager.Instance.audioManager.PlaySFX(GameManager.Instance.audioManager.click); //Ajuste
        playerSelectedEquipment = selectedDisplay.GetEquipment();
        GameManager.Instance.isEquipmentSelected = true;
    }

    public Equipment GetEnemyEquipment()
    {
        int index = Random.Range(0, equipments.Length);
        enemySelectedEquipment = equipments[index];
        return enemySelectedEquipment;
    }
}
