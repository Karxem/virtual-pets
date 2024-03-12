namespace virtual_pet.Core.GameEngine.Input
{
    public interface IInputListener
    {

        public bool IsActive { get; set; }

        public void KeyPressed(ConsoleKeyInfo info);
        public OptionStrip? GetOptionStrip() => null;
        public bool RequieresPassAll() => false;

        ///**
        // * <summary>requires return type to make it optional</summary>
        // * 
        // *  <returns>always null</returns>
        // */
        //public object SetActive() => null;

        ///**
        // * <summary>requires return type to make it optional</summary>
        // * 
        // * <returns>always null</returns>
        // */
        //public object SetInactive() => null;
    }
}
