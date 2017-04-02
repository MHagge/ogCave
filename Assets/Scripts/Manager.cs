using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    //resources
    Dictionary<string, float> resources = new Dictionary<string, float>();

    //List of available Events
    List<Event> availableEvents;

    //Event Queue
    List<Event> eventQueue;

    //timer
    private int hour;
    private int timer;
    private bool endOfDay;

	// Use this for initialization
	void Start () {
        // initial values for resources
        resources.Add("pop", 0);
        resources.Add("money", 0);
        resources.Add("food", 0);
        resources.Add("panic", 0);
        resources.Add("trust", 0);
        resources.Add("cPop", 10.0f);
        resources.Add("cMoney", 10.0f);
        resources.Add("cFood", 10.0f);
        resources.Add("cPanic", 0.0f);
        resources.Add("cTrust", 0.0f);

        //initialize lists
        availableEvents = new List<Event>();
        eventQueue = new List<Event>();

        //populate initial events
        availableEvents.Add(new Event());

        //initialize timer
        timer = 0;
        hour = 6; //6am - 12am? (6 - 24)
        endOfDay = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!endOfDay)
        {
            timer++;

            //check if 3 seconds have passed
            if (timer % 3000 == 0)
            {
                //increment hour
                hour++;

                //resource income
                resources["pop"] += resources["cPop"];
                resources["money"] += resources["cMoney"];
                resources["food"] += resources["cFood"];

                //cheaty event spawning
                if (hour == 9)
                {

                }

                if (hour == 15)
                {

                }
            }
        }
	}
}
