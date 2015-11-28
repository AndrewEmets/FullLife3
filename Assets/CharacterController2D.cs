using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rigidBody;

    private Vector2 movingDirection;

    void Awake()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
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

        Debug.DrawRay(transform.position, dir, Color.black, 0.2f);
    }
}