public class GameState
{
    public enum STATE{
        STATE_START,
        STATE_PLAYING,
        STATE_WAVEOVER,
        STATE_DEAD
    }
    public STATE State;
    public GameState(STATE initalState)
    {
        State = initalState;
    }
}