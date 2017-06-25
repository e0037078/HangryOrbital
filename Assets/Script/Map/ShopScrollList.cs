using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public float price = 1;
    public int numOwned = 0;
    public int index = -1;
}

public class ShopScrollList : MonoBehaviour
{

    public List<Item> itemList;
    public Transform contentPanel;
    // public ShopScrollList otherShop;
    public Text myGoldDisplay;
    public SimpleObjectPool buttonObjectPool;

    public GameObject successfulDisplay = null;
    public GameObject unsuccessfulDisplay = null;

    public GameObject promptBuy = null;
    public Button yes = null;
    public Button no = null;


    // Use this for initialization
    void Start()
    {
        promptBuy.SetActive(false);
        RefreshDisplay();
    }

    void Update()
    {
        UpdateGold();
    }

    void UpdateGold()
    {
        myGoldDisplay.text = "Gold:" + SaveManager.Instance.gold.ToString(".##");
    }

    void RefreshDisplay()
    {
        myGoldDisplay.text = "Gold:" + SaveManager.Instance.gold.ToString();
        RemoveButtons();  
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].index = i;
            itemList[i].price = SaveManager.Instance.costs[i];
            itemList[i].numOwned = SaveManager.Instance.upgrades[i];
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }
    // aka buy item
    public void TryTransferItemToOtherShop(Item item)
    {
        if (SaveManager.Instance.buyUpgrade(item.index)) // can buy
        {
            
            //RemoveItem(item, this);

            RefreshDisplay();
            
            // do successful display && close
            successfulDisplay.SetActive(true);

            StartCoroutine(closeDisplayAfterTime(1f));
            successfulDisplay.GetComponent<Button>().onClick.AddListener(closeSDisplay);

            Debug.Log("enough gold");

        }
        else
        {
            // do unsuccessful display
            unsuccessfulDisplay.SetActive(true);
            StartCoroutine(closeDisplayAfterTime(1f));
            unsuccessfulDisplay.GetComponent<Button>().onClick.AddListener(closeUDisplay);
        }
        Debug.Log("attempted");
    }

    void closeSDisplay()
    {
        SfxManager.PlaySound("Click");
        successfulDisplay.SetActive(false);
    }

    void closeUDisplay()
    {
        SfxManager.PlaySound("Click");
        unsuccessfulDisplay.SetActive(false);
    }

    IEnumerator closeDisplayAfterTime(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        successfulDisplay.SetActive(false);
        unsuccessfulDisplay.SetActive(false);
    }

    void AddItem(Item itemToAdd, ShopScrollList shopList)
    {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Item itemToRemove, ShopScrollList shopList)
    {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemList[i] == itemToRemove)
            {
                shopList.itemList.RemoveAt(i);
            }
        }
    }
}