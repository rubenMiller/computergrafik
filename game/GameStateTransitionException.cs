using System;

[Serializable]
public class GameStateTransitionException : Exception
{
    public GameStateTransitionException() : base() { }
    public GameStateTransitionException(string message) : base("An unallowed game Transition was made, from: " + message) { }
    public GameStateTransitionException(string message, Exception inner) : base(message, inner) { }
}