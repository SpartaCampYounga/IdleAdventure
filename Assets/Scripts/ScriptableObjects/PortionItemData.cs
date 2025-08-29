using UnityEngine;

[CreateAssetMenu(fileName = "Potion_", menuName = "Items/Potion Item")]
public class PortionItemData : ItemDataBase
{
    [Header("Potion Properties")]   //나중에 타입 추가 할지도
    public int healAmount;

    // ItemBase의 Use 함수를 오버라이드하여 포션의 효과를 구현합니다.
    public override void Use(PlayerCondition player)
    {
        if(player == null) return;

        player.GetHealed(healAmount);
        Debug.Log($"{itemName}을(를) 사용하여 HP {healAmount} 회복!");
    }
}
