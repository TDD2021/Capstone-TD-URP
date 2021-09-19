using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "TD Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private string playerName;      // Name as it appears in game
    [SerializeField] private string icon;              // UI icon for Player
    [SerializeField] private int resources;         // The amount of resources that the player currently has. 

    public string PlayerName => playerName;
    public string Icon => icon;
    public int Resources => resources;

    public PlayerData(string name, string icn, int res)
    {
        playerName = name;
        icon = icn;
        resources = res;

    }

    public void SetResources(int value)
    {
        resources = value;
    }


}

