using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    [Header("UI")]
    private List<InventorySlot> slots = new ();
    [SerializeField] private InventorySlot slotPrefab;

    [Header("Data")]    //임시 test용 시간있으면 InventoryManager로 데이터핸들링 빼기
    [SerializeField] private List<ItemDataBase> items = new ();
    public Action OnInventoryChanged;

    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;
        if (player == null) Debug.Log("ConsumableSlotContainer에서 player 접근 불가");
    }
    private void Start()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);   //todo Destroy 안할 방법. (오브젝트 풀?)
        }
        slots.Clear();

        foreach (var item in items)
        {
            InventorySlot newSlot = Instantiate(slotPrefab, transform);
            newSlot.SetItem(item);

            slots.Add(newSlot);
        }

        OnInventoryChanged?.Invoke();
    }
    public bool AddItem(ItemDataBase item)  //todo. 중복확인은 InventoryManager로 넘기기
    {
        if(items.Contains(item))
        {
            Debug.Log($"{item.itemName} 이미 소지중");
            return false;
        }

        items.Add(item);
        RefreshInventory();

        Debug.Log($"{item.itemName} 추가함");
        return true;
    }

    public bool RemoveItem(ItemDataBase item)   //todo. Use 되었을때 호출되어야함.
    {
        bool wasRemoved = items.Remove(item);

        if (wasRemoved)
        {
            Debug.Log($"{item.itemName} 제거됨");
            return true;
        }
        else
        {
            Debug.Log($"{item.itemName} 찾을 수 없음");
            return false;
        }
    }

    public int GetEnhancementAmountByType(EnhanceType enhanceType)
    {
        int enhancementAmount = 0;
        foreach (var slot in slots)
        {
            ItemDataBase _item = slot.GetItem();
            if(_item != null && _item is EquipmentItemData equipmentItem)
            {
                foreach (var enhancement in equipmentItem.enhancements)
                {
                    if(enhancement.type == enhanceType)
                    {
                        enhancementAmount += enhancement.amount;
                        enhancementAmount += enhancement.increasement * slot.itemLevel;
                    }
                }
            }
        }

        return enhancementAmount;
    }
}
