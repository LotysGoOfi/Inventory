using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private HelthsBar helthsBar;

    [SerializeField]
    private bool isPistol = false;

    private Inventory currentInventory;

    [SerializeField]
    private GameObject gameOverPanel;

    public void StartGame(Inventory inventory,int healthsPlayer)
    {
        currentInventory = inventory; 
        player = new Player(healthsPlayer, 0, 0);
        helthsBar.SetHealth(healthsPlayer);
        inventory.GetBodySlot().updateArnorBody.AddListener(AddArmorBody);
        inventory.GetHeadSlot().updateArnorHead.AddListener(AddArmorHead);
    }

    public void SetPistol()
    {
        isPistol = true;
    }

    public int GetHealthPoint()
    {
        return player.HelthPoint;
    }
    public void SetGun()
    {
        isPistol = false;
    }

    public void Attack(Enemy target)
    {
        if (currentInventory != null) 
        {
            if (isPistol)
            {
                var ammoSlotPistol = currentInventory.GetSlotAmmoPistol();
                if (ammoSlotPistol != null)
                {                  
                    ammoSlotPistol.RemoveItemNumber(1);
                    target.Damage(5, this);
                    return;
                }
            }
            if (!isPistol)
            {
                var ammoSlotGun = currentInventory.GetSlotAmmoGun();
                if (ammoSlotGun != null)
                {
                    if (ammoSlotGun.GetCurrentNumberItem() < 3) {
                      
                        return; 
                    }
                    ammoSlotGun.RemoveItemNumber(3);
                    target.Damage(9, this);
                    return;
                }              
            }
        }
    }
    public void Heal(int numberHeal)
    {
       helthsBar.SetHealth(player.Heal(numberHeal));
    }

    public void Damage(int damage, TypeArmor typeDamage)
    {
        var currentHealth = player.Damage(damage, typeDamage);
        if(currentHealth == 0)
        {
            gameOverPanel.SetActive(true);
        }
        helthsBar.SetHealth(currentHealth);
    }

    public void AddArmorHead(int arrmor) => player.ArrmorHead = arrmor;
   
    public void AddArmorBody(int arrmor) => player.ArrmorBody = arrmor;
    
}
