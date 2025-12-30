// EnemyInheritance.cs
using UnityEngine;

public abstract class EnemyInheritance : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1.5f;
    [SerializeField] public int maxHealth = 1;
    [SerializeField] public int contactDamage = 1;
    protected bool isDead = false;

    protected Transform player;
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            
        {
            player = playerObject.transform;
        }
            
    }

    protected virtual void Update()
    {
        if (isDead)
            
        {
            return;
        }

        if (player == null)
        {
            Debug.Log("theres no player to follow");
            return;
        }
        
        // Vector3 dir = (player.position - transform.position).normalized;
        // transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        
        maxHealth -= damage;
        
        if (maxHealth <= 0)
        {
            EnemyDie();
        }
    }
    
    public virtual void EnemyDie()
    {
        if (isDead) return;
        isDead = true;
        Destroy(gameObject);
    }
    
    // run take damage script and as a part of the multiplyier can take effect there
    // 
}