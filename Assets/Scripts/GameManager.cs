using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Player player;
    public int gold { get; private set; }
    public int level { get; private set; }
    public int exp { get; private set; }

    public Action OnGoldChanged;
    public Action OnLevelChanged;
    public Action OnExpChanged;

    public int maxEXP = 50; //임시 변수. //todo: 레벨업 공식 만들기.

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        player = FindObjectOfType<Player>();
        gold = 0;
        level = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            EarnGold(10);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(!SpendGold(10))
            {
                Debug.Log("골드 부족");
            }
        }
    }

    public void EarnGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke();
    }
    public bool SpendGold(int amount)
    {
        if (amount > gold)
        {
            return false;
        }
        else
        {
            gold -= amount;
            OnGoldChanged?.Invoke();
            return true;
        }
    }
    
    public void EarnEXP(int amount)
    {
        exp += amount; 
        if(exp >= maxEXP)
        {
            LevelUp();
            OnLevelChanged?.Invoke();
        }
        OnExpChanged?.Invoke();
    }
    private void LevelUp()
    {
        exp -= maxEXP;
        level++;
        if(exp >= maxEXP)
        {
            LevelUp();
        }
    }
}
