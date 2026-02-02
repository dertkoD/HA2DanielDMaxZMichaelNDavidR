using System;
using UnityEngine;

[Serializable] 
public struct WeaponPickupData
{
    public int pickerAgentId;
    public int weaponId;
    public GameObject weaponPrefab;
}
