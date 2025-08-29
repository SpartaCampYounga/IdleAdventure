using JetBrains.Annotations;
using System;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor
}
public enum EnhanceType
{
    AttackDamage,
    AttackSpeed
}

[Serializable]
public class Enhancement
{
    public EnhanceType type;
    public int amount;
    public int increasement;
}

[CreateAssetMenu(fileName = "Equipment_", menuName = "Items/Equipment Item")]
public class EquipmentItemData : ItemDataBase
{
    [Header("Equipment Properties")]
    public EquipmentType equipmentType;
    public Enhancement[] enhancements;


    public override void Equip(PlayerCondition player)
    {
        if (player == null) return;

        Debug.Log($"{itemName}을 장착함");
    }
}
