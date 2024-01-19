using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public LayerMask MonsterLayer;
    public GameObject BombVfx;
    
    private void OnCollisionEnter(Collision other)
    {
        if (((1 << other.gameObject.layer) & MonsterLayer) != 0)
        {
            //Todo: Damage 
            Debug.Log("Attack");
        }

        Instantiate(BombVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
