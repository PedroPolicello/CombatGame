using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/New Equipment")]
public class Equipment : ScriptableObject
{
    public string Name;
    public int Power;
    public int Defense;
}
