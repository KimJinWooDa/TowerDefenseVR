using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
    public GameObject[] Monsters;
    public Transform MonsterSpawnPosition;
    
    [SerializeField] private float currentMonsterTypeTimer = 0f;
    private float spawnTimer = 0f;
    public float SpawnTime = 2f;
    public float SpawnDelay = 1f;
    
    public float[] SpawnTypeTimers;
    
    private async void Update()
    {
        spawnTimer += Time.deltaTime;
        currentMonsterTypeTimer += Time.deltaTime;
        
        if (spawnTimer >= SpawnTime)
        {
            spawnTimer = 0f;
            if (currentMonsterTypeTimer < SpawnTypeTimers[0])
            {
                Instantiate(Monsters[0], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (currentMonsterTypeTimer > SpawnTypeTimers[0] && currentMonsterTypeTimer < SpawnTypeTimers[1])
            {
                Instantiate(Monsters[0], MonsterSpawnPosition.position, Quaternion.identity);
                await WaitOneSecondAsync();
                Instantiate(Monsters[1], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (currentMonsterTypeTimer > SpawnTypeTimers[1] && currentMonsterTypeTimer < SpawnTypeTimers[2])
            {
                Instantiate(Monsters[1], MonsterSpawnPosition.position, Quaternion.identity);
                await WaitOneSecondAsync();
                Instantiate(Monsters[2], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (currentMonsterTypeTimer > SpawnTypeTimers[2] && currentMonsterTypeTimer < SpawnTypeTimers[3])
            {
                Instantiate(Monsters[2], MonsterSpawnPosition.position, Quaternion.identity);
                await WaitOneSecondAsync();
                Instantiate(Monsters[3], MonsterSpawnPosition.position, Quaternion.identity);
            }
            else if (currentMonsterTypeTimer > SpawnTypeTimers[3] && currentMonsterTypeTimer < SpawnTypeTimers[4])
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
