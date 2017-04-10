using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
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
    private static System.Timers.Timer timer;
    private bool endOfDay;
    private bool eventActive;
    private bool endOfGame;
    private bool storeIsOpen;
    private int moneyStoreCount;
    private int foodStoreCount;

    public GameObject choice1;
    public GameObject choice2;

    //UI canvas reference

    // Use this for initialization
    void Start()
    {
        // initial values for resources 
        resources = new ResourceDetail(new ResourceList(100, 0, 0, 0, 0), new ResourceList(0, 10, 10, 0, 0));

        //initialize lists
        availableEvents = new List<Event>();
        eventQueue = new List<Event>();
        activeEvent = new Event("", "", new EventOption[] { });


        //populate initial events

        availableEvents.Add(new Event("Job Opening", "\"Didn’t you hear me? Mayor bumblefrump is dead!\" says Hunter once again, waking you from your shock. \"He was found near the cave outside of town. His head was bloodied and scratched and his face was contorted like he saw death himself! You know you’re the second in command so come with me to the town hall\"", new EventOption[] { new EventOption("Follow Hunter to the town hall to assume your mayoral duties.", new ResourceDetail(new ResourceList(0, 0, 0, 0, 0), new ResourceList(0, 0, 0, 0, 0))) }));

        availableEvents.Add(new Event("Office food", "\"Well, here we are. As soon as you finish signing this document, you’ll be taking charge of the town until we manage to figure out what exactly happened to old bumblefrump. So, first up, Just initial here to take official control of the town's food supply. And mind you, this is for the whole town, if you take it all for yourself or give it away, we’ll starve.. And nobody likes that.\"", new EventOption[] { new EventOption("Initial line one, under Article 2 Section 4 ", new ResourceDetail(new ResourceList(0, 0, 0, 0, 0), new ResourceList(0, 0, 0, 0, 0))) }));

        availableEvents.Add(new Event("Money is Power", "\"Alright, next up, You’ll initial here to take over management of all of the town’s monetary assets. You can use this money to help the town in different way by making purchases from the store. Careful not to go too crazy spending that money. You never know when you’re gonna need it in a jiffy.\"", new EventOption[] { new EventOption("Initial line two under Article 3 Section 1", new ResourceDetail(new ResourceList(0, 0, 0, 0, 0), new ResourceList(0, 0, 0, 0, 0))) }));

        availableEvents.Add(new Event("One signature to rule them all", "\"Excellent. Now you just need to sign the bottom, then you’ll officially be in charge of the population. If you need to, you can tell us to work by putting the population towards items in the store. Careful though, past a certain point you’ll start making people upset. People get scared and upset pretty easy around here, so you best try to keep everyone happy, else the town may topple itself over\"", new EventOption[] { new EventOption("Sign line three under Article 5 Section 2 Subsection 4.a", new ResourceDetail(new ResourceList(0, 0, 0, 0, 0), new ResourceList(0, 0, 0, 0, 0))) }));

        availableEvents.Add(new Event("It's us or them", "\"You signed that whole thing without looking at it.. Well, your eagerness aside, if you ever want to look back at this or any other event, just check your event logs. Now, before I leave you too it, I have a real decision for you. Bumblefrump left in his will 200 money to disperse amongst the townspeople. This’ll definitely make everyone happier, but we may end up needing that money. What do we do?", new EventOption[] {new EventOption("Take the Money, we may need it",new ResourceDetail(new ResourceList(0,200,0,0,0),new ResourceList(0,0,0,0,0))),
new EventOption("Give it to the town, it’ll lighten the mood",new ResourceDetail(new ResourceList(0,0,0,-.1f,.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("Rich pants, empty pockets", "Mr. Money Richpants claims that his house was broken into and some of his valuables were stolen.", new EventOption[] {new EventOption("Give him money equal to the value of what he lost.",new ResourceDetail(new ResourceList(0,-50,0,0,0),new ResourceList(0,0,0,0,0))),
new EventOption("Send people to investigate.  ",new ResourceDetail(new ResourceList(-5,0,0,.1f,.1f),new ResourceList(0,0,0,0,0))),
new EventOption("Tell him you’re sorry but you have bigger problems at the moment then petty thievery. ",new ResourceDetail(new ResourceList(0,0,0,0,-.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("Pitty party", "Mr. Money Richpant’s thief was never caught and a few more villagers are coming in claiming to have been burgled. ", new EventOption[] {new EventOption("Give the villagers money equal to the value of what they lost.",new ResourceDetail(new ResourceList(0,-70,-30,0,0),new ResourceList(0,0,0,0,0))),
new EventOption("Send people to investigate.",new ResourceDetail(new ResourceList(-10,0,0,.1f,.1f),new ResourceList(0,0,0,0,0))),
new EventOption("Tell Them that you need proof that they have been robbed before taking further action.",new ResourceDetail(new ResourceList(0,0,0,0,.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("Raiders of the stolen items", "\"Burglars\" are now \"raiding\" the village. People are panicking and demanding compensation. They think the burglars’ base is in the cave and that they are responsible for the mayor’s death.", new EventOption[] {new EventOption("Compensate all the villagers who claim being burgled.",new ResourceDetail(new ResourceList(0,-100,-50,0,0),new ResourceList(0,0,0,0,0))),
new EventOption("Set up traps for the burglars outside of the cave.",new ResourceDetail(new ResourceList(-5,0,0,.1f,0),new ResourceList(0,0,0,0,0))),
new EventOption("Do a deep investigation of each robbery.",new ResourceDetail(new ResourceList(-10,0,0,-.1f,.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("Geberic bank robbery", "There has been a huge bank heist.", new EventOption[] {new EventOption("Ignore it. It’s just the villagers trying to get money out of you.",new ResourceDetail(new ResourceList(0,-100,0,0,-.1f),new ResourceList(0,0,0,0,0))),
new EventOption("Investigate.",new ResourceDetail(new ResourceList(-5,0,0,.1f,0),new ResourceList(0,0,0,0,0))),
new EventOption("Go to the cave to try to catch the thief.",new ResourceDetail(new ResourceList(0,0,0,0,.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("The farmer who cried wolf", "A new threat has popped up in town. The town farmer has come to express his concern. \"Mr. Mayor, I’m just going to say it: We have a wolf problem. Now, I haven’t actually seen em, but we just lost three sheep today (-10 food), and I found some of their fur and some very large scratch marks just on the edge of the cave. We need to warn the people sir, someone among us or in the cave may be one of them.\"", new EventOption[] {new EventOption("Warn the town, everyone will be more paranoid, but they may be safer",new ResourceDetail(new ResourceList(0,0,0,.1f,.1f),new ResourceList(0,0,0,0,0))),
new EventOption("Tell no one, pay the farmer for his sheep and his silence, you’ll handle it",new ResourceDetail(new ResourceList(0,-150,0,0,0),new ResourceList(0,0,0,0,0))),
new EventOption("Set a trap for the wolves with some more sheep",new ResourceDetail(new ResourceList(0,0,-10,0,0),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("howling mad, or mad howling?", "\"I’m telling you, I’m not crazy\" Shouts a bloodied being pushed through the door by two others. \"Mayor, listen to me. I didn’t kill no sheep, I was attacked meself. I was in the field and saw a man walking among the sheep. I went to tell him off when.. He changed. He.. he turned into a huge wolf and ate one of the sheep. He came after me when i screamed.. But I got away. Sir, we Have a werewolf problem.\"", new EventOption[] {new EventOption("This man is a liar, he needs to die.",new ResourceDetail(new ResourceList(-1,0,0,-.1f,-.1f),new ResourceList(0,0,0,0,0))),
new EventOption("This man needs help, give him money to get back on his feet",new ResourceDetail(new ResourceList(0,-50,0,-.1f,-.1f),new ResourceList(0,0,0,0,0))),
new EventOption("The town needs to know, be on the lookout for werewolves",new ResourceDetail(new ResourceList(0,0,0,-.1f,.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("Team Jacob", "\"Sir, it’s official: There are indeed werewolves in the town. I’m not sure where they came from, or what they want, but they’re here. I know because I saw them, camped just outside of town. They looked like normal people but they had our sheep, dead and clawed up, cooking over a fire. At the least we know they’re not in the cave, but we have a problem nonetheless\". What the hunter said is concerning..", new EventOption[] {new EventOption("Send men to take them out, it's our only chance",new ResourceDetail(new ResourceList(-4,0,0,0,0),new ResourceList(0,0,0,0,0))),
new EventOption("Increase protection around the sheep, that’s all we can do",new ResourceDetail(new ResourceList(-1,0,0,.1f,0),new ResourceList(0,0,0,0,0))),
new EventOption("Ignore them, maybe they’ll go away",new ResourceDetail(new ResourceList(0,0,-1,.1f,-.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("saved by the Rick", "Some old friends of yours appear out of no-where, just like always. You ask if they can help with your problem. \"There are werewolves and everyone is piiiiiissssed. Ooooh jeez, rick they seem pretty mad. What do we do?\"", new EventOption[] {new EventOption("Shut up *bLUUuurp* Morty, There’s no such thing, we just tell everyone they’re idiots",new ResourceDetail(new ResourceList(0,0,0,0,-.2f),new ResourceList(0,0,0,0,0))),
new EventOption("WEREWOLVES! Aww shit, Morty, shit. They followed me morty. We gotta pay a hunter to kill em all, Morty, the entire race needs to be destroyed Morty, you hear me? *BUrrp* DESTROYED.",new ResourceDetail(new ResourceList(0,-250,0,0,0),new ResourceList(0,0,0,0,0))),
new EventOption("*beerRRRP* Look, Morty, if I know Werewolves, and I don’t, all they need is a sacrifice. Then we’ll be *urp* fine Morty. Just give em a person. -Rick no-. Fine, you wuss, just *BRRRPP* give em a few goats and I’m sure that’ll do.",new ResourceDetail(new ResourceList(0,0,-5,0,0),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("crazy about ghosts", "A raving lunatic woman runs wildly into the the town square. Most of what she says is incomprehensible but a few word can be made out like \"ghosts\" and \"the cave\"...  a crowd is forming around her.", new EventOption[] {new EventOption("The woman’s obviously sick. Send her to the doctor.",new ResourceDetail(new ResourceList(0,0,0,.1f,.1f),new ResourceList(0,0,0,0,0))),
new EventOption("The woman’s nuts and causing a ruckus. Lock her up for the night.",new ResourceDetail(new ResourceList(0,0,0,.1f,-.1f),new ResourceList(0,0,0,0,0))),
new EventOption("Do nothing and see how this plays out. ",new ResourceDetail(new ResourceList(0,0,0,.1f,0),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("Terrors in the night", "There lots of talk about the crazy woman in town.  A few people say they saw her come from the direction of the cave.  One man says he heard a voice during the night accompanied by a ghostly apparition. ", new EventOption[] {new EventOption("Hold an event to assure people the ghosts are without a doubt not real",new ResourceDetail(new ResourceList(0,0,0,-.1f,0),new ResourceList(0,0,0,0,0))),
new EventOption("Hand out ghost wards and tell people to sprinkle salt around their doors and windows",new ResourceDetail(new ResourceList(0,0,0,.1f,.1f),new ResourceList(0,0,0,0,0)))}));

        availableEvents.Add(new Event("Too spooky for them", "The ghost stories are causing panic. People are hearing the voices of their dead loved ones. Some even claim to hear the voice of the dead mayor.  They say the the dead mayor is telling them that he was attacked by malicious spirits in the cave.  ", new EventOption[] {new EventOption("Have the village priest to perform from a exorcism?",new ResourceDetail(new ResourceList(0,0,0,-.1f,-.1f),new ResourceList(0,0,0,0,0))),
new EventOption("Sacrifice a small herd to cattle to the spirits and ask them to please leave.",new ResourceDetail(new ResourceList(0,0,-20,-.4f,-.2f),new ResourceList(0,0,0,0,0))),
new EventOption("Try again to assure the villagers that ghosts and spirits are not real.",new ResourceDetail(new ResourceList(0,0,0,0,.2f),new ResourceList(0,0,0,0,0)))}));





        //availableEvents.Add(new Event("Farm House Burns Down", "A fire burned down the farm house!", new EventOption[] { new EventOption("Oh no!", new ResourceDetail(new ResourceList(0, 0, -50, 0, 0), new ResourceList(0, 0, -1, 0, 0))), new EventOption("We must rebuild!", new ResourceDetail(new ResourceList(0, -100, 0, 0, 0), new ResourceList())) }));
       // availableEvents.Add(new Event("Noises In The Forest", "The hunters have reported strange noises from the forest and are becoming uneasy.", new EventOption[] { new EventOption("Send parties to investigate.", new ResourceDetail(new ResourceList(0, -30, 0, 0, 0), new ResourceList())), new EventOption("It's probably nothing.", new ResourceDetail(new ResourceList(), new ResourceList(0, -1, -1, 0, 0))) }));

        //initialize Shop
        store = new Store();
        store.AddStock(new Item("Farm", "A farm capable of growing crops", new ResourceDetail(new ResourceList(0, -100, -10, 0, 0), new ResourceList(0, 0, 1, 0, 0))));

        //initialize timer
        timer = new System.Timers.Timer(1000);
        timer.AutoReset = true;
        timer.Start();
        timer.Elapsed += TimerEvents;
        hour = 6; //6am - 12am? (6 - 24)
        endOfDay = false;
        eventActive = false;
        storeIsOpen = false;
        endOfGame = false;

        foodStoreCount = moneyStoreCount = (int)resources.current.population / 2;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

        //cheaty event spawning
        if (hour == 9 && !eventActive)
        {
            eventActive = true;
            SetEvent();
            timer.Stop();
        }

        if (hour == 15 && !eventActive)
        {
            eventActive = true;
            SetEvent();
            timer.Stop();
        }

        if (hour == 24)
        {
            EndOfDay();
        }

        if (resources.current.population <= 0 || resources.current.panic >= 100 || availableEvents.Count == 0)
        {
            GameEnd();
        }
    }

    void TimerEvents(object source, ElapsedEventArgs e)
    {
        if (!endOfDay && !storeIsOpen)
        {
            hour++;
            //resource income
            resources.current = resources.current + resources.change;

            if (resources.current.food < 0 || resources.current.money < 0)
            {
                resources.current.panic += 1;
            }

        }

        if (endOfDay)
        {
            endOfDay = false;

            GameObject.FindGameObjectWithTag("EndOfDay").GetComponent<RectTransform>().position = new Vector3(512f, -20, 0);
        }
    }

    //updates the UI elements that need to be updated
    void UpdateUI()
    {
        //update timer
        GameObject.FindGameObjectWithTag("Time").GetComponent<Text>().text = hour + ":00";

        //update panic meter
        //Debug.Log(GameObject.FindGameObjectWithTag("PanicBar").GetComponent<RectTransform>().offsetMax.x);
        GameObject.FindGameObjectWithTag("PanicBar").GetComponent<RectTransform>().offsetMax = new Vector2(-200 + (resources.current.panic * 2), GameObject.FindGameObjectWithTag("PanicBar").GetComponent<RectTransform>().offsetMax.y);

        //update resource values
        GameObject.FindGameObjectWithTag("Resources").GetComponent<Text>().text = "Population: " + resources.current.population + "     Food: " + resources.current.food + "    Money: " + resources.current.money;
    }

    //ends the day
    void EndOfDay()
    {
        //set endOfDay to true
        endOfDay = true;

        // set time to 0
        hour = 0;

        //move end of day message to screen
        GameObject.FindGameObjectWithTag("EndOfDay").GetComponent<RectTransform>().position = new Vector3(512f, 450f, 0);
    }

    //get's a random event from the list, removes from available, adds to queue
    Event GetEvent()
    {
        //gets a random index
        //int randomEvent = (int)(Mathf.Floor(Random.value * availableEvents.Count));

        //save it to a temp holder
        Event returnEvent = availableEvents[0];

        //remove from available, add to queue
        availableEvents.RemoveAt(0);
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

        Debug.Log(activeEvent.OptionCount);
        if (activeEvent.OptionCount < 3)
        {
            choice2.gameObject.SetActive(false);

            if (activeEvent.OptionCount == 1)
            {
                choice1.gameObject.SetActive(false);
            }
        }

        //move the event prefab into view
        GameObject.FindGameObjectWithTag("Event").GetComponent<RectTransform>().position = new Vector3(512f, 550f, 0);
    }

    public void ButtonClick(int index)
    {
        //reset position of event
        GameObject.FindGameObjectWithTag("Event").GetComponent<RectTransform>().position = new Vector3(512f, -20, 0);
        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);

        //quick and dirty resource updating from event
        ResourceDetail resourceUpdate = activeEvent.getOption(index).ResourceEffects;
        this.resources = this.resources + resourceUpdate;

        //unpause
        timer.Start();

        // hack to get by update calling multiple events per hour
        hour++;

        // restart loop
        eventActive = false;
    }

    public void StoreClick()
    {
        if (storeIsOpen)
        {
            storeIsOpen = false;
            GameObject.FindGameObjectWithTag("StoreWrapper").GetComponent<RectTransform>().position = new Vector3(512f, -20f, 0);
            timer.Start();
        }
        else
        {
            storeIsOpen = true;
            GameObject.FindGameObjectWithTag("StoreWrapper").GetComponent<RectTransform>().position = new Vector3(512f, 384f, 0);
            timer.Stop();
        }
    }

    public void StoreTaskAdd(int moneyOrFood)
    {
        if (moneyOrFood == 0) //money
        {
            float moneyTask = float.Parse(GameObject.FindGameObjectWithTag("MoneyStoreCount").GetComponent<Text>().text);
            GameObject.FindGameObjectWithTag("MoneyStoreCount").GetComponent<Text>().text = (moneyTask + 1).ToString();
            resources.change.money = moneyTask / 5.0f;
        }
        else
        {
            float foodTask = float.Parse(GameObject.FindGameObjectWithTag("foodStoreCount").GetComponent<Text>().text);
            GameObject.FindGameObjectWithTag("FoodStoreCount").GetComponent<Text>().text = (foodTask + 1).ToString();
            resources.change.food = foodTask / 5.0f;
        }
    }

    public void StoreTaskSub(int moneyOrFood)
    {
        if (moneyOrFood == 0) //money
        {
            float moneyTask = float.Parse(GameObject.FindGameObjectWithTag("MoneyStoreCount").GetComponent<Text>().text);
            GameObject.FindGameObjectWithTag("MoneyStoreCount").GetComponent<Text>().text = (moneyTask - 1).ToString();
            resources.change.money = moneyTask / 5.0f;
        }
        else
        {
            float foodTask = float.Parse(GameObject.FindGameObjectWithTag("foodStoreCount").GetComponent<Text>().text);
            GameObject.FindGameObjectWithTag("FoodStoreCount").GetComponent<Text>().text = (foodTask - 1).ToString();
            resources.change.food = foodTask / 5.0f;
        }
    }

    public void FoodBuySell(bool buy)
    {
        if (buy)
        {
            resources.current.food += 1;
            resources.current.money -= 1;
        }
        else
        {
            resources.current.food -= 1;
            resources.current.money += 1;
        }
    }

    public void GameEnd()
    {
        endOfGame = true;

        if (availableEvents.Count == 0) //you win
        {
            GameObject.FindGameObjectWithTag("YouWin").GetComponent<RectTransform>().position = new Vector3(512f, 450f, 0);
        }
        else //you lose
        {
            GameObject.FindGameObjectWithTag("YouLose").GetComponent<RectTransform>().position = new Vector3(512f, 450f, 0);
        }
    }
}
