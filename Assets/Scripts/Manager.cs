using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    //resources
    ResourceDetail resources;

    //List of available Events
    List<Event> availableEvents;

    //Event Queue
    List<Event> eventQueue;

    private Event activeEvent;

    //Store
    private Store store;

    //timer
    public int hour;
    public int timer;
    private bool endOfDay;
    private bool timerPaused;

    //UI canvas reference
    public Canvas canvas;

	// Use this for initialization
	void Start () {
        // initial values for resources 
        resources = new ResourceDetail(new ResourceList(0, 0, 0, 0, 0), new ResourceList(10, 10, 10, 0, 0));

        //initialize lists
        availableEvents = new List<Event>();
        eventQueue = new List<Event>();
        activeEvent = new Event("", "", new EventOption[] { });

        //populate initial events
        availableEvents.Add(new Event("Farm House Burns Down", "A fire burned down the farm house!", new EventOption[] { new EventOption("Oh no!", new ResourceDetail(new ResourceList(0,0,-50,0,0),new ResourceList(0,0,-1,0,0))), new EventOption("We must rebuild!", new ResourceDetail(new ResourceList(0,-100,0,0,0), new ResourceList())) }));
        availableEvents.Add(new Event("Noises In The Forest", "The hunters have reported strange noises from the forest and are becoming uneasy.", new EventOption[] { new EventOption("Send parties to investigate.", new ResourceDetail(new ResourceList(0,-30,0,0,0),new ResourceList())), new EventOption("It's probably nothing.", new ResourceDetail(new ResourceList(),new ResourceList(0,-1,-1,0,0))) }));

        //initialize Shop
        store = new Store();
        store.AddStock(new Item("Farm", "A farm capable of growing crops", new ResourceDetail(new ResourceList(0,-100,-10,0,0), new ResourceList(0,0,1,0,0))));

        //initialize timer
        timer = 0;
        hour = 6; //6am - 12am? (6 - 24)
        endOfDay = false;
        timerPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!endOfDay && !timerPaused)
        {
            timer++;

            //check if 3 seconds have passed
            if (timer % 180 == 0)
            {
                //increment hour
                hour++;

                //resource income
                resources.current = resources.current + resources.change;

                //cheaty event spawning
                if (hour == 9)
                {
                    SetEvent();
                    timerPaused = true;
                }

                if (hour == 15)
                {
                    SetEvent();
                    timerPaused = true;
                }

                if (hour == 24)
                {
                    EndOfDay();
                }
            }

            //update the UI
            UpdateUI();
        }
	}

    //updates the UI elements that need to be updated
    void UpdateUI()
    {
        //update timer
        GameObject.FindGameObjectWithTag("Time").GetComponent<Text>().text = hour + ":00";

        //update resource values
        GameObject.FindGameObjectWithTag("Resources").GetComponent<Text>().text = "Population: " + resources.current.population + "     Food: " + resources.current.food + "    Money: " + resources.current.money;
    }

    //ends the day
    void EndOfDay()
    {
        //set endOfDay to true
        endOfDay = true;

        //move end of day message to screen
        GameObject.FindGameObjectWithTag("EndOfDay").GetComponent<RectTransform>().position = new Vector3(320f, 260f, 0);
    }

    //get's a random event from the list, removes from available, adds to queue
    Event GetEvent()
    {
        //gets a random index
        int randomEvent = (int)(Random.value * availableEvents.Count);

        //save it to a temp holder
        Event returnEvent = availableEvents[randomEvent];

        //remove from available, add to queue
        availableEvents.RemoveAt(randomEvent);
        eventQueue.Add(activeEvent);

        return returnEvent;
    }

    void SetEvent()
    {
        //grab an event
        activeEvent = GetEvent();

        //update the event UI
        GameObject.FindGameObjectWithTag("Event").GetComponent<Text>().text = activeEvent.Title;
        GameObject.FindGameObjectWithTag("EventDescription").GetComponent<Text>().text = activeEvent.Description;

        //updates each choice CHANGE THIS LATER FOR X CHOICES INSTEAD OF 2 OPTIONS
        for (int i = 0; i < activeEvent.OptionCount; i++)
        {
            GameObject.Find("Choice" + i).gameObject.GetComponentInChildren<Text>().text = activeEvent.getOption(i).Text;
        }

        //move the event prefab into view
        GameObject.FindGameObjectWithTag("Event").GetComponent<RectTransform>().position = new Vector3(320f, 260f, 0);
    }

    public void ButtonClick(int index)
    {
        //reset position of event
        GameObject.FindGameObjectWithTag("Event").GetComponent<RectTransform>().position = new Vector3(740f, 260f, 0);

        //quick and dirty resource updating from event
        ResourceDetail resourceUpdate = activeEvent.getOption(index).ResourceEffects;
        this.resources = this.resources + resourceUpdate;

        //unpause
        timerPaused = false;
    }
}
