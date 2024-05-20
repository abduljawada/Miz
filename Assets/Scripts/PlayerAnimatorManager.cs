using UnityEngine;

public class PlayerAnimatorManager : AnimatorManager
{
    GameManager gameManager => GameManager.instance;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = gameManager.cam.ScreenToWorldPoint(Input.mousePosition);
        if (transform.parent.position.x > mousePos.x)
        {
            SetFloat("IsRight", -1);
        }
        else if (transform.parent.position.x < mousePos.x)
        {
            SetFloat("IsRight", 1);
        }
    }
}
