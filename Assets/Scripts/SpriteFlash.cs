using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    GameManager gameManager => GameManager.instance;
    Material defaultMaterial;
    public void Flash()
    {
        defaultMaterial = spriteRenderer.material;
        spriteRenderer.material = gameManager.flashMaterial;

        Invoke("RemoveFlash", gameManager.flashTime);
    }

    void RemoveFlash()
    {
        spriteRenderer.material = defaultMaterial;
    }
}
