using UnityEngine;

public class Enemy1 : EnemyInheritance
{
    
    protected override void Start()
    {
        // Override stats for this enemy type
        maxHealth = 10;
        moveSpeed = 1.5f;
        contactDamage = 5;

        base.Start();
    }
    
}