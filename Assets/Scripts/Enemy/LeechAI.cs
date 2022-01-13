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
    private float speed = 8;
    public float viewDistance;
    private float damage = -15;
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
    private float attackCooldownTime = 0.2f;
    public GameObject player;
    public LayerMask maskOfLayer;
    // Start is called before the first frame update
    void Start()
    { //assigns values that arent assigned immediately.
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
    { //draws a linecast from this position to the target (player) and accepts layer masks so it can detect walls.
        cantSeePlayer = Physics.Linecast(transform.position, target.position, maskOfLayer);

        //in case a value that isnt negative is put into the script, it turns it negative for it to work.
        if (damage > 0)
        {
            damage = -damage;
        }
        if (agent.remainingDistance > agent.stoppingDistance)
        {//animator booleans setting animations to run
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        //grabs the distance between this and the target position and performs tests once found to check if this object can follow the player
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= viewDistance && !cantSeePlayer)
        {
            agent.speed = 2;
            followingPlayer = true;
            animator.SetBool("canSeePlayer", true);
            agent.SetDestination(target.position);
            FaceTarget();
            //if object is close enough it may attack as long as the health is greater than 0
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
    //attack method that grabs the player health and subtracts health from it.
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
    //uses a navmeshsphere and picks a random point inside of it for the leech to patrol too
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    //face the target at all times to make it more realistic
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    //how often can the object wander around the map
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
    //draws gizmos to help devs see and test what needs to be done
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
    //grabs the health of the leech
    public override float getHealth()
    {
        return health;
    }

    //sets the health of the leech
    public override void setHealth(float newHealth)
    {
        health = newHealth;
    }
    //changes the health by an amount
    public override void changeHealthByAmount(float amount)
    {
        health += amount;
    }
    //returns the object type
    public override float returnType()
    {
        return type;
    }
}
