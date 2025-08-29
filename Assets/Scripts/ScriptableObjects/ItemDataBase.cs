using UnityEngine;

public class ItemDataBase : ScriptableObject
{
    [Header("Information")]
    public int id;
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    public int price;

    public virtual void Use(PlayerCondition player) { }
    public virtual void Equip(PlayerCondition player) { }

}
