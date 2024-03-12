internal class EventGenerator
{
    private static readonly Random random = new Random();

    public static LevelEvent GenerateRandomEvent()
    {
        Array events = Enum.GetValues(typeof(LevelEvent));

        if (events == null || events.Length == 0)
        {
            Console.WriteLine("No events available.");
            return LevelEvent.DefaultEvent;
        }

        LevelEvent randomEvent = (LevelEvent)events.GetValue(random.Next(events.Length));

        return randomEvent;
    }
}
