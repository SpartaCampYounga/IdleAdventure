using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableSlot : MonoBehaviour
{
    [Header("Data")]
    private ItemDataBase item;

    [Header("UI")]
    public Button slotButton;
    public Image itemIcon;
    public Image timeIndicator;

    public event Action OnItemUsed;

    public void SetItem(ItemDataBase? item)
    {
        this.item = item;

        if(item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.enabled = true;
            slotButton.interactable = true;
        }
        else
        {
            EmptySlot();
        }
    }
    public void EmptySlot()
    {
        item = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
        slotButton.interactable = false;
    }

    public void OnButtonClicked()
    {
        if (item != null)
        {
            OnItemUsed?.Invoke();
        }
    }

    public ItemDataBase GetItem()
    {
        return item;
    }
}
