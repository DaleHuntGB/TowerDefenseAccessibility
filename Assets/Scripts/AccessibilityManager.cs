using UnityEngine;

public class AccessibilityManager : MonoBehaviour
{
    // Color Profile Variables
    [HideInInspector]
    public class ColorProfile
    {
        public Color gameWallClr;
        public Color gameTileClr;
        public Color startPointClr;
        public Color endPointClr;
        public Color enemyRouteClr;
        public Color enemyClr;
        public Color turretClr;
        public Color turretBulletClr;
        public Color highHealthClr;
        public Color lowHealthClr;
    }

    // Create Instances of Each Colour Profile.
    public ColorProfile defaultProfile;
    public ColorProfile protanopiaProfile;
    public ColorProfile deuteranopiaProfile;
    public ColorProfile tritanopiaProfile;
    public ColorProfile highContrastProfile;
    public ColorProfile greyscaleProfile;
    public ColorProfile customProfile;
    public ColorProfile currentProfile;

    // References to game objects (Set these in the Unity Editor or find them dynamically in Start() or Awake())
    public GameObject gameWall;
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject enemyRoute;
    public GameObject enemy;
    public GameObject turret;
    public GameObject turretBullet;

    public ColorProperty currentColorProperty;
    private void Awake()
    {
        DefineProfiles();
        currentProfile = defaultProfile;
    }

    // Define Each Color For Each Profile.
    public void DefineProfiles()
    {
        defaultProfile = new ColorProfile()
        {
            gameWallClr = new Color(15 / 255f, 15 / 255f, 15 / 255f, 255 / 255f),           // #0F0F0F
            startPointClr = new Color(64 / 255f, 64 / 255f, 255 / 255f, 255 / 255f),        // #4040FF
            endPointClr = new Color(128 / 255f, 64 / 255f, 255 / 255f, 255 / 255f),         // #8040FF
            enemyRouteClr = new Color(128 / 255f, 128 / 255f, 255 / 255f, 255 / 255f),      // #8080FF
            enemyClr = new Color(255 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),             // #FF4040
            turretClr = new Color(255 / 255f, 128 / 255f, 64 / 255f, 255 / 255f),           // #FF8040
            turretBulletClr = new Color(0 / 255f, 170 / 255f, 187 / 255f, 255 / 255f),      // #00AABB
            highHealthClr = new Color(64 / 255f, 255 / 255f, 64 / 255f, 255 / 255f),        // #40FF40
            lowHealthClr = new Color(255 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),         // #FF4040
        };

        protanopiaProfile = new ColorProfile()
        {
            gameWallClr = new Color(15 / 255f, 15 / 255f, 15 / 255f, 255 / 255f),           // #0F0F0F
            startPointClr = new Color(64 / 255f, 128 / 255f, 255 / 255f, 255 / 255f),       // #4080FF
            endPointClr = new Color(128 / 255f, 128 / 255f, 255 / 255f, 255 / 255f),        // #8080FF
            enemyRouteClr = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255 / 255f),      // #808080
            enemyClr = new Color(128 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),             // #804040
            turretClr = new Color(128 / 255f, 128 / 255f, 64 / 255f, 255 / 255f),           // #808040
            turretBulletClr = new Color(0 / 255f, 170 / 255f, 187 / 255f, 255 / 255f),      // #00AABF
            highHealthClr = new Color(64 / 255f, 255 / 255f, 128 / 255f, 255 / 255f),       // #40FF80
            lowHealthClr = new Color(128 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),         // #804040
        };


        deuteranopiaProfile = new ColorProfile()
        {
            gameWallClr = new Color(15 / 255f, 15 / 255f, 15 / 255f, 255 / 255f),           // #0F0F0F
            startPointClr = new Color(64 / 255f, 64 / 255f, 255 / 255f, 255 / 255f),        // #4040FF
            endPointClr = new Color(64 / 255f, 128 / 255f, 255 / 255f, 255 / 255f),         // #4080FF
            enemyRouteClr = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255 / 255f),      // #808080
            enemyClr = new Color(255 / 255f, 128 / 255f, 128 / 255f, 255 / 255f),           // #FF8080
            turretClr = new Color(255 / 255f, 170 / 255f, 64 / 255f, 255 / 255f),           // #FFAA40
            turretBulletClr = new Color(0 / 255f, 170 / 255f, 187 / 255f, 255 / 255f),      // #00AABF
            highHealthClr = new Color(128 / 255f, 255 / 255f, 128 / 255f, 255 / 255f),      // #80FF80
            lowHealthClr = new Color(255 / 255f, 128 / 255f, 128 / 255f, 255 / 255f),       // #FF8080
        };


        tritanopiaProfile = new ColorProfile()
        {
            gameWallClr = new Color(15 / 255f, 15 / 255f, 15 / 255f, 255 / 255f),           // #0F0F0F
            startPointClr = new Color(255 / 255f, 255 / 255f, 64 / 255f, 255 / 255f),       // #FFFF40
            endPointClr = new Color(255 / 255f, 255 / 255f, 128 / 255f, 255 / 255f),        // #FFFF80
            enemyRouteClr = new Color(255 / 255f, 255 / 255f, 128 / 255f, 255 / 255f),      // #FFFF80
            enemyClr = new Color(255 / 255f, 128 / 255f, 64 / 255f, 255 / 255f),            // #FF8040
            turretClr = new Color(255 / 255f, 170 / 255f, 128 / 255f, 255 / 255f),          // #FFAA80
            turretBulletClr = new Color(128 / 255f, 170 / 255f, 0 / 255f, 255 / 255f),      // #80AA00
            highHealthClr = new Color(128 / 255f, 255 / 255f, 64 / 255f, 255 / 255f),       // #80FF40
            lowHealthClr = new Color(255 / 255f, 128 / 255f, 64 / 255f, 255 / 255f),        // #FF8040
        };


        highContrastProfile = new ColorProfile()
        {
            gameWallClr = new Color(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f),              // #000000
            startPointClr = new Color(255 / 255f, 255 / 255f, 0 / 255f, 255 / 255f),        // #FFFF00
            endPointClr = new Color(0 / 255f, 255 / 255f, 0 / 255f, 255 / 255f),            // #00FF00
            enemyRouteClr = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f),          // #FF0000
            enemyClr = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f),               // #FF0000
            turretClr = new Color(0 / 255f, 0 / 255f, 255 / 255f, 255 / 255f),              // #0000FF
            turretBulletClr = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f),    // #FFFFFF
            highHealthClr = new Color(0 / 255f, 255 / 255f, 0 / 255f, 255 / 255f),          // #00FF00
            lowHealthClr = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f),           // #FF0000
        };


        greyscaleProfile = new ColorProfile()
        {
            gameWallClr = new Color(15 / 255f, 15 / 255f, 15 / 255f, 255 / 255f),           // #0F0F0F
            startPointClr = new Color(64 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),         // #404040
            endPointClr = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255 / 255f),        // #808080
            enemyRouteClr = new Color(192 / 255f, 192 / 255f, 192 / 255f, 255 / 255f),      // #C0C0C0
            enemyClr = new Color(64 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),              // #404040
            turretClr = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255 / 255f),          // #808080
            turretBulletClr = new Color(192 / 255f, 192 / 255f, 192 / 255f, 255 / 255f),    // #C0C0C0
            highHealthClr = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f),      // #FFFFFf
            lowHealthClr = new Color(64 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),          // #404040
        };

        customProfile = new ColorProfile()
        {
            gameWallClr = new Color(15 / 255f, 15 / 255f, 15 / 255f, 255 / 255f),           // #0F0F0F
            startPointClr = new Color(64 / 255f, 64 / 255f, 255 / 255f, 255 / 255f),        // #4040FF
            endPointClr = new Color(128 / 255f, 64 / 255f, 255 / 255f, 255 / 255f),         // #8040FF
            enemyRouteClr = new Color(128 / 255f, 128 / 255f, 255 / 255f, 255 / 255f),      // #8080FF
            enemyClr = new Color(255 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),             // #FF4040
            turretClr = new Color(255 / 255f, 128 / 255f, 64 / 255f, 255 / 255f),           // #FF8040
            turretBulletClr = new Color(0 / 255f, 170 / 255f, 187 / 255f, 255 / 255f),      // #00AABB
            highHealthClr = new Color(64 / 255f, 255 / 255f, 64 / 255f, 255 / 255f),        // #40FF40
            lowHealthClr = new Color(255 / 255f, 64 / 255f, 64 / 255f, 255 / 255f),         // #FF4040
        };
    }

    // Improved Colour Differences for Protanopia, Deuteranopia, Tritanopia & Greyscale.
    // https://www.colorhexa.com
    /*
        
        #0F0F0F // Game Wall
        Protanopia: #1B1B1B 
        Deuteranopia: #1D1A1B
        Tritanopia: #1B1B1D
        Greyscale: #0F0F0F
        
        #4040FF // Start Point
        Protanopia: #0066C6
        Deuteranopia: #006CAC
        Tritanopia: #007379
        Greyscale: #565656

        #8040FF // End Point
        Protanopia: #0071DD
        Deuteranopia: #0078C1
        Tritanopia: #577A83
        Greyscale: #696969

        #8080FF // Enemy Route
        Protanopia: #5790FF
        Deuteranopia: #2B96F8
        Tritanopia: #639DA8
        Greyscale: #8E8E8E

        #FF4040 // Enemy
        Protanopia: #9C8F5E
        Deuteranopia: #B1893B
        Tritanopia: #FF494C
        Greyscale: #797979

        #FF8040 // Turret
        Protanopia: #BAA853
        Deuteranopia: #D0A041
        Tritanopia: #FF8189
        Greyscale: #9F9F9F

        #00AABB // Turret Bullet
        Protanopia: #9B9EB4
        Deuteranopia: #999CC5
        Tritanopia: #00AEBB
        Greyscale: #797979

        #40FF40 // High Health
        Protanopia: #F9E041
        Deuteranopia: #FFD9A8
        Tritanopia: #8CEEFF
        Greyscale: #B0B0B0

        #FF4040 // Low Health
        Protanopia: #9C8F5E
        Deuteranopia: #B1893B
        Tritanopia: #FF494C
        Greyscale: #797979
     */

    // Apply Colors from Color Profiles.
    public void ApplyColorProfile(ColorProfile profile)
    {
        currentProfile = profile;

        // Get Each Material Color & Assign the new colour from the specific profile.
        gameWall.GetComponent<Renderer>().sharedMaterial.color = profile.gameWallClr;
        startPoint.GetComponent<Renderer>().sharedMaterial.color = profile.startPointClr;
        endPoint.GetComponent<Renderer>().sharedMaterial.color = profile.endPointClr;
        enemyRoute.GetComponent<Renderer>().sharedMaterial.color = profile.enemyRouteClr;
        enemy.GetComponent<Renderer>().sharedMaterial.color = profile.enemyClr;
        turret.GetComponent<Renderer>().sharedMaterial.color = profile.turretClr;
        turretBullet.GetComponent<Renderer>().sharedMaterial.color = profile.turretBulletClr;
    }
    // Switch Color Profile
    public void SwitchColorProfile(string mode)
    {
        switch (mode)
        {
            case "Default":
                ApplyColorProfile(defaultProfile);
                break;
            case "Protanopia":
                ApplyColorProfile(protanopiaProfile);
                break;
            case "Deuteranopia":
                ApplyColorProfile(deuteranopiaProfile);
                break;
            case "Tritanopia":
                ApplyColorProfile(tritanopiaProfile);
                break;
            case "HighContrast":
                ApplyColorProfile(highContrastProfile);
                break;
            case "Greyscale":
                ApplyColorProfile(greyscaleProfile);
                break;
            case "Custom":
                ApplyColorProfile(customProfile);
                break;
        }
    }

    public ColorProfile GetCurrentProfile()
    {
        return currentProfile;
    }

    public void UpdateCustomProfile(ColorProperty property, Color newColor)
    {
        switch (property)
        {
            case ColorProperty.GameWall:
                customProfile.gameWallClr = newColor;
                break;
            case ColorProperty.StartPoint:
                customProfile.startPointClr = newColor;
                break;
            case ColorProperty.EndPoint:
                customProfile.endPointClr = newColor;
                break;
            case ColorProperty.EnemyRoute:
                customProfile.enemyRouteClr = newColor;
                break;
            case ColorProperty.Enemy:
                customProfile.enemyClr = newColor;
                break;
            case ColorProperty.Turret:
                customProfile.turretClr = newColor;
                break;
            case ColorProperty.TurretBullet:
                customProfile.turretBulletClr = newColor;
                break;
            case ColorProperty.HighHealth:
                customProfile.highHealthClr = newColor;
                break;
            case ColorProperty.LowHealth:
                customProfile.lowHealthClr = newColor;
                break;
        }
        ApplyColorProfile(customProfile);
    }
}


