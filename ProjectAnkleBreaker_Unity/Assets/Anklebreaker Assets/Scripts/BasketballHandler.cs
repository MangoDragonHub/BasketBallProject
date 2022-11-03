using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballHandler : MonoBehaviour
{
    public bool hasBall;
    public Transform playerHand;
    public GameObject player;

    [SerializeField] private Transform originalSpawn;
    [SerializeField] private int respawnWaitTime;

    [SerializeField]
    Transform _target;
    Rigidbody _rb;

    [SerializeField]
    float initialAngle, scoreDistance, maxXFailure, maxYFailure, maxZFailure,  threeChance, twoChance;
    Vector3 initialPos;

    bool shotEntered;

    int scoreToAdd;

    // Start is called before the first frame update
    void Start()
    {
        hasBall = false;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if Player has the ball or not.
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            //Connects to player's Animator to play Dribble Animation
            hasBall = true;
            Animator playerAnims = player.GetComponent<Animator>();
            playerAnims.SetBool("hasBall", true);
            //Debug to tell who has the ball
            Debug.Log($"{player} has the ball");

            //Attaches Ball to Player
            this.gameObject.transform.SetParent(playerHand.transform);
            _rb.isKinematic = true;
            transform.localPosition = Vector3.zero;
            shotEntered = false;
        }
        if (other.gameObject.CompareTag("EntryCheck"))
        {
            shotEntered = true;
        }
        if (other.gameObject.CompareTag("ExitCheck"))
        {
            if (shotEntered)
            {
                GameManager.Instance.score += scoreToAdd;
                Debug.Log($"Player scored, Score: {GameManager.Instance.score}");
                StartCoroutine(ResetBall());
            }
            shotEntered = false;
        }

    }

    public void ShootBall()
    {
        if (!hasBall) return;
        //Shoots the ball
        hasBall = false;
        _rb.isKinematic = false;
        transform.SetParent(null);
        Launch(CalculateTarget());
        Debug.Log($"{player} has shot the ball");
    }

    Vector3 CalculateTarget()
    {
        Vector3 target = _target.position;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);

        if (distance > scoreDistance && Random.Range(0f, 1f) > threeChance || 
            distance < scoreDistance && Random.Range(0f, 1f) > twoChance)
        {
            target += Vector3.forward * Random.Range(-maxZFailure, maxZFailure);
            target += Vector3.right * Random.Range(-maxXFailure, maxXFailure);
            target += Vector3.up * Random.Range(-maxYFailure, maxYFailure);
        }
        
        return target;
    }

    void Launch(Vector3 targetPos)
    {
        initialPos = transform.position;

        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
        Vector3 planarPosition = new Vector3(initialPos.x, 0, initialPos.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = initialPos.y - targetPos.y;

        if (distance > scoreDistance)
        {
            scoreToAdd = 3;
        }
        else
        {
            scoreToAdd = 2;
        }

        // Original equation https://physics.stackexchange.com/questions/27992/solving-for-initial-velocity-required-to-launch-a-projectile-to-a-given-destinat?rq=1
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition);
        if (targetPos.x < initialPos.x) angleBetweenObjects *= -1;
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        _rb.useGravity = true;
        _rb.velocity = finalVelocity;
    }

    public void SetParent(GameObject newParent) 
    {
        //Sets the Game Object as a child to the object it want to be parented
        playerHand.transform.parent = newParent.transform;
        //Console Check
        Debug.Log("Player's Parent: " + playerHand.transform.parent.name);
    }

    public void DetachFromParent() 
    {
        //Detaches ball from Player's hand
        if (hasBall == false) 
        {
            transform.parent = null;
        }
       
    }

    IEnumerator ResetBall() 
    {
        if (shotEntered) 
        {
            yield return new WaitForSeconds(respawnWaitTime);
            this.transform.position = originalSpawn.position;
        }
    
    }

}
