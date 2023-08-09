using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InventorySystem : NetworkBehaviour
{
    public static InventorySystem instance;

    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;
    public List<InventoryItem> inventory { get; private set; }

    public Action onInventoryChangeEvent;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if(itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData referenceData)
    {
        if(itemDictionary.TryGetValue(referenceData,out InventoryItem value))
        {
            value.AddToStackServerRpc();
        }
        else
        {
            InventoryItem item = new InventoryItem(referenceData);
            inventory.Add(item);
            itemDictionary.Add(referenceData, item);
        }
    }


    public void Remove(InventoryItemData referenceData)
    {
        if(itemDictionary.TryGetValue(referenceData,out InventoryItem value))
        {
            value.RemoveFromStackServerRpc();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(referenceData);
            }
        }

    }
}

