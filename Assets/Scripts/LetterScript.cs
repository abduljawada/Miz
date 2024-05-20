using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour
{
    [SerializeField] GameObject letter;
    [SerializeField] float boxRadius = .5f;
    [SerializeField] LayerMask playerMask;
    GameManager gameManager => GameManager.instance;
    DialogueManager dialogueManager => DialogueManager.instance;
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(transform.position, new Vector2(boxRadius, boxRadius), 0, new Vector2(0, 0), boxRadius, playerMask);

        if (raycastHit2D && !dialogueManager.inDialogue)
        {
            letter.SetActive(true);
        }
        else
        {
            letter.SetActive(false);
        }
    }
}
