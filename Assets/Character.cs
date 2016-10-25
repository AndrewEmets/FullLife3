using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    #region Hunger
    [SerializeField]private float hunger;
    public float Hunger
    {
        get { return hunger; }
        set
        {
            if (hunger < MaxHunger)
            {
                hunger = value;

                if (hunger > MaxHunger)
                    OnHungerFilled.Invoke();
            }

            hunger = Mathf.Clamp(hunger, 0, MaxHunger);

            if (HungerFillerImage != null)
                HungerFillerImage.fillAmount = hunger / MaxHunger;
        }
    }
    public float HungerSpeed = 1;
    public Image HungerFillerImage;
    public float MaxHunger = 10;
    private UnityEvent OnHungerFilled;
    #endregion

    public GameObject attackPrefab;

    public AudioClip[] RandomComments;
    public AudioClip[] ZombieEatComments;
    public AudioClip[] BonusCommetns;

    private AudioSource audioSource;
    private Rigidbody2D rigidbody2D;
    public AudioSource MusicSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Hunger = 0;
        OnHungerFilled = new UnityEvent();
        OnHungerFilled.AddListener(Die);
    }

    void Update()
    {
        Hunger += HungerSpeed*Time.deltaTime;
    }

    public void AddHunger(float value)
    {
        Debug.Log(Hunger);
        Hunger += value;
        Debug.Log(Hunger);
    }

    public void Attack(Vector2 direction)
    {
        GameObject.Instantiate(attackPrefab, transform.position, Quaternion.Euler(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var z = collision.collider.gameObject.GetComponent<Zombie>();

        if (z == null)
            return;

        if (z.isStunned)
            EatZombie(z);
        else
            GetHit(z);
    }

    public void BonusGet()
    {
        if (soundPlayer == null)
            soundPlayer = StartCoroutine(StartPlayingSound(BonusCommetns[Random.Range(0, BonusCommetns.Length)]));
    }

    private Coroutine soundPlayer = null;
    private void EatZombie(Zombie zombie)
    {
        AddHunger(-zombie.Satiety);

        if (soundPlayer == null)
            soundPlayer = StartCoroutine(StartPlayingSound(ZombieEatComments[Random.Range(0, ZombieEatComments.Length)]));

        Destroy(zombie.gameObject);
    }

    IEnumerator StartPlayingSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        var mv = MusicSource.volume;
        MusicSource.volume *= 0.2f;

        yield return new WaitForSeconds(clip.length + 0.2f);
        MusicSource.volume = mv;
        soundPlayer = null;
    }

    private void GetHit(Zombie zombie)
    {
        var pushVector = (rigidbody2D.position - (Vector2)zombie.transform.position).normalized * 300;
        Debug.Log(pushVector);
        rigidbody2D.AddForce(pushVector, ForceMode2D.Impulse);
    }

    void Die()
    {
        Debug.Log("You have died!");
    }

    void HUDUpdate()
    {

    }
}