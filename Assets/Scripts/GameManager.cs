using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [Header("Stages")]
    public StageData[] allStageData;
    public StageManager stageManager { get; private set; }

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
        gold = 0;   //나중에는 저장된 값 불러오기로 확장 
        level = 1;
        stageManager = FindObjectOfType<StageManager>();
    }

    private void Start()
    {
        StartRandomStage();
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

    public void StartRandomStage()
    {
        if(allStageData.Length == 0)
        {
            Debug.Log("스테이지 하나도 없음...");
            return;
        }

        int randomIndex = Random.Range(0, allStageData.Length);
        StageData newStageData = allStageData[randomIndex];

        stageManager.InitStage(newStageData);
    }

    public void EndStage()
    {
        //todo: UI로 표현하기
        Debug.Log("스테이지 완료");

        Invoke("StartRandomStage", 3f);
    }

    public void UpdatePlayerEnemyList(List<GameObject> enemies)
    {
        Debug.Log($"GameManager: 플레이어에게 {enemies.Count}마리 적 리스트 전달.");
        player.Movement.SetEnemyTargetList(enemies);
    }

    /////////Sett 메서드들

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
