using AutoSet.Utils;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("[Core]")]
    [SerializeField, AutoSet] protected NavMeshAgent agent; 
    [SerializeField, AutoSet] private Animator animator;
    protected Professor professor;
    
    [Space(10)]
    [Header("[Layer]")]
    public LayerMask ProfessorLayer;
    
    [Space(10)]
    [Header("[Value]")]
    public MonsterInfo MonsterInfo;
    public float AttackTime = 2f;
    private float attackTimer = 0f;
    
    [Space(10)]
    [Header("[FX]")]
    [SerializeField, AutoSet] private AudioSource audioSource;
    public AudioClip SpawnAudio;
    public AudioClip DeathSound;
    
    protected float damage;
    protected float hp;
    protected float speed;
    protected int gold;
    private bool isDead = false;
    
    private readonly int Attack1 = Animator.StringToHash("Attack");
    private readonly int Die1 = Animator.StringToHash("Die");
    private readonly int Hit = Animator.StringToHash("GetHit");
    private const string Professor = "Professor";

    private Transform professorTransform;
   
    
    private void Start()
    {
        damage = MonsterInfo.Damage;
        hp = MonsterInfo.Hp;
        speed = MonsterInfo.Speed;
        gold = MonsterInfo.Gold;
        
        professor = null;
        professorTransform = GameObject.FindGameObjectWithTag(Professor).transform;

        audioSource.clip = SpawnAudio;
        audioSource.Play();
        
        agent.speed = speed;
        agent.SetDestination(professorTransform.position);
    }

    private void Update()
    {
        if (isDead) return;
        
        attackTimer += Time.deltaTime;
        
        if (professor == null)
        {
            return;
        }

        if (attackTimer >= AttackTime)
        {
            attackTimer = 0f;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if(isDead) return;
        
        agent.isStopped = true;
        animator.SetTrigger(Attack1);
        professor.GetHit(damage);
    }

    private void Die()
    {
        isDead = true;
        
        audioSource.clip = DeathSound;
        audioSource.Play();
        agent.isStopped = true;
        animator.SetTrigger(Die1);
        GameManager.Instance.GetGold(gold);
    }

    public void GetHit(float damage)
    {
        if(isDead) return;
     
        agent.isStopped = true;
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger(Hit);
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void Recover()
    {
        if(isDead) return;
        
        agent.isStopped = false;
        agent.SetDestination(professorTransform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & ProfessorLayer) != 0)
        {
            professor = other.GetComponent<Professor>();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & ProfessorLayer) != 0)
        {
            if (professor)
            {
                professor = null;
            }
        }
    }
}
