using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //units and resourses count

    public int resourses = 10;
    private int totalResourses;
    private int startResourses = 10;
    private int searchersCount = 2;
    private int totalSearchers;
    public int battleshipsCount = 0;
    private int totalBattleShips;
    public int enemyCount = 0;
    private int waveCount = 0;
    private int wavesTillAttack = 3;
    //cost count
    private int searcherBringResoursesCount = 5;
    private int battleshipEatingCount = 8;

    private int searcherCost = 5;
    private int battleshipCost = 10;


    private bool isWon;
    private bool isLost;
    public bool gameStopped;
    public bool buyingSearcher;
    public bool buyingBattleShip;
    private bool isAttacking;
    public bool playMusic;
    //gameobjects
    public GameObject pausePanel;
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject wonPanel;
    public GameObject lostPanel;
    public GameObject invasionObject;
    public GameObject gameConditions;
    public GameObject waveAfterAttention;
    public GameObject waveText;
    public GameObject gameInfo;

    public Text enemyCountText;
    public Text searchersCountText;
    public Text battleshipsCountText;
    public Text resoursesCountText;
    public Text waveNumberText;
    public Text invasionAttentionNumber;
    public Text currentInvasionText;
    public Text totalResoursesNumberText;
    public Text totalBattleShipsNumberText;
    public Text totalSearchersNumberText;
    public AudioSource music;

    public Timers timers;

    //buttons
    public Button searchersBuyButton;
    public Button battleshipsBuyButton;
    void Start()
    {
        playMusic = true;
        music.Play();
        gameStopped = true;
        timers = GetComponent<Timers>();
        music = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        StartEnemyInvasion();
        DecreaseResourses();
        CheckButtonsStatus();
        CheckUnits();
        Resourses();
        WinConditions();
        if (isWon)
            WonGame();
        if (isLost)
            LostGame();
    }
    public void RestartGame()
    {
        resourses = startResourses;
        resoursesCountText.text = resourses.ToString();
        searchersCount = 2;
        searchersCountText.text = searchersCount.ToString();
        battleshipsCount = 0;
        battleshipsCountText.text = battleshipsCount.ToString();
        totalResourses = resourses;
        totalSearchers = searchersCount;
        totalBattleShips = battleshipsCount;
        waveAfterAttention.SetActive(false);
        wavesTillAttack = 3;
        invasionObject.SetActive(true);
        invasionAttentionNumber.text = wavesTillAttack.ToString();
        timers.RestartTimers();
        isLost = false;
        isWon = false;
        buyingSearcher = false;
        buyingBattleShip = false;
        gameStopped = false;
        waveCount = 0;
        enemyCount = 0;
        enemyCountText.text = enemyCount.ToString();
        waveText.SetActive(true);
        gameInfo.SetActive(false);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        menuPanel.SetActive(false);
        wonPanel.SetActive(false);
        lostPanel.SetActive(false);
        gameConditions.SetActive(true);
    }
    public void BackToMenu()
    {
        gameStopped = true;
        gameConditions.SetActive(false);
        wonPanel.SetActive(false);
        lostPanel.SetActive(false);
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void PauseGame()
    {
        gameStopped = !gameStopped;
        if (gameStopped)
            pausePanel.SetActive(true);
        else
            pausePanel.SetActive(false);
    }
    public void StartEnemyInvasion()
    {
        if (!gameStopped)
        {
            timers.EnemyInvasionTimer();
            if (timers.invasionTimer <= 0)
            {
                isAttacking = true;
                waveCount++;
                wavesTillAttack--;
                invasionAttentionNumber.text = wavesTillAttack.ToString();
                if (wavesTillAttack < 0)
                {
                    waveText.SetActive(false);
                    waveAfterAttention.SetActive(true);
                    waveNumberText.text = waveCount.ToString();
                }
                timers.invasionTimer = timers.nextInvasionTimer;
                timers.invasionWaitingImage.fillAmount = 1;
                if (waveCount >= 3 && isAttacking)
                {
                    LoseConditions();
                    battleshipsCount -= enemyCount;
                    battleshipsCountText.text = battleshipsCount.ToString();
                    enemyCount++;
                    enemyCountText.text = enemyCount.ToString();
                }
                isAttacking = false;
            }
        }
    }
    public void WonGame()
    {
        gameStopped = true;
        wonPanel.SetActive(true);
        gameInfo.SetActive(true);
    }
    public void LostGame()
    {
        gameStopped = true;
        lostPanel.SetActive(true);
        gameInfo.SetActive(true);
    }
    private void LoseConditions()
    {
        if (enemyCount > battleshipsCount)
        {
            isLost = true;
        }
    }
    private void WinConditions()
    {
        if (resourses > 5000 || searchersCount > 200)
        {
            isWon = true;
            searchersBuyButton.interactable = false;
            battleshipsBuyButton.interactable = false;
        }
    }
    private void Resourses()
    {
        if (!gameStopped)
        {
            if (timers.resoursesTimer <= 0)
            {
                resourses += searcherBringResoursesCount * searchersCount;
                resoursesCountText.text = resourses.ToString();
                totalResourses += searcherBringResoursesCount * searchersCount;
                totalResoursesNumberText.text = totalResourses.ToString();
                timers.resoursesTimer = timers.startResoursesTimer;
            }
        }
    }
    public void BuySearchers()
    {
        if (!gameStopped)
        {
            searchersBuyButton.interactable = false;
            if (resourses >= searcherCost)
            {
                buyingSearcher = true;
                resourses -= searcherCost;
                resoursesCountText.text = resourses.ToString();
            }
        }
    }
    private void DecreaseResourses()
    {
        if (battleshipsCount > 0)
        {
            if (timers.decreasingResoursesTimer <= 0)
            {
                if (resourses < battleshipsCount * battleshipEatingCount)
                    battleshipsCount--;
                else
                {
                    timers.decreasingResoursesTimer = timers.resoursesTimerForDecrease;
                    resourses -= battleshipsCount * battleshipEatingCount;
                    resoursesCountText.text = resourses.ToString();
                }
            }
        }
    }
    public void BuyBattleships()
    {
        if (!gameStopped)
        {
            if (resourses >= battleshipCost)
            {
                battleshipsBuyButton.interactable = false;
                buyingBattleShip = true;
                resourses -= battleshipCost;
                resoursesCountText.text = resourses.ToString();
            }
        }
    }
    private void CheckButtonsStatus()
    {
        if (resourses >= searcherCost && !buyingSearcher)
            searchersBuyButton.interactable = true;
        else
            searchersBuyButton.interactable = false;
        if (resourses >= battleshipCost && !buyingBattleShip)
            battleshipsBuyButton.interactable = true;
        else
            battleshipsBuyButton.interactable = false;
    }
    private void CheckUnits()
    {
        if (buyingSearcher)
        {
            timers.UnitsTimer();
            if (timers.searcherTimer <= 0)
            {
                searchersCount++;
                searchersCountText.text = searchersCount.ToString();
                totalSearchers++;
                totalSearchersNumberText.text = totalSearchers.ToString();
                searchersBuyButton.interactable = true;
                timers.searcherTimer = timers.searcherBuyTime;
                buyingSearcher = false;
            }
        }
        if (buyingBattleShip)
        {
            timers.UnitsTimer();
            if (timers.battleshipTimer <= 0)
            {
                battleshipsCount++;
                battleshipsCountText.text = battleshipsCount.ToString();
                totalBattleShips++;
                totalBattleShipsNumberText.text = totalBattleShips.ToString();
                battleshipsBuyButton.interactable = true;
                timers.battleshipTimer = timers.battleshipBuyTime;
                buyingBattleShip = false;
            }
        }
    }
    public void ChangeSoundAndImageState()
    {
        playMusic = !playMusic;
        if (playMusic)
            music.Play();
        else
            music.Stop();
    }
}
