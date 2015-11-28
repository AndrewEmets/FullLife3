using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    bool isStunned = false;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void GetHit()
    {
        if (!isStunned)
        {
            isStunned = true;
            StartCoroutine(StartUnstanning(2f));
            animator.SetBool("hit", true);
        }
    }

    IEnumerator StartUnstanning(float time)
    {
        yield return new WaitForSeconds(time);

        animator.SetBool("hit", false);
        isStunned = false;
    }
}