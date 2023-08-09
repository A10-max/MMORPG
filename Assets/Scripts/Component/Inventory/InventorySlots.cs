using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Image icon;
    public Text label;
    public GameObject stackObj;
    public Text stackLabel;

    public void Setup(InventoryItem item)
    {
        icon.sprite = item.data.icon;
        label.text = item.data.disPlayName;
        if(item.stackSize <= 1 )
        {
            stackObj.SetActive(false);
            return;
        }

        stackLabel.text = item.stackSize.ToString();
    }
}
