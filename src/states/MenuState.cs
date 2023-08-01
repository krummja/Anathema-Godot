using Godot;

public partial class MenuState : State<States>
{
    public MenuState(Main main) : base(main, States.MENU)
    {
        this.Name = "__Menu";
    }

    public override void Enter()
    {
        Director director = this.main.Director;
        MainMenu menu = director.LoadImmediate<MainMenu>(
            "main_menu/",
            "main_menu",
            MountPoint.OVERLAY
        );

        menu.ButtonPressed += OnMainMenuEvent;
        this.main.Overlay.Push(menu);
    }

    public override void Exit()
    {
        this.main.Overlay.Pop<Node>();
    }

    private void OnMainMenuEvent(MainMenuEvent @event)
    {
        switch (@event)
        {
            case MainMenuEvent.NEW_GAME:
                {
                    Director director = this.main.Director;
                    NewGameMenu menu = director.LoadImmediate<NewGameMenu>(
                    "main_menu/new_game_menu",
                    "new_game_menu",
                    MountPoint.OVERLAY
                );

                    menu.StartGamePressed += OnNewGameMenuEvent;
                    menu.ButtonPressed += OnStackableMenuEvent;

                    this.main.Overlay.Push(menu);
                    break;
                }

            case MainMenuEvent.LOAD_GAME:
                {
                    Director director = this.main.Director;
                    LoadGameMenu menu = director.LoadImmediate<LoadGameMenu>(
                    "main_menu/load_game_menu",
                    "load_game_menu",
                    MountPoint.OVERLAY
                );

                    menu.ButtonPressed += OnStackableMenuEvent;

                    this.main.Overlay.Push(menu);
                    break;
                }

            case MainMenuEvent.EXIT:
                {
                    this.main.StateMachine.TransitionTo(States.EXIT);
                    break;
                }
        }
    }

    private void OnNewGameMenuEvent()
    {
        NewGameMenu newGameMenu = this.main.Overlay.Pop<NewGameMenu>();
        newGameMenu.StartGamePressed -= OnNewGameMenuEvent;
        newGameMenu.ButtonPressed -= OnStackableMenuEvent;

        this.main.StateMachine.TransitionTo(States.LOAD);
    }

    private void OnStackableMenuEvent(StackableMenuEvent @event)
    {
        switch (@event)
        {
            case StackableMenuEvent.BACK:
                {
                    Node screen = this.main.Overlay.Pop<Node>();

                    if (screen is NewGameMenu newGameMenu)
                        newGameMenu.ButtonPressed -= OnStackableMenuEvent;
                    if (screen is LoadGameMenu loadGameMenu)
                        loadGameMenu.ButtonPressed -= OnStackableMenuEvent;

                    screen.QueueFree();
                    break;
                }
        }
    }
}