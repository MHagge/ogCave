struct ResourceList
{
    public float population;
    public float money;
    public float food;
    public float panic;
    public float trust;

    public ResourceList(float pop, float money, float food, float panic, float trust)
    {
        this.population = pop;
        this.money = money;
        this.food = food;
        this.panic = panic;
        this.trust = trust;
    }
    public static ResourceList operator +(ResourceList lhs, ResourceList rhs)
    {
        ResourceList sum;
        sum.population = lhs.population + rhs.population;
        sum.money = lhs.money + rhs.money;
        sum.food = lhs.food + rhs.food;
        sum.panic = lhs.panic + rhs.panic;
        sum.trust = lhs.trust + rhs.trust;
        return sum;
    }

}

struct ResourceDetail
{
    public ResourceList current;
    public ResourceList change;

    public ResourceDetail(ResourceList curr, ResourceList change)
    {
        this.current = curr;
        this.change = change;
    }
    public static ResourceDetail operator +(ResourceDetail lhs, ResourceDetail rhs)
    {
        ResourceDetail sum;
        sum.current = lhs.current + rhs.current;
        sum.change = lhs.change + rhs.change;
        return sum;
    }
}


