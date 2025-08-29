using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Data")]
    private ItemDataBase item;
    public int itemLevel { get; private set; } = 1;
    [SerializeField] private int currentUpgradePrice;

    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
    public TextMeshProUGUI price;
    public Button upgradeButton;

    private PlayerCondition playerCondition;

    private void Awake()
    {
        playerCondition = GameManager.Instance.player.Condition;
        if (playerCondition == null) Debug.Log("InventorySlot에서 player condition 못찾음.");

        upgradeButton.onClick.AddListener(UpgradeItem);
    }

    private void RefreshInformation()
    {
        icon.sprite = item.itemIcon;
        name.text = item.itemName;
        description.text = item.itemDescription;    //todo: 현재 enhancement로 출력 (SO에서 OnValidate 사용)
        SetUpgradePrice();
        price.text = currentUpgradePrice.ToString();

        GameManager.Instance.inventoryContainer.OnInventoryChanged?.Invoke();
    }
    public void SetItem(ItemDataBase newItem)
    {
        item = newItem;
        if(item != null)
        {
            RefreshInformation();

            icon.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
            price.gameObject.SetActive(true);
            if(item is PortionItemData)
            {
                upgradeButton.gameObject.SetActive(false);
            }
            else if(item is EquipmentItemData) 
            {
                upgradeButton.gameObject.SetActive(true);
            }
        }
        else
        {
            ClearSlot();
        }
    }
    public void ClearSlot()
    {
        item = null;
        icon.gameObject.SetActive(false);
        name.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        price.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(false);
    }

    private void UpgradeItem()
    {
        if (item == null) return;

        if(GameManager.Instance.gold >= currentUpgradePrice)
        {
            GameManager.Instance.SpendGold(currentUpgradePrice);
            itemLevel++;
            Debug.Log($"{item.itemName}을 업그레이드함.");
            RefreshInformation();
        }
        else
        {
            Debug.Log($"돈이 부족해서 업그레이드 못함.");
        }
    }

    private void SetUpgradePrice()   //임시 계산식.
    {
        currentUpgradePrice = (int)((item.price * itemLevel) * 1.5f);
    }

    public ItemDataBase GetItem()
    {
        return item;
    }
}
