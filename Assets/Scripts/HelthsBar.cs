using UnityEngine;

public class HelthsBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform barHealth;

    
    public void SetHealth(int health)
    {
        barHealth.sizeDelta = new Vector2 (health*2, 50);
    }
}
