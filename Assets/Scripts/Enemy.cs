using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private HelthsBar helthsBar;

    [SerializeField]
    private int cuurentHealth;

    [SerializeField]
    private TypeArmor typeDamage;

    [SerializeField]
    private GameController gameController;

    public int CuurentHealth { get => cuurentHealth; set => cuurentHealth = value; }

    public void Damage(int number,PlayerController player)
    {
        if (cuurentHealth - number <= 0)
        {
            ResetEmemy(100);
        }
        else
        {
            cuurentHealth -= number;
            helthsBar.SetHealth(cuurentHealth);
        }

        if (typeDamage == TypeArmor.Head)
        {
            player.Damage(damage,TypeArmor.Body);
        }
        else
        {
            player.Damage(damage, TypeArmor.Head);
        }
        
    }
    public void ResetEmemy(int healths)
    {
        cuurentHealth = 100;
        helthsBar.SetHealth(cuurentHealth);
        gameController.Reward();
    }

    
}
