using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateStats : MonoBehaviour {
    public Text HP;
    public Text DPS;
    public Text Level;

    public Text LineDMG;
    public Text LineProb;

    public Text BackDMG;
    public Text BackProb;

    public Text ForwardDMG;
    public Text ForwardProb;

    public Text LightningDMG;
    public Text LightningProb;

    public Text FireBallDMG;
    public Text FireBallProb;


    public void update()
    {
        HP.text = "HP  : " +  SaveManager.Instance.BaseHP;
        DPS.text = "DPS : " + SaveManager.Instance.DPS;
        Level.text = "LEVEL: " + SaveManager.Instance.level;

        LineDMG.text = "DMG :" + SaveManager.Instance.gestureDMG[0];
        LineProb.text = "Prob:" + SaveManager.Instance.gestureProb[0].ToString("0.##");

        BackDMG.text = "DMG :" + SaveManager.Instance.gestureDMG[1];
        BackProb.text = "Prob:" + SaveManager.Instance.gestureProb[1].ToString("0.##");

        ForwardDMG.text = "DMG :" + SaveManager.Instance.gestureDMG[2];
        ForwardProb.text = "Prob:" + SaveManager.Instance.gestureProb[2].ToString("0.##");

        LightningDMG.text = "DMG :" + SaveManager.Instance.gestureDMG[3];
        LightningProb.text = "Prob:" + SaveManager.Instance.gestureProb[3].ToString("0.##");

        FireBallDMG.text = "DMG :" + SaveManager.Instance.gestureDMG[4];
        FireBallProb.text = "Prob:" + SaveManager.Instance.gestureProb[4].ToString("0.##");

    }
}
