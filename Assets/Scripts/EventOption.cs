
class EventOption
{
    private string text;
    public string Text
    {
        get { return text; }
    }
    private float[] resourceEffects;
    public float[] ResourceEffects
    {
        get { return resourceEffects; }
    }

    /// <summary>
    /// Creates an event option
    /// </summary>
    /// <param name="text">the option text</param>
    /// <param name="resourceEffects">the option resource effects</param>
    public EventOption(string text, float[] resourceEffects)
    {
        this.text = text;
        this.resourceEffects = resourceEffects;
    }

}
