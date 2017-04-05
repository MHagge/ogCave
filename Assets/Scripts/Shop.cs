using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store
{
    private Dictionary<string,Item> stock;

    public Store()
    {
        this.stock = new Dictionary<string, Item>();
    }
    public Store(List<Item> stock)
	{
        this.stock = new Dictionary<string, Item>();
        foreach (Item i in stock)
            AddStock(i);
	}

    public void AddStock(Item item)
    {
        stock.Add(item.Name, item);
    }

    public ResourceDetail Purchase(string name)
    {
        Item purchased = stock[name];
        if (purchased != null)
        {
            stock.Remove(name);
            return purchased.Effect;
        }
        return new ResourceDetail();
    }
}
