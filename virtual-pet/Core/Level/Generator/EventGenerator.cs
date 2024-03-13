using virtual_pet.Core.Level.Enums;

internal class EventGenerator
{
    private static readonly Random random = new Random();

    public static LevelEvent GenerateRandomEvent()
    {
        Array events = Enum.GetValues(typeof(LevelEvent));

        if (events == null || events.Length == 0)
        {
            Environment.Exit(0);
        }

        LevelEvent randomEvent = (LevelEvent)events.GetValue(random.Next(events.Length));

        return randomEvent;
    }
}
