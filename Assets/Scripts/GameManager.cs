using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Public
    public AccessibilityManager AccessibilityManager;
    // Public Variables
    public Transform spawnPoint;
    public Transform enemy;
    public TMPro.TextMeshProUGUI playerHPText;
    public TMPro.TextMeshProUGUI playerMoneyText;
    public TMPro.TextMeshProUGUI waveCountText;
    public TMPro.TextMeshProUGUI restartGameText;
    public RawImage restartGameOverlay;
    // Private
    private GameObject gameWall;
    private GameObject[] gameTiles;
    private GameObject startPoint;
    private GameObject endPoint;  
    private GameObject enemyRoute;
    private GameObject[] spawnedEnemies;
    private GameObject turret;
    public static GameManager GM = null;
    //private GameObject turretBullet;
    private float currentHealth = 500f;
    private float maxHealth = 500f;
    private float spawnInterval = 5f;
    private float spawnCooldown = 2f;
    private int currentWaveCount = 0;
    private int maxWaveCount = 5;
    private bool isGameOver = false;
    private int playerMoney = 500;

    void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        restartGameText.text = "";
        restartGameOverlay.enabled = false;
        isGameOver = false;
        currentHealth = maxHealth;
        playerMoney = 500;
        currentWaveCount = 0;
        UpdateGameTiles();
        UpdateUITextElements();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        gameWall = GameObject.FindGameObjectWithTag("GameWall");
        gameTiles = GameObject.FindGameObjectsWithTag("GameTile");
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
        enemyRoute = GameObject.FindGameObjectWithTag("EnemyRoute");
        turret = GameObject.FindGameObjectWithTag("Turret");
        playerHPText = GameObject.FindGameObjectWithTag("PlayerHPText").GetComponent<TMPro.TextMeshProUGUI>();
        playerMoneyText = GameObject.FindGameObjectWithTag("PlayerMoneyText").GetComponent<TMPro.TextMeshProUGUI>();
        waveCountText = GameObject.FindGameObjectWithTag("WaveCountText").GetComponent<TMPro.TextMeshProUGUI>();
        restartGameText = GameObject.FindGameObjectWithTag("RestartGameText").GetComponent<TMPro.TextMeshProUGUI>();
        restartGameOverlay = GameObject.FindGameObjectWithTag("RestartGameOverlay").GetComponent<RawImage>();
        GameObjectsDebug();
        AccessibilityManager = FindObjectOfType<AccessibilityManager>();
        if (AccessibilityManager != null)
        {
            AccessibilityManager.ApplyColorProfile(AccessibilityManager.currentProfile);
        }
        UpdateGameTiles();
        UpdateUITextElements();
    }

    void Update()
    {
        spawnCooldown -= Time.deltaTime;
        if (spawnCooldown <= 0 && !isGameOver)
        {
            spawnCooldown = spawnInterval;
            currentWaveCount++;
            if (currentWaveCount <= maxWaveCount && !isGameOver)
            {
                StartCoroutine(SpawnEnemyWave());
            }
        }
        UserInput();
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

    public void UpdateGameTiles()
    {
        // Fetch the current color profile from AccessibilityManager
        var currentProfile = AccessibilityManager.GetCurrentProfile();

        // Calculate the health percentage
        float healthPercentage = currentHealth / maxHealth;

        Debug.Log("Health Percent: " + healthPercentage);

        // Interpolate between the high and low health colors based on the current health percentage
        Color tileColor = Color.Lerp(currentProfile.lowHealthClr, currentProfile.highHealthClr, healthPercentage);

        // Check if gameTiles is null or empty
        if (gameTiles == null || gameTiles.Length == 0)
        {
            // Re-acquire gameTiles references here, for example:
            gameTiles = GameObject.FindGameObjectsWithTag("GameTile");
        }

        // Apply the interpolated color to each game tile
        foreach (var gameTile in gameTiles)
        {
            if (gameTile != null) // Check if the gameTile is not null
            {
                var renderer = gameTile.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = tileColor;
                }
            }
        }
    }


    public IEnumerator SpawnEnemyWave()
    {
        if (!isGameOver)
        {
            for (int i = 0; i < currentWaveCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f);
                UpdateUITextElements();
            }
        }
    }
    public void SpawnEnemy()
    {
        GameObject enemyInstance = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation).gameObject;
        spawnedEnemies = new GameObject[] { enemyInstance };
    }

    public void EnemyReachedEnd(GameObject enemy)
    {
        // Get Enemy Health and Subtract It From Current Health.
        float enemyHealth = enemy.GetComponent<EnemyController>().enemyHealth;
        currentHealth -= enemyHealth;
        // Update Game Tiles (Reflecting Health Changes)
        UpdateGameTiles();
        // Destroy The Enemy That Reached The End
        Destroy(enemy);

        // Game Over Check
        if (currentHealth <= 0)
        {
            GameOver();
        }

        // Update UI Text Elements
        UpdateUITextElements();

    }

    private void PlaceTurret(int turretCost, GameObject turret)
    {
        if (!isGameOver && playerMoney >= turretCost)
        {
            Ray rayCastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayCastHit;
            if (Physics.Raycast(rayCastRay, out rayCastHit))
            {
                if (rayCastHit.collider.tag == "GameTile")
                {
                    // Place the turret on the game tile not inside the game tile
                    Vector3 turretPosition = rayCastHit.collider.transform.position + new Vector3(0, 1.25f, 0);
                    Instantiate(turret, turretPosition, Quaternion.identity);
                    playerMoney -= turretCost;
                    Debug.Log("Turret Placed. Remaining money: " + playerMoney);
                    // Update UI Text Elements
                    UpdateUITextElements();
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

    private void GameOver()
    {
        // Set Game Over Flag
        isGameOver = true;

        // Display Game Over Text
        restartGameText.text = "Press 'R' to Restart";
        restartGameOverlay.enabled = true;

        playerMoneyText.text = "";
        waveCountText.text = "";
        playerHPText.text = "";

        // Destroy All Enemies.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Destroy All Turrets
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            Destroy(turret);
        }

        // Destroy All Turret Bullets
        GameObject[] turretBullets = GameObject.FindGameObjectsWithTag("TurretBullet");
        foreach (GameObject turretBullet in turretBullets)
        {
            Destroy(turretBullet);
        }
    }

    private void UpdateUITextElements()
    {
        playerHPText.text = "Health: " + (currentHealth/maxHealth) * 100 + "%";
        playerMoneyText.text = "Money: $" + playerMoney;
        waveCountText.text = "Wave: " + currentWaveCount + " / " + maxWaveCount;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GM.Awake();
    }

    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene settingsMenuScene = SceneManager.GetSceneByName("Settings_Menu");

            if (settingsMenuScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync("Settings_Menu");
                Time.timeScale = 1;
            }
            else
            {
                StartCoroutine(LoadSettingsMenuCoroutine());
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaceTurret(100, turret);
        }

        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            RestartGame();
        }
    } 
}
