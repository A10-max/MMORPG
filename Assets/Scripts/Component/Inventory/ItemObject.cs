using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ItemObject : NetworkBehaviour
{
    public InventoryItemData referenceItem;

    public void OnHandlePickupItem()
    {
        InventorySystem.instance.Add(referenceItem);
        NetworkManager.Destroy(gameObject);
    }
}
