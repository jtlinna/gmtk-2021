public static class InputManager
{
    private static ControlScheme _controlScheme;
    public static ControlScheme ControlScheme
    {
        get
        {
            if(_controlScheme == null)
            {
                _controlScheme = new ControlScheme();
                _controlScheme.Enable();
            }

            return _controlScheme;
        }
    }
}
