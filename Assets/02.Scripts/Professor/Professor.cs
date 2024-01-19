using AutoSet.Utils;
using UnityEngine;

public class Professor : MonoBehaviour
{
    [SerializeField, AutoSet] private Animator animator;
    public float Hp = 100f;
    
    private readonly int Hit = Animator.StringToHash("GetHit");
    private readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        Hp = 100f;
    }

    public void GetHit(float damage)
    {
        Hp -= damage;
        if (Hp > 0f)
        {
            animator.SetTrigger(Hit);
        }
        else
        {
            animator.SetTrigger(Die);
            GameManager.Instance.GameOver();
        }
       
    }
}
