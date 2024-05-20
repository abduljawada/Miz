using UnityEngine;
using Pathfinding;

public class TargetManager : MonoBehaviour
{
    [SerializeField] float distance = 5f;
    AIDestinationSetter destinationSetter => GetComponent<AIDestinationSetter>();
    Collider2D myCollider => GetComponent<Collider2D>();
    RaycastHit2D[] results = new RaycastHit2D[1];
    GameManager gameManager => GameManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationSetter.target == null)
        {
            myCollider.Raycast(gameManager.player.transform.position - transform.position, results, distance);

            if (results[0].collider != null)
            {
                if (results[0].collider.gameObject == gameManager.player)
                {
                    destinationSetter.target = gameManager.player.transform;
                }
            }
        }
    }
}
