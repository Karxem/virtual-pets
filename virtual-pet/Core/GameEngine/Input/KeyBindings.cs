namespace virtual_pet.Core.GameEngine.Input
{
    public class KeyBindings
    {
        public static class MenuBindings
        {
            public static KeyBindingEntry MoveUp { get; } = new KeyBindingEntry("Move Up", new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false), "▲");
            public static KeyBindingEntry MoveDown { get; } = new KeyBindingEntry("Move Down", new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false), "▼");
            public static KeyBindingEntry Select { get; } = new KeyBindingEntry("Select", new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false), "↲");
            public static KeyBindingEntry Return { get; } = new KeyBindingEntry("Return", new ConsoleKeyInfo(' ', ConsoleKey.Escape, false, false, false), "Esc");
        }

    }
    public class KeyBindingEntry
    {
        public string Name { get; set; }
        public ConsoleKeyInfo? Binding { get; set; }
        public string? BindingText { get; set; }
        public ConsoleKeyInfo? SecondaryBinding { get; set; }
        public string? SecondaryBindingText { get; set; }
        public KeyBindingEntry(string name, ConsoleKeyInfo binding, string bindingText, ConsoleKeyInfo? secondaryBinding = null, string? secondaryBindingText = null)
        {
            Name = name;
            Binding = binding;
            BindingText = bindingText;
            SecondaryBinding = secondaryBinding;
            SecondaryBindingText = secondaryBindingText;
        }
    }
}
