using Cysharp.Threading.Tasks;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public LayerMask MonsterLayer;
    public float FiringAngle = 45.0f;
    public float Gravity = 9.8f;
    public float AttackDelay = 1f;
    public float AttackTimer = 0f;
    public Transform ProjectilePrefab;
    public Transform Turret;
    public Transform TurretMuzzle;
    
    private Transform targetMonster;
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & MonsterLayer) != 0)
        {
            targetMonster = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & MonsterLayer) != 0)
        {
            targetMonster = null;
        }
    }
    
    private async void Update()
    {
        AttackTimer += Time.deltaTime;
        if (targetMonster)
        {
            Vector3 targetDirection = targetMonster.position - Turret.position;
            Turret.rotation = Quaternion.LookRotation(targetDirection);
        }
        else
        {
            Turret.rotation = Quaternion.LookRotation(Vector3.zero);
            return;
        }
        
        if (AttackTimer >= AttackDelay)
        {
            AttackTimer = 0f;
            await AttackMonster();
        }
    }

    private async UniTask AttackMonster()
    {
        Transform projectile = Instantiate(ProjectilePrefab, TurretMuzzle.position, Quaternion.identity);
        projectile.forward = Turret.forward;

        float targetDistance = Vector3.Distance(targetMonster.position, Turret.position);
        float projectileVelocity = targetDistance / (Mathf.Sin(2 * FiringAngle * Mathf.Deg2Rad) / Gravity);

        float Vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(FiringAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(FiringAngle * Mathf.Deg2Rad);

        float flightDuration = targetDistance / Vx;

        float elapsed = 0;
        while (elapsed < flightDuration + AttackDelay)
        {
            if (projectile != null)
            {
                projectile.Translate(new Vector3(0, Vy * Time.deltaTime, Vx * 0.5f * Time.deltaTime), Space.Self);
                Vy -= Gravity * Time.deltaTime;
                elapsed += Time.deltaTime;
            }
            else
            {
                break;
            }

            await UniTask.Yield();
        }
    }
}
