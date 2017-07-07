using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SampleButton : MonoBehaviour
{

    public Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public Text priceText;
    public Text numOwned;
    public GameObject DescriptionPanel;


    private Item item;
    private ShopScrollList scrollList;

    EventTrigger eventTrigger = null;


    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
        eventTrigger = gameObject.GetComponent<EventTrigger>();
        AddEventTrigger(OnPointerDown, EventTriggerType.PointerDown);
        AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);
            
    }
    private void OnPointerDown()
    {
        scrollList.openDescription(item.itemName, item.description);
        Debug.Log("down");
    }
    private void OnPointerUp()
    {
        scrollList.closeDescription();
        scrollList.TryTransferItemToOtherShop(item);
        Debug.Log("up");
    }
    //Taken from https://github.com/AyARL/UnityGUIExamples/blob/master/EventTrigger/Assets/TriggerSetup.cs
    // Use listener with no parameters
    private void AddEventTrigger(UnityAction action, EventTriggerType triggerType)
    {
        // Create a nee TriggerEvent and add a listener
        EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
        trigger.AddListener((eventData) => action()); // ignore event data

        // Create and initialise EventTrigger.Entry using the created TriggerEvent
        EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

        // Add the EventTrigger.Entry to delegates list on the EventTrigger
        //eventTrigger.delegates.Add(entry);
        eventTrigger.triggers.Add(entry);
    }

    public void Setup(Item currentItem, ShopScrollList currentScrollList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        iconImage.sprite = item.icon;
        priceText.text = item.price.ToString("###.#") + " Lvl" + item.numOwned;
        scrollList = currentScrollList;

    }

    public void HandleClick()
    {
        SfxManager.PlaySound("Click");
        scrollList.TryTransferItemToOtherShop(item);
    }
}