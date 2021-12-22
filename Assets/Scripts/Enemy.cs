using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private List<Transform> locations;
    private const string PLAYER_TAG = "Player";
    
    private int locationIndex = 0;
    private NavMeshAgent agent;
    private Transform player;
    
    private int lives = 3;

    public int Lives
    {
        get { return lives; }
        private set
        {
            lives = value;
            
            if (lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextPatrolLocation();
        player = GameObject.FindGameObjectWithTag(PLAYER_TAG).transform;
    }

    private void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            Debug.LogError("Count of locations is 0!");
            return;
        }
        
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    private void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayerTag(other.gameObject))
        {
            agent.destination = player.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayerTag(other.gameObject))
        {
            Debug.Log("Player out of range");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Lives -= 1;
            Debug.Log("Critical hit!");
        }
    }

    private bool IsPlayerTag(GameObject collider)
    {
        return collider.CompareTag(PLAYER_TAG);
    }
}
