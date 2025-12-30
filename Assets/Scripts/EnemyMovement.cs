using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementTypes
    {
        StraightDown,
        SineDown,
        StopAndGo,
        StopExplode
    }
    
    [SerializeField] private MovementTypes movementType = MovementTypes.StraightDown;
    [SerializeField] private bool useSpeedFromEnemyBase = true;
    [SerializeField] private float localSpeed = 2f;

    //Sine Down movement
    
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 2f;
    [SerializeField] private float phaseOffset = 0f;

    //Stop and Go Movement
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float stopDuration = 0.5f;

    public enum ExplosionPatternID
    {
        Ring,
        Cone,
        Sine
    }

    //Stop and Explode movement
    [SerializeField] private float travelTime = 1f;
    [SerializeField] private float stopTimeBeforeExplode = 1.5f;
    [SerializeField] private ExplosionPatternID explosionPattern = ExplosionPatternID.Ring;

    // Runtime
    private Rigidbody2D rb;
    private EnemyInheritance enemyBase;
    private float startOnX;
    private float timer;
    private bool hasExploded;

    // Stop&Go state
    private bool isStopped;
    private float stateTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyBase = GetComponent<EnemyInheritance>();
    }

    private void Start()
    {
        startOnX = rb.position.x;
        timer = 0f;
        hasExploded = false;
        isStopped = false;
        stateTimer = 0f;
    }

    private void FixedUpdate()
    {
        // Fallback in case the enemy needs an rb
        if (rb == null)
        {
            return;
        }

        float dt = Time.fixedDeltaTime;
        timer += dt;
        float speed = GetSpeed();

        switch (movementType)
        {
            case MovementTypes.StraightDown:
                MoveStraightDown(speed, dt);
                break;

            case MovementTypes.SineDown:
                MoveSineDown(speed, dt);
                break;

            case MovementTypes.StopAndGo:
                MoveStopAndGo(speed, dt);
                break;

            case MovementTypes.StopExplode:
                MoveStopExplode(speed, dt);
                break;
        }
    }

    private float GetSpeed()
    {
        // If you don't have MoveSpeed exposed in EnemyInheritance yet,
        // either add a public property there or set useSpeedFromEnemyBase = false.
        if (useSpeedFromEnemyBase && enemyBase != null)
        {
            return enemyBase.moveSpeed;
        }

        return localSpeed;
    }

    private void MoveStraightDown(float speed, float dt) // this is the simple enemy movement that goes down
    {
        Vector2 pos = rb.position; // changes the position to 
        pos += Vector2.down * speed * dt; // go down using delta time
        rb.MovePosition(pos); // this moves the position of the enemy
    }

    private void MoveSineDown(float speed, float dt)
    {
        Vector2 pos = rb.position;
        pos.y -= speed * dt; 
        float x = startOnX + Mathf.Sin((timer + phaseOffset) * frequency) * amplitude;
        pos.x = x;
        rb.MovePosition(pos);
    }

    private void MoveStopAndGo(float speed, float dt)
    {
        stateTimer += dt; // would this maybe be better with a couroutine

        if (!isStopped)
        {
            Vector2 pos = rb.position;
            pos += Vector2.down * speed * dt;
            rb.MovePosition(pos);

            if (stateTimer >= moveDuration)
            {
                isStopped = true;
                stateTimer = 0f;
            }
        }
        else
        {
            // Stopped phase (do nothing)
            if (stateTimer >= stopDuration)
            {
                isStopped = false;
                stateTimer = 0f;
            }
        }
    }

    private void MoveStopExplode(float speed, float dt)
    {
        if (hasExploded)
        {
            return;
        }

        if (timer < travelTime) // first enemy has to travel down
        {
            Vector2 pos = rb.position;
            pos += Vector2.down * speed * dt;
            rb.MovePosition(pos);
            return;
        }
        
        float timeSinceStop = timer - travelTime; // stop and then explode once
        if (timeSinceStop >= stopTimeBeforeExplode)
        {
            hasExploded = true;
            //onExplode?.Invoke(); //here's where the bullets will be added I think and will appear from the middle of the dead enemy and explore in a ring pattern but the bullets essentually straight down
            // don't destroy after the bullet explosion, keep moving down and then wait for a bit then explode again if not killed by player
        }
    }

    public ExplosionPatternID GetExplosionPattern() => explosionPattern; //end somehow?
    
}

