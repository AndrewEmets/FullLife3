using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rigidBody;
    private Animator animator;

    private Vector2 movingDirection;
    private Character character;

    void Awake()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        character = GetComponent<Character>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        InputUpdate();
    }

    private void InputUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var dir = new Vector2(horizontal, vertical);
        rigidBody.position += dir * Speed * Time.deltaTime;

        if (dir .magnitude > 0)
            movingDirection = dir.normalized;

        Debug.DrawRay(transform.position, movingDirection, Color.black, 0.2f);

        if (movingDirection.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (movingDirection.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Attack(movingDirection);
        }

        animator.SetFloat("absSpeed", dir.magnitude);
    }
}