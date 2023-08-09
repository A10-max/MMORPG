using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[Serializable]
public class InventoryItem
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(InventoryItemData source)
    {
        data = source;
        AddToStackServerRpc();
    }

    public void AddToStackServerRpc()
    {
        stackSize++;
    }

    public void RemoveFromStackServerRpc()
    {
        stackSize--;
    }
}
