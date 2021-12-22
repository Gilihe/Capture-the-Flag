using UnityEngine;
using UnityEngine.Events;

public class GameBehavior : MonoBehaviour
{
    [SerializeField]
    private int maxItems = 4;
    
    private int itemsCollected = 0;
    private int playerHP = 10;

    public UnityAction OnAllItemCollected;
    public UnityAction<int> OnItemCollected;
    public UnityAction<int> OnItemPlayerDamage;
    public UnityAction OnPlayerDeath;
    
    public int Items
    {
        get { return itemsCollected; }
        set {
            itemsCollected = value;
            
            if(itemsCollected >= maxItems)
            {
                OnAllItemCollected?.Invoke();
            }
            else
            {
                OnItemCollected?.Invoke(itemsCollected);
            }
        }
    }
    
    public int PlayerHP
    {
        get { return playerHP; }
        set {
            playerHP = value;
            OnItemPlayerDamage?.Invoke(playerHP);

            if (playerHP <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    public int MaxItems
    {
        get { return maxItems; }
    }
}
