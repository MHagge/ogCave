using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Item
{
    private string name;
    public string Name { get { return name; } }
    private string description;
    public string Description { get; set; }
    private ResourceDetail effect;
    public ResourceDetail Effect { get; set; }

    public Item(string name, string description, ResourceDetail effect)
    {
        this.name = name;
        this.description = description;
        this.effect = effect;
    }
}

