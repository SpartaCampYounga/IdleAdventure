using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    [Header("Base Status")]
    public int baseAttackDamage = 10;
    public float baseAttackInterval = 1f;

    [Header("Total Status")]
    public int totalAttackDamage;
    public float totalAttackInterval;

    [Header("Condition")]
    public int currentHealth;
    public int maxHealth = 100;

    public Action OnHPChanged;

    //private Dictionary<EquipmentType, EquipmentItemData> equippedItems = new Dictionary<EquipmentType, EquipmentItemData>();

    private void Start()
    {
        currentHealth = maxHealth;
        OnHPChanged?.Invoke();

        if(GameManager.Instance.inventoryContainer != null)
        {
            GameManager.Instance.inventoryContainer.OnInventoryChanged += RecalculateTotalStatus;
        }
        RecalculateTotalStatus();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        Debug.Log($"Player (이)가 {damage}를 입어 체력이 {currentHealth} / {maxHealth}가 되었다.");

        OnHPChanged?.Invoke();
    }

    public void GetHealed(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        Debug.Log($"Player (이)가 {amount}를 회복하여 체력이 {currentHealth} / {maxHealth}가 되었다.");

        OnHPChanged?.Invoke();
    }

    //public void Equip(EquipmentItemData item)
    //{
    //    if (equippedItems.ContainsKey(item.equipmentType))
    //    {
    //        equippedItems.Remove(item.equipmentType);
    //    }

    //    equippedItems.Add(item.equipmentType, item);
    //    RecalculateTotalStatus();
    //}
    //public void UnEquip(EquipmentItemData item)
    //{
    //    if(equippedItems.ContainsKey(item.equipmentType))
    //    {
    //        equippedItems.Remove(item.equipmentType);
    //        RecalculateTotalStatus();
    //    }
    //}
    public void RecalculateTotalStatus()    //todo. enhancementtype이 추가될 상황에 대비해둘 필요 있음. enum을 순회한다거나.
    {
        Debug.Log("PlayerCondition-RecalculateTotalStatus 호출됨");
        InventoryContainer inventory = GameManager.Instance.inventoryContainer;

        if (inventory == null)
        {
            Debug.Log("인벤토리 없음");
            return;
        }


        foreach(EnhanceType type in System.Enum.GetValues(typeof(EnhanceType)))
        {
            int enhancementAmount = inventory.GetEnhancementAmountByType(type);

            switch (type)
            {
                case EnhanceType.AttackDamage:
                    totalAttackDamage = baseAttackDamage;
                    totalAttackDamage += enhancementAmount;
                    break;
                case EnhanceType.AttackSpeed:
                    totalAttackInterval = baseAttackInterval;
                    totalAttackInterval *= (1 - (enhancementAmount / 100f));
                    break;
            }
        }
    }
}
