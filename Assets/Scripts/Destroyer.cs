using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float timeForDestroy = 2;

    private void Start()
    {
        Destroy(gameObject, timeForDestroy);
    }
}
