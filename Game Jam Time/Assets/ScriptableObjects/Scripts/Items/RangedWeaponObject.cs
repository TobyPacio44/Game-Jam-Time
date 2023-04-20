using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New RangedWeapon Object", menuName = "Inventory/Items/RangedWeapon")]
public class RangedWeaponObject : ItemObject
{
    public GameObject bullet;
    public float cooldown;

    public void Awake()
    {
        type = ItemType.RangedWeapon;
    }
}
