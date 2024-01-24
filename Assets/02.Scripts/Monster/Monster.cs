using System;
using AutoSet.Utils;
using Cysharp.Threading.Tasks;
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
    public float AnimationDelay = 1f;
    public float AttackTime = 2f;
    private float attackTimer = 0f;
    
    protected float damage;
    protected float hp;
    protected float speed;
    protected int gold;
    
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
        
        agent.speed = speed;
        agent.SetDestination(professorTransform.position);
    }

    private void Update()
    {
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
        agent.isStopped = true;
        animator.SetTrigger(Attack1);
        professor.GetHit(damage);
    }

    private async void Die()
    {
        agent.isStopped = true;
        animator.SetTrigger(Die1);
        GameManager.Instance.GetGold(gold);
        await WaitOneSecondAsync();
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public async void GetHit(float damage)
    {
        agent.isStopped = true;
        await WaitOneSecondAsync();
        Recover();
        
        animator.SetTrigger(Hit);
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Recover()
    {
        if (gameObject != null)
        {
            agent.isStopped = false;
            agent.SetDestination(professorTransform.position);
        }
    }
    
    private async UniTask WaitOneSecondAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(AnimationDelay), cancellationToken: destroyCancellationToken);
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
