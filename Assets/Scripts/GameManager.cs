using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Public
    public AccessibilityManager AccessibilityManager;
    // Public Variables
    public Transform spawnPoint;
    public Transform enemy;
    // Private
    private GameObject gameWall;
    private GameObject[] gameTiles;
    private GameObject startPoint;
    private GameObject endPoint;  
    private GameObject enemyRoute;
    private GameObject turret;
    private GameManager GM;
    //private GameObject turretBullet;
    private float currentHealth = 500f;
    private float maxHealth = 100f;
    private float spawnInterval = 5f;
    private float spawnCooldown = 2f;
    private int currentWaveCount = 0;
    private int maxWaveCount = 5;
    private bool isGameOver = false;

    void Start()
    {
        if (GM == null)
        {
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        // Find All GameObjects by their tags
        gameWall = GameObject.FindGameObjectWithTag("GameWall");
        gameTiles = GameObject.FindGameObjectsWithTag("GameTile");
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
        enemyRoute = GameObject.FindGameObjectWithTag("EnemyRoute");
        turret = GameObject.FindGameObjectWithTag("Turret");
        //turretBullet = GameObject.FindGameObjectWithTag("TurretBullet");

        // Perform a debug check to ensure all objects are correctly initialized
        GameObjectsDebug();

        AccessibilityManager.ApplyColorProfile(AccessibilityManager.currentProfile);
    }

    void Update()
    {
        spawnCooldown -= Time.deltaTime;
        if (spawnCooldown <= 0 && !isGameOver)
        {
            spawnCooldown = spawnInterval;
            currentWaveCount++;
            if (currentWaveCount <= maxWaveCount)
            {
                StartCoroutine(SpawnEnemyWave());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene settingsMenuScene = SceneManager.GetSceneByName("Settings_Menu");

            if (settingsMenuScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync("Settings_Menu");
                Time.timeScale = 1; // Resume time when closing the settings menu.
            }
            else
            {
                StartCoroutine(LoadSettingsMenuCoroutine());
            }
        }
    }

    IEnumerator LoadSettingsMenuCoroutine()
    {
        Time.timeScale = 0; // Pause the game when opening the settings menu.

        // Load the scene additively and wait for it to complete.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Settings_Menu", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the asynchronous scene fully loads
        }

        // Set the loaded scene as active.
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Settings_Menu"));
    }

    void GameObjectsDebug()
    {
        // Check to see if each game object was correctly initialized and log errors if not
        if (!gameWall) Debug.LogError("GameWall was not initialized correctly.");
        if (gameTiles.Length == 0) Debug.LogError("GameTiles were not initialized correctly."); else Debug.Log(gameTiles.Length + " GameTiles were created.");
        if (!startPoint) Debug.LogError("StartPoint was not initialized correctly.");
        if (!endPoint) Debug.LogError("EndPoint was not initialized correctly.");
        if (!enemyRoute) Debug.LogError("EnemyRoute was not initialized correctly.");
        if (!turret) Debug.LogError("Turret was not initialized correctly.");
        //if (!turretBullet) Debug.LogError("TurretBullet was not initialized correctly.");
    }
    // Additional GameManager logic here (game state management, spawning enemies, etc.)

    public void UpdateGameTiles()
    {
        // Fetch the current color profile from AccessibilityManager
        var currentProfile = AccessibilityManager.GetCurrentProfile();

        // Calculate the health percentage
        float healthPercentage = currentHealth / maxHealth;

        // Interpolate between the high and low health colors based on the current health percentage
        Color tileColor = Color.Lerp(currentProfile.lowHealthClr, currentProfile.highHealthClr, healthPercentage);

        // Apply the interpolated color to each game tile
        foreach (var gameTile in gameTiles)
        {
            var renderer = gameTile.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = tileColor;
            }
        }
    }

    public IEnumerator SpawnEnemyWave()
    {
        for (int i = 0; i < currentWaveCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }
    public void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

    }
    public void EnemyReachedEnd(GameObject enemy)
    {
        Debug.Log(currentHealth);
        // Access EnemyController to get current health
        EnemyController EnemyController = enemy.GetComponent<EnemyController>();
        if (EnemyController != null)
        {
            float enemyHealth = EnemyController.GetCurrentHealth();
            // Use enemyHealth to subtract from player health
            currentHealth -= enemyHealth;  // Assuming you want to subtract the enemy's current health from the player's health
            UpdateGameTiles();
            if (currentHealth <= 0)
            {
                isGameOver = true;
                foreach (GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(enemyObject);
                }
            }
        }
    }


    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("Settings_Menu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
