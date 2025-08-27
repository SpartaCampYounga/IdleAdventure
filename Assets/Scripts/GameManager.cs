using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Player player;
    private int gold = 0;
    public int Gold
    {
        get { return gold; }
        set     //일단은 퍼블릭 셋터 접근 허용.
        { 
            gold = value;
            OnGoldChanged?.Invoke();
        }
    }
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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Gold++;
        }
    }
}
