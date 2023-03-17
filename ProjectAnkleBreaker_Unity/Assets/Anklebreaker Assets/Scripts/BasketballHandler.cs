using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballHandler : MonoBehaviour
{
    public bool hasBall;
    private Transform playerHand;
    private GameObject player;
    private GameObject attachPoint;
    private MeshCollider Court;

    [SerializeField] private Transform originalSpawn;
    [SerializeField] private int respawnWaitTime;

    [SerializeField]
    Transform _target;
    Rigidbody _rb;

    [SerializeField]
    float initialAngle, scoreDistance, maxXFailure, maxYFailure, maxZFailure,  threeChance, twoChance;
    Vector3 initialPos;

    bool shotEntered;
    bool animAlreadyPlayed; //This is the spamproof boolean flag so that methods dont get replayed again when the player does certain actions.

    int scoreToAdd;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        hasBall = false;
        _rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        GameObject Court_gameObject = GameObject.Find("Court");
        Court = Court_gameObject.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            player = other.gameObject;
            attachPoint = player.transform.Find("AttachPoint").gameObject;
            FindRightHand(); //Goes through the WHOOOOOOOOOOOOOOOOLE hirarchy in the Player prefab to find the hand_r Transform. Is this really the only way? maybe i can do a foreach instead.
            //playerHand = player.transform.Find("hand_r");
            this.gameObject.GetComponent<CamerCloseAndFar>().Switch_Cameras(false);
            //---Tellopenhasball
            //Connects to player's Animator to play Dribble Animation
            hasBall = true;
            Animator playerAnims = player.GetComponent<Animator>();
            playerAnims.SetBool("hasBall", true);
            //Debug to tell who has the ball
            Debug.Log($"{player} has the ball");

            //Attaches Ball to Player
            _rb.isKinematic = true;
            shotEntered = false;
            if (!animAlreadyPlayed) //To make sure the animator doesnt get played again when shooting the ball.
            {
                anim.enabled = true;
                animAlreadyPlayed = true;
                transform.localPosition = Vector3.zero;
                this.gameObject.transform.SetParent(attachPoint.transform);
            }
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

    private void FindRightHand()
    {
        Transform temp = player.transform.Find("M_TestGuy").transform.Find("Game_engine").transform.Find("Root").transform.Find("pelvis").transform.Find("spine_01").transform.Find("spine_02").transform.Find("spine_03").transform.Find("clavicle_r").transform.Find("upperarm_r").transform.Find("lowerarm_r").transform.Find("hand_r");
        playerHand = temp;
    }

    public void ChangeParentToPlayerHand() //This method disables the Animator component for the ball and changes its parent to the player's hand instead of the Attach Point.
    {
        anim.enabled = false;
        this.gameObject.transform.SetParent(playerHand.transform);
        transform.localPosition = Vector3.zero;
    }

    public void ShootBall()
    {
        if (!hasBall) return;
        //Shoots the ball
        this.gameObject.GetComponent<CamerCloseAndFar>().Switch_Cameras(true);
        //---Tellopenhasnotball
        hasBall = false;
        ChangeParentToPlayerHand();
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
        StartCoroutine(changeSpamproofBool()); //resets the animAlreadyPlayed boolean to false
    }

    IEnumerator changeSpamproofBool()
    {
        yield return new WaitForSeconds(2f);
        animAlreadyPlayed = false;
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
