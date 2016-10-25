using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    private new Renderer renderer;
    private new Collider2D collider;

    public float time = 0;
    private float endTime;

    private bool isAplied = false;
    private Character appliedTo;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isAplied && Time.time >= endTime)
        {
            DeclineBonus(appliedTo);
        }
    }

    public virtual void ApplyBonus(Character character)
    {
        renderer.enabled = false;
        collider.enabled = false;
        transform.SetParent(character.transform);
        endTime = time + Time.time;
        
        isAplied = true;
        appliedTo = character;
        character.BonusGet();
    }

    protected virtual void DeclineBonus(Character character)
    {
        Debug.Log("Bonus declined");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var character = other.gameObject.GetComponentInChildren<Character>();
        if (character == null)
            return;
        
        ApplyBonus(character);
    }
}