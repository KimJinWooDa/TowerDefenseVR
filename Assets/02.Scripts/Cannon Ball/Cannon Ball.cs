using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public LayerMask MonsterLayer;
    public GameObject BombVfx;

    private float damage;

    public void Init(float damage)
    {
        this.damage = damage;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (((1 << other.gameObject.layer) & MonsterLayer) != 0)
        {
            if(other.gameObject.TryGetComponent(out Monster monster))
            {
                monster.GetHit(damage);
            }
            
        }

        Instantiate(BombVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
