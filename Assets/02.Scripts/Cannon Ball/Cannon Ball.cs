using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public LayerMask MonsterLayer;
   
    [Space(10)]
    [Header("[FX]")]
    public GameObject BombVfx;
    public AudioClip BombClip;

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

        AudioSource.PlayClipAtPoint(BombClip, transform.position);
        Instantiate(BombVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
