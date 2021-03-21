using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "TD Game/Tower")]
public class TowerData : ScriptableObject
{
    [SerializeField] private string towerName;      // Name as it appears in game
    [SerializeField] private string description;    // In game description
    [SerializeField] private GameObject prefab;     // Tower prefab
    [SerializeField] private int icon;              // UI icon for menus
    [SerializeField] private int damage;            // Damage dealt by tower
    [SerializeField] private int range;             // Firing range of tower

    public string TowerName => towerName;
    public string Description => description;
    public GameObject Prefab => prefab;
    public int Icon => icon;
    public int Damage => damage;
    public int Range => range;


}
