using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public float flashTime;
    public Material flashMaterial;
    public Camera cam;
    public CameraShake cameraShake; 
    public float shakeIntensity; 
    public float shakeTime;
    public GameObject deathUI;
    public GameObject deathParticle;
    public GameObject exclamationPrefab;
    public GameObject questionPrefab;
    public Vector3 spawnPoint;
    public GameObject playerSword;
    public GameObject playerBow;
    public Health[] enemies;
    //public float particleSpawnDelay = .2f;
    private void Awake()
    {
        instance = this;
    }

    public void EndGame()
    {
        Invoke("StopTime", 3);
        player.GetComponent<PlayerHealth>().Win();
        deathUI.SetActive(true);
    }

    public void GiveWeapon(bool isBow)
    {
        if (isBow)
        {
            GameObject weapon = Instantiate(playerBow, player.transform);
            weapon.transform.localPosition = playerBow.transform.position;
        }
        else
        {
            GameObject weapon = Instantiate(playerSword, player.transform);
            weapon.transform.localPosition = playerSword.transform.position;
        }
    }

    void StopTime()
    {
        Time.timeScale = 0;
    }

}
