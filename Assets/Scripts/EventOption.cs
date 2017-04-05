
class EventOption
{
    private string text;
    public string Text
    {
        get { return text; }
    }
    private ResourceDetail resourceEffects;
    public ResourceDetail ResourceEffects
    {
        get { return resourceEffects; }
    }

    /// <summary>
    /// Creates an event option
    /// </summary>
    /// <param name="text">the option text</param>
    /// <param name="resourceEffects">the option resource effects</param>
    public EventOption(string text, ResourceDetail resourceEffects)
    {
        this.text = text;
        this.resourceEffects = resourceEffects;
    }

}
