using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
    public GameObject[] Monsters;
    public Transform MonsterSpawnPosition;

    private float spawnTimer = 0f;
    public float SpawnTime = 2f;
    public float SpawnDelay = 1f;


    private async void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= SpawnTime)
        {
            spawnTimer = 0f;
            if (GameManager.Instance.State == GameState.Stage1)
            {
                Instantiate(Monsters[0], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (GameManager.Instance.State == GameState.Stage2)
            {
                Instantiate(Monsters[0], MonsterSpawnPosition.position, Quaternion.identity);
                await WaitOneSecondAsync();
                Instantiate(Monsters[1], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (GameManager.Instance.State == GameState.Stage3)
            {
                Instantiate(Monsters[1], MonsterSpawnPosition.position, Quaternion.identity);
                await WaitOneSecondAsync();
                Instantiate(Monsters[2], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (GameManager.Instance.State == GameState.Stage4)
            {
                Instantiate(Monsters[2], MonsterSpawnPosition.position, Quaternion.identity);
                await WaitOneSecondAsync();
                Instantiate(Monsters[3], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (GameManager.Instance.State == GameState.Stage5)
            {
                Instantiate(Monsters[3], MonsterSpawnPosition.position, Quaternion.identity);
                await WaitOneSecondAsync();
                Instantiate(Monsters[4], MonsterSpawnPosition.position, Quaternion.identity);
            }
        }
    }
    
    private async UniTask WaitOneSecondAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(SpawnDelay));
    }
}
