using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public int health;
    public int speed;
    public float jump;

    public bool hasCooldown = false;

    public float coolDuration = 0f;
}
