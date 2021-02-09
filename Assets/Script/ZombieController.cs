using UnityEngine;
using UnityEngine.AI;

public class ZombieController : BaseStat
{

    public AudioSource claw;
    private NavMeshAgent agent;
    private Transform player;
    private DropItemController dropItemController;
    public LayerMask whatIsGround, whatIsPlayer;

    private Animator animator;
    Hitbox hitbox;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float stayedTime;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // point when killed
    public int points = 10;

    bool isDead = false;

    ZombieHealthBar healthBar;


    private void Start()
    {   
        claw = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dropItemController = GetComponent<DropItemController>();
        player = FindObjectOfType<PlayerController>().transform;
        healthBar = GetComponentInChildren<ZombieHealthBar>();

        healthBar.SetSliderValue(currentHealth, maxHealth);

        agent.speed = currentSpeed;
    }

    private void Update()
    {
        if (isDead) return;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // enemy beheavior
        if (playerInSightRange && !playerInAttackRange && !alreadyAttacked) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    public virtual void TakeDamage(int damage, bool critical)
    {
        if (isDead) return;

        Debug.Log(name + " take " + damage + " damage");
        ChangeHealth(-damage);

        healthBar.SetSliderValue(currentHealth, maxHealth);
        healthBar.ShowDamageText(damage, critical);

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        // trigger death animation
        animator.SetTrigger("dead");

        // drop item
        dropItemController.TriggerDrop(transform.position + (Vector3.up * 3));

        // increase score when kill enemy
        GameManager.Instance.ChangeScore(points);
        GameManager.Instance.RemoveEnemyInScene(gameObject);

        // disable enemy
        Debug.Log(name + " is dead...");
        isDead = true;
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        healthBar.Disable();
        this.enabled = false;

    }

    private void ChasePlayer()
    {
        animator.SetBool("moving", true);
        agent.SetDestination(player.position);
        claw.Play();
    }

    public virtual void AttackPlayer()
    {

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        LookAtPlayer();

        if (!alreadyAttacked)
        {
            ///Attack code here
            animator.SetBool("moving", false);
            animator.SetTrigger("attack");
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void LookAtPlayer()
    {
        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 20);
    }
}

