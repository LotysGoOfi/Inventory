using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Item[] AllItems;

    private PlayerController player;

    private Enemy enemy;

    [SerializeField]
    private GameObject panelGameOver;
    

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        player = GetComponent<PlayerController>();       
    }

    private void Start()
    {
        var saveData = Load();
        if (saveData != null)
        {
            RestartGame(saveData.enemyHealthsPoint, saveData.playerHealthsPoint, saveData.slots);
        }
        else { 
            RestartGame();
        }
    } 


    public void Reward()
    {
        var empitySlot = inventory.GetEmptySlot();
        int itemID = Random.Range(0,AllItems.Length);
        empitySlot.AddItem(AllItems[itemID],1);
    }

    public void GeameAction()
    {
        player.Attack(enemy);
    }

    public void RestartGame(int healthsEnemy, int healthsPlayer, SlotData[] slotsData)
    {
        enemy.ResetEmemy(healthsEnemy);
        player.StartGame(inventory,healthsPlayer);

        inventory.DeliteAllItems();
        panelGameOver.SetActive(false);

        AddStartItem(slotsData);
    }

    public void Save()=> SaveSessionManager.SaveData(inventory, AllItems, enemy.CuurentHealth, player.GetHealthPoint());

    void OnApplicationQuit()
    {
        Save();
    }

    public void RestartGame()
    {
        RestartGame(100, 100, new SlotData[3]
        {
            new(0,1,50),
            new(1,0,100),
            new(2,6,6)
        });
    }
    public SaveData Load()=> SaveSessionManager.LoadData();
    
    private void AddStartItem(SlotData[] slotsData)
    {
        foreach (SlotData slot in slotsData)
        {
            inventory.AddItem(slot.slotID, AllItems[slot.itemID], slot.numberItem);
        }

    }
}
