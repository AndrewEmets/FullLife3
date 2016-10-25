using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Zombie : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    public float speed;

    public bool isStunned = false;

    public float wallCheckDistance;

    private Animator animator;
    private AudioReactorScaler scaler;
    private Vector2 movingDirection;
    public float Satiety;

    void Awake()
    {
        animator = GetComponent<Animator>();
        scaler = GetComponentInChildren<AudioReactorScaler>();
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        movingDirection = (Vector2)transform.right;
    }

    void Update()
    {
        if (!isStunned)
        {
            rigidbody2D.MovePosition(rigidbody2D.position + movingDirection*speed*Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        var r = Random.Range(0, 2);

        if (r == 0)
            movingDirection = new Vector2(-movingDirection.y, movingDirection.x);
        else
            movingDirection = new Vector2(movingDirection.y, -movingDirection.x);
    }

    private bool isHittedAWallOrZombie()
    {
        var rayres = Physics2D.Raycast(transform.position, movingDirection, wallCheckDistance, LayerMask.GetMask("walls"));
        var rayres2 = Physics2D.Raycast(transform.position, movingDirection, wallCheckDistance, LayerMask.GetMask("zombies"));
        Debug.DrawLine(transform.position, transform.position + (Vector3)movingDirection * wallCheckDistance, Color.black, 0.2f);

        if (rayres.collider != null)
            return true;

        if (rayres2.collider != null)
        {
            if (rayres2.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public void GetHit()
    {
        if (!isStunned)
        {
            isStunned = true;
            StartCoroutine(StartUnstanning(2f));
            animator.SetBool("hit", true);
            scaler.enabled = false;
        }
    }

    private IEnumerator StartUnstanning(float time)
    {
        yield return new WaitForSeconds(time);

        animator.SetBool("hit", false);
        isStunned = false;
        scaler.enabled = true;
    }
}