using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Player player;
    public int gold { get; private set; }
    //public int Gold
    //{
    //    get { return gold; }
    //    private set
    //    { 
    //        gold = value;
    //        OnGoldChanged?.Invoke();
    //    }
    //}
    public Action OnGoldChanged;

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
}
