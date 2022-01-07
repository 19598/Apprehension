using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeechAI : EnemyClass
{
    public Animator animator;
    public Health playerHealth;
    [SerializeField] public string enemyName;
    public float health;
    public float speed = 1;
    public float viewDistance;
    public float damage = 10;
    Transform target;
    Vector3 orgPos;
    [SerializeField]NavMeshAgent agent;
    public LayerMask groundMask;
    private float timer;
    public float wanderRadius;
    public float wanderTimer;
    private Vector3 curPos;
    private Vector3 lastPos;
    private Vector3 position;
    private float type = 0;
    public AnimationClip clip;
    public float attackCooldownTime;
    public GameObject player;
    public LayerMask maskOfLayer;
    // Start is called before the first frame update
    void Start()
    {
        attackCooldownTime = clip.length;
        orgPos = transform.position;
        wanderTimer = 15f;
        wanderRadius = viewDistance / 2;
        timer = wanderTimer;
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        playerHealth = FindObjectOfType<Health>();
    }
    bool followingPlayer;
    bool cantSeePlayer;
    // Update is called once per frame
    void Update()
    {
        cantSeePlayer = Physics.Linecast(transform.position, target.position, maskOfLayer);

        if (damage > 0)
        {
            damage = -damage;
        }
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= viewDistance && !cantSeePlayer)
        {
            agent.speed = 2;
            followingPlayer = true;
            animator.SetBool("canSeePlayer", true);
            agent.SetDestination(target.position);
            FaceTarget();

            if (distance <= agent.stoppingDistance && playerHealth.getHealth() > 0)
            {
                animator.SetBool("canAttack", true);
                Attack();
            }
            else
            {
                animator.SetBool("canAttack", false);
                attackCooldownTime = clip.length;
            }
        }
        else
        {
            agent.speed = speed;
            followingPlayer = false;
            animator.SetBool("canSeePlayer", false);
            animator.SetBool("canAttack", false);
            Wander();
        } 
    }
    public void Attack()
    {
        if (attackCooldownTime > 0)
        {
            attackCooldownTime -= Time.deltaTime;
        }
        else
        {
            playerHealth.changeHealthByAmount(damage);
            attackCooldownTime = clip.length;
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(orgPos, wanderRadius, 3);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (cantSeePlayer)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }

        if (followingPlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
        }
    }
    public override float getHealth()
    {
        return health;
    }
    public override void setHealth(float newHealth)
    {
        health = newHealth;
    }
    public override void changeHealthByAmount(float amount)
    {
        health += amount;
    }
    public override float returnType()
    {
        return type;
    }
}
