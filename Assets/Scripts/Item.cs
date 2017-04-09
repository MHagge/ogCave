using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Item
{
    private string name;
    public string Name { get { return name; } set { name = value; } }
    private string description;
    public string Description { get { return description; } set { description = value; } }
    private ResourceDetail effect;
    public ResourceDetail Effect { get { return effect; } set { effect = value; } }

    public Item(string name, string description, ResourceDetail effect)
    {
        this.name = name;
        this.description = description;
        this.effect = effect;
    }
}

