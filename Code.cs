using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class IdleGame : MonoBehaviour
{
// useful chars = \

    /*
        TODO:
        - Add upgrades people upgrades gif when you upgrade



    */

    // music
    public bool isMute; 

    // time
    public Text timeText;
    public DateTime time;

    // text
    public Text coinsText;
    public Text coinsPerSecondText;
    public int coinsClickValue;

    // numbers
    public double coins;
    public double coinsPerSecondValue;

    // click upgrade
    public Text clickUp;
    public double clickUpCost;
    public int clickUpLevel;

    // production upgrade
    public Text prodUp;
    public double prodUpCost;
    public int prodUpLevel;

    // Start is called before the first frame update
    void Start()
    {
        load();
    }

    // Update is called once per frame
    void Update()
    {   
        DateTime time = DateTime.Now;
        timeText.text = time.ToString("h:mm:ss tt");
        coinsText.text = "Shrimps: " + coins.ToString("F0");
        coins += coinsPerSecondValue * Time.deltaTime;

        coinsPerSecondValue = prodUpLevel;
        coinsPerSecondText.text = coinsPerSecondValue.ToString("F0") + " Shrimps/s";
        clickUp.text = "Click Upgrade\nCost: " + clickUpCost.ToString("F0") + "\nPower: +1 Shrimp/c\nLevel: " + clickUpLevel.ToString("F0");
        prodUp.text = "Production Upgrade\nCost: " + prodUpCost.ToString("F0") + "\nPower: +1 Shrimp/s\nLevel: " + prodUpLevel.ToString("F0");
    
        save();
    }

    public void Click()
    {
        coins += coinsClickValue;
    }

    // upgrades

    public void upgradeProduction()
    {
        if (coins >= prodUpCost) {
            prodUpLevel++;
            coins -= prodUpCost;
            prodUpCost = prodUpCost * 1.07;
        }
    }

    public void upgradeClick()
    {
        if (coins >= clickUpCost) {
            clickUpLevel++;
            coins -= clickUpCost;
            clickUpCost = clickUpCost * 1.07;
            coinsClickValue++;
        }
    }

    // music Mute

    public void Mute()
    {
        isMute = ! isMute;
        AudioListener.volume =  isMute ? 0 : 1;
    }

    // load and save

    public void save()
    {
        PlayerPrefs.SetString("coins", coins.ToString());
        PlayerPrefs.SetString("clickUpCost",clickUpCost.ToString());
        PlayerPrefs.SetString("prodUpCost",prodUpCost.ToString());

        PlayerPrefs.SetInt("coinsClickValue",coinsClickValue);
        PlayerPrefs.SetInt("clickUpLevel",clickUpLevel);
        PlayerPrefs.SetInt("prodUpLevel",prodUpLevel);
    }

    public void load()
    {
        coins = double.Parse(PlayerPrefs.GetString("coins","0"));
        clickUpCost = double.Parse(PlayerPrefs.GetString("clickUpCost","100"));
        prodUpCost = double.Parse(PlayerPrefs.GetString("prodUpCost","50"));

        coinsClickValue = PlayerPrefs.GetInt("coinsClickValue",1);
        clickUpLevel = PlayerPrefs.GetInt("clickUpLevel",0);
        prodUpLevel = PlayerPrefs.GetInt("prodUpLevel",0);
    }
}
