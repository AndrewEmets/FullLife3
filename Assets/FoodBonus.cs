using System.Collections;
using UnityEngine;

public class FoodBonus : Bonus
{
    public float hungerPoints;

    public override void ApplyBonus(Character character)
    {
        base.ApplyBonus(character);
        Debug.Log("cheese");
        character.AddHunger(-hungerPoints);
    }
}