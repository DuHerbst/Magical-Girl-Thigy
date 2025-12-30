using UnityEngine;

public class PlayerBullets : Bullets
{
 
    public override float damage { get; set; } = 3f;
   public override float speed { get; set; } = 10f;
    
   
    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
        }
        
        Destroy(gameObject);
    }
}
