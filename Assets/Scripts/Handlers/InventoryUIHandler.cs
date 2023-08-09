using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{
    public GameObject slotPrefab;

    private void Start()
    {
        InventorySystem.instance.onInventoryChangeEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (InventoryItem item in InventorySystem.instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);

        InventorySlots slots = obj.GetComponent<InventorySlots>();
        slots.Setup(item);
    }
}
