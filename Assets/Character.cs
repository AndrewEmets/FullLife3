using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    #region Hunger
    [SerializeField]private float hunger;
    public float Hunger
    {
        get { return hunger; }
        set
        {
            //if (hunger < MaxHunger)
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

    void Awake()
    {
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

    void Die()
    {
        Debug.Log("You have died!");
    }

    void HUDUpdate()
    {

    }
}