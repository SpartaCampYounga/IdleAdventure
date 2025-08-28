using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ConsumableSlotContainer : MonoBehaviour
{
    [Header("UI")]
    private ConsumableSlot[] slots = new ConsumableSlot[3];
    [SerializeField] private GameObject slotPrefab;

    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;
        if (player == null) Debug.Log("ConsumableSlotContainer에서 player 접근 불가");
    }

    private void Start()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, transform);
            slots[i] = newSlot.GetComponent<ConsumableSlot>();

            slots[i].slotButton.onClick.AddListener(slots[i].OnButtonClicked);

            int index = i;
            slots[index].OnItemUsed += () => UseItem(index);

            slots[index].SetItem(null);
        }
    }

    public bool AddItem(ItemDataBase item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem() == null)
            {
                slots[i].SetItem(item);
                Debug.Log($"{i + 1}번 슬롯에 {item.itemName}을 추가함");
                return true;
            }
        }

        Debug.Log("인벤토리 가득참");
        return false;
    }
    public void UseItem(int itemIndex) { 
        if(itemIndex < 0 || itemIndex >= slots.Length || slots[itemIndex].GetItem() == null)
        {
            Debug.Log("슬롯 인덱스 잘못됨");
        }

        ItemDataBase itemToUse = slots[itemIndex].GetItem();

        itemToUse.Use(player.Condition);

        slots[itemIndex].EmptySlot();
    }
}
