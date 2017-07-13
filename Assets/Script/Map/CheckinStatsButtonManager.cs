using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckinStatsButtonManager : MonoBehaviour {

    public GameObject statsPanel;
    public GameObject checkinPanel;
    public GameObject shop;
    public GameObject checkin;
    public GameObject closeStats;
    public GameObject closeCheckin;
    public GameObject stats;
    public GameObject settings;

	// Use this for initialization
	void Start ()
    {
        stats.GetComponent<Button>().onClick.AddListener(toggleButtonStats);
        closeStats.GetComponent<Button>().onClick.AddListener(toggleButtonStats);

        checkin.GetComponent<Button>().onClick.AddListener(toggleButtonCheckin);
        closeCheckin.GetComponent<Button>().onClick.AddListener(toggleButtonCheckin);
    }
	
    void toggleButtonStats()
    {
        if (stats.activeSelf)
        {
            shop.SetActive(false);
            checkin.SetActive(false);
            settings.SetActive(false);
            statsPanel.SetActive(true);
            stats.SetActive(false);
        }
        else
        {
            shop.SetActive(true);
            checkin.SetActive(true);
            settings.SetActive(true);
            statsPanel.SetActive(false);
            stats.SetActive(true);

        }

    }

    void toggleButtonCheckin()
    {
        if (checkin.activeSelf)
        {
            shop.SetActive(false);
            stats.SetActive(false);
            settings.SetActive(false);
            checkinPanel.SetActive(true);
            checkin.SetActive(false);
        }
        else
        {
            shop.SetActive(true);
            stats.SetActive(true);
            settings.SetActive(true);
            checkinPanel.SetActive(false);
            checkin.SetActive(true);
        }
    }
}
