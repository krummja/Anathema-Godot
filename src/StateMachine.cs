using Godot;
using System.Collections.Generic;

public enum States
{
    BOOT,
    SPLASH,
    LOAD,
    MENU,
    RUN,
    PAUSE,
    EXIT,
}

public partial class State<T> : Node
{
    public delegate void DelegateNoArg();

    public DelegateNoArg OnEnter;
    public DelegateNoArg OnExit;

    protected Main main;

    public T ID { get; private set; }

    public State()
    { }

    public State(Main main, T id)
    {
        this.main = main;
        this.ID = id;
    }

    public virtual void Enter()
    { }

    public virtual void Exit()
    { }
}

public class StateMachine<T>
{
    private Main main;
    private Dictionary<T, State<T>> states;

    public State<T> CurrentState { get; private set; }

    public StateMachine(Main main)
    {
        this.states = new Dictionary<T, State<T>>();
        this.main = main;
    }

    public void Add(State<T> state)
    {
        this.states.Add(state.ID, state);
    }

    public State<T> Get(T stateID)
    {
        if (this.states.ContainsKey(stateID))
            return this.states[stateID];
        return null;
    }

    public void TransitionTo(T stateID)
    {
        State<T> state = states[stateID];

        if (this.CurrentState == state)
            return;
        if (this.CurrentState != null)
        {
            this.CurrentState.OnExit?.Invoke();
            this.CurrentState.Exit();
            this.main.State.RemoveChild(this.CurrentState);
        }

        this.CurrentState = state;

        if (this.CurrentState != null)
        {
            this.main.State.AddChild(this.CurrentState);
            this.CurrentState.Enter();
            this.CurrentState.OnEnter?.Invoke();
        }
    }
}