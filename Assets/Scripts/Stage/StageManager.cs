using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int spawnedEnemyCount = 0;
    private int defeatedEnemyCount = 0;
    private StageData currentStageData;

    private List<GameObject> currentEnemies;

    public LayerMask enemyLayer;

    public void InitStage(StageData stageData)
    {
        this.currentStageData = stageData;
        this.spawnedEnemyCount = 0;
        this.defeatedEnemyCount = 0;
        currentEnemies = new List<GameObject>();
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        Debug.Log("SpawnEnemies 함수가 실행되었습니다.");
        if (currentStageData == null)
        {
            Debug.Log("스테이지 없음");
            return;
        }
        Debug.Log($"현재 스테이지의 적 목록 크기: {currentStageData.enemies.Count}");

        foreach (var stageEnemy in currentStageData.enemies)
        {
            spawnedEnemyCount += stageEnemy.count;
            for(int i = 0; i < stageEnemy.count; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();

                if(spawnPosition != Vector3.zero)
                {
                    GameObject enemy = Instantiate(stageEnemy.enemyData.enemyPrefab, spawnPosition, Quaternion.identity);

                    currentEnemies.Add(enemy); 

                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    if(enemyController != null )
                    {
                        enemyController.Init(stageEnemy.enemyData);
                    }
                }
            }
        }
        Debug.Log($"StageManager: 적 소환 완료. 총 {currentEnemies.Count}마리.");

        GameManager.Instance.UpdatePlayerEnemyList(currentEnemies);
    }
    private Vector3 GetRandomSpawnPosition()
    {
        int maxAttempts = 10;
        int currentAttempt = 0;
        float enemyRadius = 1.0f;

        while(currentAttempt < maxAttempts)
        {
            float xOffset = Random.Range(-currentStageData.spawnSize.x / 2f, currentStageData.spawnSize.x / 2f);
            float zOffset = Random.Range(-currentStageData.spawnSize.z / 2f, currentStageData.spawnSize.z / 2f);

            Vector3 randomPos = new Vector3(currentStageData.spawnCenter.x + xOffset, transform.position.y, currentStageData.spawnCenter.z + zOffset);

            //Debug.Log($"시도 {currentAttempt + 1} / {maxAttempts}: 위치 {randomPos.ToString()} 확인 중...");


            if (!Physics.CheckSphere(randomPos, enemyRadius, enemyLayer))
            {
                return randomPos;
            }
            currentAttempt++;
        }

        return Vector3.zero;    //적절한 위치 못 찾음ㅜ
    }

    public void OnEnemyDefeated(GameObject enemy)
    {
        defeatedEnemyCount++;
        Debug.Log($"적 처치함! 현재 {defeatedEnemyCount} / {spawnedEnemyCount}");

        if (defeatedEnemyCount >= spawnedEnemyCount)
        {
            GameManager.Instance.EndStage();
        }
    }

    private void OnDrawGizmos()
    {
        if (currentStageData == null) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(currentStageData.spawnCenter, currentStageData.spawnSize);
    }
}
