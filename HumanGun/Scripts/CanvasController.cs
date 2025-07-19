using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Orbay.Core.GameManager;
using Orbay.Core.Singelton;
using TMPro;

public class CanvasController : Singelton<CanvasController>
{
    [Header("Stickman Prefab")]
    public GameObject stickmanPrefab;

    [Header("Panel")]
    public GameObject startUpPanel,winPanel,failPanel,fixedPanel,upgradePanel,startUpgPanel;

    [Header("Components")]
    public Slider loadingSlider;

    public Text levelText, moneyText,dmgCost,rangeCost,stickMCost,moneyCost;

    public TextMeshProUGUI earnedText, dmgLvLText,rangeLvlText,stickMLvlText,moneyLvlText;

    

   [SerializeField] private GameObject moneyPrefab;

    bool isFinishTrigger;
    private void Start()
    {
        EventHandler.finishTrigger += OnFinishTrigger;
        EventHandler.endGame += OnGameEnd;

        levelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);
        moneyText.text = "$" + PlayerPrefs.GetInt("Money", 0);

        dmgLvLText.text = "Level " + PlayerPrefs.GetInt("DmgLevel", 1);
        dmgCost.text = "$" + PlayerPrefs.GetInt("DmgCost", 50);

        rangeLvlText.text = "Level " + PlayerPrefs.GetInt("RangeLevel", 1);
        rangeCost.text = "$" + PlayerPrefs.GetInt("RangeCost", 50);

        moneyLvlText.text = "Level " + PlayerPrefs.GetInt("MoneyLevel", 1);
        moneyCost.text = "$" + PlayerPrefs.GetInt("MoneyCost", 5);
    }
  
    public void NextScene()
    {
        var tmp = PlayerPrefs.GetInt("Level", 1);
        tmp++;
        PlayerPrefs.SetInt("Level", tmp);
        levelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);
        LevelManager.Instance.LoadScene(tmp);
        earnedText.text = "$0";
        CanvasController.Instance.startUpgPanel.SetActive(true);




    }

    public void ResetLevel()
    {
        LevelManager.Instance.LoadScene(PlayerPrefs.GetInt("Level", 1));
    }

    void OnGameEnd()
    {
        if (isFinishTrigger) winPanel.SetActive(true);
        else failPanel.SetActive(true);
       
        isFinishTrigger = false;
    }

    void OnFinishTrigger(float tmp)
    {
        isFinishTrigger = true;
    }
    

    public void MoneyCollected()
    {
        Instantiate(moneyPrefab,transform);
    }

    public void AddMoney()
    {
        earnedText.text = "$" + ((int.Parse(earnedText.text[1..]) + 1)+ (PlayerPrefs.GetInt("MoneyLevel", 1)-1));
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0)+ PlayerPrefs.GetInt("Income",1));
        moneyText.text = "$" + PlayerPrefs.GetInt("Money", 0);
    }

    public void UpgradeMoney()
    {
        if (PlayerPrefs.GetInt("Money", 0) < PlayerPrefs.GetInt("MoneyCost", 5)) return;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - PlayerPrefs.GetInt("MoneyCost", 5));
        moneyText.text = "$" + PlayerPrefs.GetInt("Money", 0);

        PlayerPrefs.SetInt("MoneyLevel", PlayerPrefs.GetInt("MoneyLevel", 1) + 1);
        PlayerPrefs.SetInt("MoneyCost", PlayerPrefs.GetInt("MoneyCost", 5) + 5);

        PlayerPrefs.SetInt("Income", PlayerPrefs.GetInt("Income", 1) + 1);

        moneyLvlText.text = "Level " + PlayerPrefs.GetInt("MoneyLevel", 1);
        moneyCost.text = "$" + PlayerPrefs.GetInt("MoneyCost", 5);
    }
    public void UpgradeDmg()
    {
        if (PlayerPrefs.GetInt("Money", 0) < PlayerPrefs.GetInt("DmgCost", 5)) return;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - PlayerPrefs.GetInt("DmgCost", 5));
        moneyText.text = "$" + PlayerPrefs.GetInt("Money", 0);

        PlayerPrefs.SetInt("DmgLevel", PlayerPrefs.GetInt("DmgLevel", 1)+1);
        PlayerPrefs.SetInt("DmgCost", PlayerPrefs.GetInt("DmgCost", 50)+50);

        dmgLvLText.text = "Level " + PlayerPrefs.GetInt("DmgLevel", 1); 
        dmgCost.text = "$"+ PlayerPrefs.GetInt("DmgCost", 50);
    }


    public void UpgradeRange()
    {
        if (PlayerPrefs.GetInt("Money", 0) < PlayerPrefs.GetInt("RangeCost", 5)) return;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - PlayerPrefs.GetInt("RangeCost", 5));
        moneyText.text = "$" + PlayerPrefs.GetInt("Money", 0);

        PlayerPrefs.SetInt("RangeLevel", PlayerPrefs.GetInt("RangeLevel", 1) + 1);
        PlayerPrefs.SetInt("RangeCost", PlayerPrefs.GetInt("RangeCost", 50) + 50);

        rangeLvlText.text = "Level " + PlayerPrefs.GetInt("RangeLevel", 1);
        rangeCost.text = "$" + PlayerPrefs.GetInt("RangeCost", 50);
    }

   public void UpgradeStickM()
    {
        if (PlayerPrefs.GetInt("Money", 0) < PlayerPrefs.GetInt("stickMCost", 5)) return;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - PlayerPrefs.GetInt("stickMCost", 5));
        moneyText.text = "$" + PlayerPrefs.GetInt("Money", 0);

        GameObject tmp = Instantiate(stickmanPrefab);
       
        PlayerPrefs.SetInt("stickMLevel", PlayerPrefs.GetInt("stickMLevel", 1) + 1);
        PlayerPrefs.SetInt("stickMCost", PlayerPrefs.GetInt("stickMCost", 50) + 50);

        stickMLvlText.text = "Level " + PlayerPrefs.GetInt("stickMLevel", 1);
        stickMCost.text = "$" + PlayerPrefs.GetInt("stickMCost", 50);

        EventHandler.collectStickM.Invoke(tmp.GetInstanceID());
        EventHandler.updateList.Invoke(tmp);
    }

}
