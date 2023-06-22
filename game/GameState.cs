public class GameState
{
    public enum STATE
    {
        STATE_START,
        STATE_PLAYING,
        STATE_WAVEOVER,
        STATE_UPGRADEMENU,
        STATE_DEAD,
        STATE_WON
    }
    public void transitionToState(STATE nextState)
    {
        if (CurrentState == nextState)
        {
            return;
        }
        if (nextState == STATE.STATE_START)
        {
            throw new GameStateTransitionException("any STATE to STATE_START.");
        }
        switch (CurrentState)
        {
            case STATE.STATE_START:
                {
                    if (nextState != STATE.STATE_PLAYING)
                    {
                        throw new GameStateTransitionException("STATE_START not to STATE_PLAYING.");
                    }
                    CurrentState = nextState;
                    break;
                }
            case STATE.STATE_PLAYING:
                {
                    CurrentState = nextState;
                    break;
                }
            case STATE.STATE_WAVEOVER:
                {
                    if (nextState == STATE.STATE_UPGRADEMENU || nextState == STATE.STATE_WON)
                    {
                        CurrentState = nextState;
                        break;
                    }
                    throw new GameStateTransitionException("STATE_WAVEOVER to a state that is not STATE_UPGRADEMENU.");
                }
            case STATE.STATE_UPGRADEMENU:
                {
                    if (nextState != STATE.STATE_PLAYING)
                    {
                        throw new GameStateTransitionException("STATE_UPGRADEMENU to a state that is not STATE_PLAYING.");
                    }
                    CurrentState = nextState;
                    break;
                }
            case STATE.STATE_DEAD:
                {
                    if (nextState != STATE.STATE_PLAYING)
                    {
                        throw new GameStateTransitionException("STATE_DEAD to a state that is not STATE_PLAYING.");
                    }
                    CurrentState = nextState;
                    break;
                }
            case STATE.STATE_WON:
                {
                    if (nextState != STATE.STATE_PLAYING)
                    {
                        throw new GameStateTransitionException("STATE_WON to a state that is not STATE_PLAYING.");
                    }
                    CurrentState = nextState;
                    break;
                }
        }


    }
    public STATE CurrentState { get; private set; }

    public GameState(STATE initalState)
    {
        CurrentState = initalState;
    }
}