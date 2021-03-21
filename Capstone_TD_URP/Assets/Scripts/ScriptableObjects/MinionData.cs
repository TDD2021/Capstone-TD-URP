using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion Data", menuName = "TD Game/Minion")]
public class MinionData : ScriptableObject
{
    [SerializeField] private string minionName;     // Name as it appears in game
    [SerializeField] private string description;    // In game description
    [SerializeField] private GameObject prefab;     // Minion prefab
    [SerializeField] private int icon;              // UI icon for menus
    [SerializeField] private int speed;             // Minion speed

    public string MinionName => minionName;
    public string Description => description;
    public GameObject Prefab => prefab;
    public int Icon => icon;
    public int Speed => speed;
}
