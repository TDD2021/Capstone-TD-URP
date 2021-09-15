using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "TD Game/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private string playerName;      // Player Name as it appears in game
    [SerializeField] private int icon;              // UI icon for player
    [SerializeField] private int wins;             //  number of games that the player has won
    [SerializeField] private int loses;             //  number of games that the player has lost
    [SerializeField] private int currency;             // Amount of money/currency that the player owns

    public string PlayerName => playerName;
    public int Icon => icon;
    public int Wins => wins;
    public int Loses => loses;
    public int Currency => currency;

    public void setCurrency(int value) {
        currency = value;
    }


}