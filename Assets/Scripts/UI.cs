using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class for game UI.
/// </summary>
public class UI : MonoBehaviour {

    public static Slider healthSlider;
    public static Text healthText;
    public static Slider ammoSlider;
    public static Text ammoText;
    public static Text remainingLivesText;
    public static GameObject HUD;
    public static GameObject pauseMenu;
    public static GameObject helpMenu;
    public static GameObject creditsMenu;
    public static GameObject startMenu;
    public static GameObject highScoreMenu;
    public static GameObject endGameMenu;
    public static GameObject selectMenu;
    public static GameObject selectMenu2;

    public static GameObject mainCamera;
    public static GameObject startMenuCamera;

    public static string backToWhere;
    public static Player player;


    private void Start()
    {
        //get all components and objects
        healthSlider = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Slider>();
        healthText = transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>();
        ammoSlider = transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Slider>();
        ammoText = transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>();
        remainingLivesText = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>();
        HUD = transform.GetChild(0).gameObject;
        pauseMenu = transform.GetChild(1).gameObject;
        helpMenu = transform.GetChild(2).gameObject;
        creditsMenu = transform.GetChild(3).gameObject;
        startMenu = transform.GetChild(4).gameObject;
        selectMenu = transform.GetChild(5).gameObject;
        selectMenu2 = transform.GetChild(6).gameObject;
        highScoreMenu = transform.GetChild(7).gameObject;
        endGameMenu = transform.GetChild(8).gameObject;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        startMenuCamera = GameObject.FindGameObjectWithTag("startMenuCamera");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        StartMenu();
    }

    /// <summary>
    /// changes the health and ammo UI.
    /// </summary>
    public static void setValues(Player player)
    {
        healthSlider.maxValue = Player.maxHealth;
        healthSlider.value = player.currentHealth;
        healthText.text = player.currentHealth + "/" + Player.maxHealth;
        ammoSlider.maxValue = player.weapon.maxMagazine;
        ammoSlider.value = player.weapon.currentMagazine;
        ammoText.text = player.weapon.currentMagazine + "/" + player.weapon.maxMagazine;
        remainingLivesText.text = player.remainingLives + " lives";
    }

    /// <summary>
    /// shows the start menu.
    /// </summary>
    public static void StartMenu()
    {
        Time.timeScale = 0f;

        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
        creditsMenu.SetActive(false);
        selectMenu.SetActive(false);
        selectMenu2.SetActive(false);
        highScoreMenu.SetActive(false);
        startMenu.SetActive(true);
        mainCamera.SetActive(false);
        endGameMenu.SetActive(false);

        startMenuCamera.SetActive(true);
    }

    public static void SelectMenu()
    {
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        selectMenu.SetActive(true);
    }

    public static void HighScore()
    {
        if (startMenu.activeSelf)
        {
            startMenu.SetActive(false);
            backToWhere = "start";

        }
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            backToWhere = "pause";
        }
        highScoreMenu.SetActive(true);
    }

    /// <summary>
    /// Pause game and show pause menu.
    /// </summary>
    public static void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Unpause game and hide all menus.
    /// </summary>
    public static void Continue()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
        creditsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        selectMenu.SetActive(false);
        selectMenu2.SetActive(false);
        startMenu.SetActive(false);

        HUD.SetActive(true);
        mainCamera.SetActive(true);
        startMenuCamera.SetActive(false);
    }


    /// <summary>
    /// shows the help menu.
    /// </summary>
    public static void Help()
    {
        if (startMenu.activeSelf)
        {
            startMenu.SetActive(false);
            backToWhere = "start";

        }
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            backToWhere = "pause";
        }
        helpMenu.SetActive(true);
    }

    /// <summary>
    /// shows the credits menu.
    /// </summary>
    public static void Credits()
    {
        if (startMenu.activeSelf)
        {
            startMenu.SetActive(false);
            backToWhere = "start";

        }
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            backToWhere = "pause";
        }
        creditsMenu.SetActive(true);
    }

    /// <summary>
    /// quits the game.
    /// </summary>
    public static void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// go back to pause menu.
    /// </summary>
    public static void Back()
    {
        if (backToWhere == "start")
        {
            startMenu.SetActive(true);

        }
        if (backToWhere == "pause")
        {
            pauseMenu.SetActive(true);
        }
        helpMenu.SetActive(false);
        creditsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        endGameMenu.SetActive(false);
    }

    public static void EndGame()
    {
        Time.timeScale = 0f;
        backToWhere = "start";
        player.Restart();
        Enemy.Restart();
        endGameMenu.SetActive(true);
    }
    /// <summary>
    /// Changes player's stuff.
    /// </summary>
    public static void Select(string what)
    {
        switch (what)
        {
            case "assaultRifle":
                player.ChangeSelection(weapon: Player.AssaultRiflePrefab);
                selectMenu.SetActive(false);
                selectMenu2.SetActive(true);
                break;
            case "shotgun":
                player.ChangeSelection(weapon: Player.ShotgunPrefab);
                selectMenu.SetActive(false);
                selectMenu2.SetActive(true);
                break;
            case "minigun":
                player.ChangeSelection(weapon: Player.MinigunPrefab);
                selectMenu.SetActive(false);
                selectMenu2.SetActive(true);
                break;
            case "fireBullet":
                player.ChangeSelection(bullet: Player.FireBulletPrefab);
                Continue();
                break;
            case "explosiveBullet":
                player.ChangeSelection(bullet: Player.ExplosiveBulletPrefab);
                Continue();
                break;
            case "classicBullet":
                player.ChangeSelection(bullet: Player.ClassicBulletPrefab);
                Continue();
                break;
            default:
                break;
        }
    }

    // OnClick requires non static methods so these are wrappers for those methods
    public void SelectMenuWrapper() { SelectMenu(); }
    public void HighScoreWrapper() { HighScore(); }
    public void ContinueWrapper() { Continue(); }
    public void HelpWrapper() { Help(); }
    public void CreditsWrapper() { Credits(); }
    public void QuitWrapper() { Quit(); }
    public void BackWrapper() { Back(); }
    public void SelectWrapper(string what) { Select(what); }

}
