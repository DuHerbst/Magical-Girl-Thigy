using UnityEngine;
using UnityEngine.Serialization;

public class BulletVariant1 : Bullets
{
 
    public override float damage { get; set; }= 3f;
    public override float speed { get; set; } = 10f;
    public Transform player;
    public GameObject variant1prefab;
    
    
    
    void OnEnemyShoot()
    {
        rb.rotation = Mathf.PI * 180 / Mathf.PI;
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
        
        Destroy(gameObject);
    }
}
