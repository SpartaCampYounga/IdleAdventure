using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Information")]
    public string enemyName;

    [Header("Status")]
    public int maxHealth;
    public int attackDamage;
    public float attackRange;
    public float attactInterval;

    [Header("Reward")]
    public int rewardGold;
    public int rewardEXP;

    [Header("Resource")]
    public GameObject enemyPrefab;
}