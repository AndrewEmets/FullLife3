using UnityEngine;

public class SpeedBonus : Bonus
{
    public float SpeedBooster;

    public override void ApplyBonus(Character character)
    {
        base.ApplyBonus(character);


        var charController = character.gameObject.GetComponent<CharacterController2D>();
        if (charController != null)
        {
            Debug.Log("speed");
            charController.Speed += SpeedBooster;
        }
    }

    protected override void DeclineBonus(Character character)
    {
        base.DeclineBonus(character);

        var charController = character.gameObject.GetComponent<CharacterController2D>();
        if (charController != null)
        {
            charController.Speed -= SpeedBooster;
        }
    }
}