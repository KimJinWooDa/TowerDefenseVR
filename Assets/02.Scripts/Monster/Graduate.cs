using UnityEngine;

public class Graduate : Monster
{
    [SerializeField] private ParticleSystem explosionEffect;
    
    protected override void Attack()
    {
        agent.isStopped = true;
        professor.GetHit(damage);

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
