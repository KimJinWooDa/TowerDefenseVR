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
    
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= SpawnTime)
        {
            spawnTimer = 0f;
            if (GameManager.Instance.State == GameState.Stage1)
            {
                Spawn(0, false);
            }
            else if (GameManager.Instance.State == GameState.Stage2)
            {
                Spawn(0);
            }
            else if (GameManager.Instance.State == GameState.Stage3)
            {
                Spawn(1);
            }
            else if (GameManager.Instance.State == GameState.Stage4)
            {
                Spawn(2);
            }
            else if (GameManager.Instance.State == GameState.Stage5)
            {
                Spawn(3);
            }
        }
    }

    private async void Spawn(int index, bool spawnMultipleTypes = true)
    {
        Instantiate(Monsters[index], MonsterSpawnPosition.position, Quaternion.identity);
        if (spawnMultipleTypes)
        {
            await WaitOneSecondAsync();
            Instantiate(Monsters[index+1], MonsterSpawnPosition.position, Quaternion.identity);
        }
    }
    
    private async UniTask WaitOneSecondAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(SpawnDelay));
    }
}
