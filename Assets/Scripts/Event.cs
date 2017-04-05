using System.Collections.Generic;


class Event
{
    private string title;
    private string description;

    private List<EventOption> options;


    /// <summary>
    /// Creates an event
    /// </summary>
    /// <param name="title">Event title</param>
    /// <param name="description">Event Description</param>
    /// <param name="options">List of event options</param>
    public Event(string title, string description, List<EventOption> options)
    {
        this.title = title;
        this.description = description;
        this.options = options;

        if (OptionCount == 0)
        {
            addDefaultOption();
        }
    }

    /// <summary>
    /// Creates an event
    /// </summary>
    /// <param name="title">Event title</param>
    /// <param name="description">Event Description</param>
    /// <param name="options">any number of option objects</param>
    public Event(string title, string description, params EventOption[] options)
    {
        this.title = title;
        this.description = description;
        this.options = new List<EventOption>(options);

        if (OptionCount == 0)
        {
            addDefaultOption();
        }
    }

    /// <summary>
    /// Adds a default "ok" option if no options are given
    /// </summary>
    private void addDefaultOption()
    {
        options.Add(new EventOption("Ok", new ResourceDetail()));
    }


    /// <summary>
    /// Get title of event
    /// </summary>
    public string Title
    {
        get { return title; }
    }

    /// <summary>
    /// Get event description
    /// </summary>
    public string Description
    {
        get { return description; }
    }

    /// <summary>
    /// Get number of options in event
    /// </summary>
    public int OptionCount
    {
        get { return options.Count; }
    }


    /// <summary>
    /// Returns the event option at the given index, or null if out of range
    /// </summary>
    /// <param name="optionNumber">The option index to get</param>
    /// <returns>Event option requested, or null</returns>
    public EventOption getOption(int optionNumber)
    {
        if (optionNumber < 0 || optionNumber >= OptionCount)
        {
            return null;
        }
        return options[optionNumber];
    }


}

