using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    public static FighterController player;
    public static EnemySpawner enemySpawner;
    public static  GameManager gameManager;
    public static CameraController camera;
    public static SoundManager soundManager;
    public static CodeTextController codeText;
    public static PlayServicesController playServices;
    void Awake()
    {
        if (player == null) {

            player = FindObjectOfType<FighterController>();

        }
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();

        }
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();

        }
        if (camera == null)
        {
            camera = FindObjectOfType<CameraController>();

        }
        if (soundManager == null)
        {
           soundManager = FindObjectOfType<SoundManager>();

        }
        if (codeText == null)
        {
            codeText = FindObjectOfType<CodeTextController>();

        }
        if (playServices == null)
        {
            playServices = FindObjectOfType<PlayServicesController>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playServices == null)
        {
            playServices = FindObjectOfType<PlayServicesController>();

        }
        if (camera == null)
        {
            camera = FindObjectOfType<CameraController>();

        }
        if (player == null)
        {
            player = FindObjectOfType<FighterController>();

        }
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();

        }
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();

        }
        if (soundManager == null)
        {
            soundManager = FindObjectOfType<SoundManager>();

        }

        if (codeText == null)
        {
            codeText = FindObjectOfType<CodeTextController>();

        }
    }
}
