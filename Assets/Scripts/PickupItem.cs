using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private GameBehavior gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameBehavior>().GetComponent<GameBehavior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Item Collected!");
            
            gameManager.Items += 1;
        }
    }
}
