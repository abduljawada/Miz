using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameManager gameManager => GameManager.instance;
    [SerializeField] float damping = 5f;
    [SerializeField] float multiplicationFactor = 2f;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = gameManager.cam.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 newPosition = (Vector2) gameManager.player.transform.position + mousePos / subtractionFactor;

        Vector2 mouseDir = mousePos - (Vector2) gameManager.player.transform.position;

        float distanceFactor = mouseDir.magnitude * multiplicationFactor;

        Vector2 newPosition = (Vector2)gameManager.player.transform.position + mouseDir.normalized * distanceFactor;

        transform.position = Vector2.Lerp(transform.position, newPosition, damping * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
