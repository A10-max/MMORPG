using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data;")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string disPlayName;
    public Sprite icon;
    public GameObject prefab;
}
