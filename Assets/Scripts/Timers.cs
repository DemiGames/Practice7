using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timers : MonoBehaviour
{
    //timers
    public float resoursesTimer;
    public float startResoursesTimer;
    public float decreasingResoursesTimer;
    public float resoursesTimerForDecrease;
    public float nextInvasionTimer;
    public float invasionTimer;
    public float searcherTimer;
    public float battleshipTimer;
    public float searcherBuyTime;
    public float battleshipBuyTime;
    //images
    public Image searcherWaitingImage;
    public Image battleshipWaitingImage;
    public Image invasionWaitingImage;
    public Image decreasingResoursesWaitingImage;
    public Image resoursesWaitingImage;
    public GameManager gm;
    private void Start()
    {
        gm = GetComponent<GameManager>();
    }
    private void Update()
    {
        EnemyInvasionTimer();
        ResoursesTimer();
        UnitsTimer();
    }
    public void RestartTimers()
    {
        battleshipTimer = battleshipBuyTime;
        searcherTimer = searcherBuyTime;
        resoursesTimer = startResoursesTimer;
        decreasingResoursesTimer = resoursesTimerForDecrease;
        invasionTimer = nextInvasionTimer;
        battleshipWaitingImage.fillAmount = 0;
        searcherWaitingImage.fillAmount = 0;
        resoursesWaitingImage.fillAmount = 1;
        invasionWaitingImage.fillAmount = 1;
        decreasingResoursesWaitingImage.fillAmount = 1;
    }
    public void EnemyInvasionTimer()
    {
        if (!gm.gameStopped)
        {
            invasionTimer -= Time.deltaTime;
            invasionWaitingImage.fillAmount = invasionTimer / nextInvasionTimer;
        }
    }
    public void ResoursesTimer()
    {
        if (!gm.gameStopped)
        {
            resoursesTimer -= Time.deltaTime;
            resoursesWaitingImage.fillAmount = resoursesTimer / startResoursesTimer;
        }
        if (gm.battleshipsCount > 0 && !gm.gameStopped)
        {
            decreasingResoursesTimer -= Time.deltaTime;
            decreasingResoursesWaitingImage.fillAmount = decreasingResoursesTimer / resoursesTimerForDecrease;
        }
    }
    public void UnitsTimer()
    {
        if (gm.buyingSearcher && !gm.gameStopped)
        {
            searcherTimer -= Time.deltaTime;
            searcherWaitingImage.fillAmount = searcherTimer / searcherBuyTime;
        }
        else
            searcherWaitingImage.fillAmount = 0;
        if (gm.buyingBattleShip && !gm.gameStopped)
        {
            battleshipTimer -= Time.deltaTime;
            battleshipWaitingImage.fillAmount = battleshipTimer / battleshipBuyTime;
        }
        else
            battleshipWaitingImage.fillAmount = 0;
    }
}
