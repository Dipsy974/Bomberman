using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public PlayerController Player;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode moveUpKey;
    public KeyCode moveDownKey;
    public KeyCode bombKey;
    public KeyCode rewindKey;

    private List<ICommand> _commands;
    [SerializeField] private int nbMoveBackwards; 

    private void Awake()
    {
        
    }

    private void Start()
    {
        _commands = new List<ICommand>();
    }


    //PARTIE 3
    void Update()
    {
        if (Input.GetKeyDown(moveUpKey))
        {
            StartCoroutine(AddMoveCommand(1, 0, moveUpKey));
        }

        if (Input.GetKeyDown(moveLeftKey))
        {
            StartCoroutine(AddMoveCommand(0, -1, moveLeftKey));
        }

        if (Input.GetKeyDown(moveDownKey))
        {
            StartCoroutine(AddMoveCommand(-1, 0, moveDownKey));
        }


        if (Input.GetKeyDown(moveRightKey))
        {
            StartCoroutine(AddMoveCommand(0, 1, moveRightKey));
        }

        if (Input.GetKeyDown(rewindKey))
        {
            if (Player.GetCanRewind())
            {
                StartCoroutine(UndoAllCommands());
                Player.SetCanRewind(false); 
            }
            
        }

        if (Input.GetKeyDown(bombKey) && Player.bombAvailable)
        {
            Player.LaunchBomb(); 
        }
    }

    //private void AddMoveCommand(int _verticalDir, int _horizontalDir)
    //{
    //    var command = new MoveCommand(Player.moveSpeed, _verticalDir, _horizontalDir);
    //    AddCommandToList(command);
    //}


    private IEnumerator AddMoveCommand(int _verticalDir, int _horizontalDir, KeyCode key)
    {
        while (Input.GetKey(key))
        {
            Vector3 direction = new Vector3(_horizontalDir, 0, _verticalDir);
            Ray theRay = new Ray(Player.transform.position - new Vector3(0, 0.3f,0), Player.transform.TransformDirection(direction * 1f));
            Debug.DrawRay(Player.transform.position - new Vector3(0, 0.3f, 0), Player.transform.TransformDirection(direction * 1f));

            if(!Physics.Raycast(theRay, out RaycastHit hit, 1f))
            {
                var command = new MoveCommand(Player.moveSpeed, _verticalDir, _horizontalDir);
                if(_commands.Count > nbMoveBackwards)
                {
                    _commands.RemoveAt(0); 
                }
                _commands.Add(command);
                command.Execute(Player, out var finished);
                yield return new WaitUntil(finished);
            }
            yield return new WaitForEndOfFrame(); 
        }

    }

    //private IEnumerator AddCommandToList(ICommand command)
    //{
    //    _commands.Add(command);
    //    command.Execute(Player, out var finished);
    //    yield return new WaitUntil(finished);
    //}


    //private void AddCommandToList(ICommand command)
    //{
    //    _commands.Add(command);
    //    command.Execute(Player, out var finished);
    //}

    private void Undo()
    {
        if (_commands.Count == 0) { return; }
        var commandsLastIndex = _commands.Count - 1;
        _commands[commandsLastIndex].Undo(Player, out var finished);
        _commands.RemoveAt(commandsLastIndex);
    }

    private void ExecuteAll()
    {
        StartCoroutine(ExecuteAllCommands());
    }

    private IEnumerator ExecuteAllCommands()
    {
        foreach (var command in _commands)
        {
            command.Execute(Player, out var finished);
            yield return new WaitUntil(finished);
        }
    }

    private IEnumerator UndoAllCommands()
    {
        for (int i = _commands.Count - 1; i >= 0; i--)
        {
            _commands[i].Undo(Player, out var finished);
            yield return new WaitUntil(finished);
        }
        _commands.Clear(); 
    }


    //private void SwapControls()
    //{
    //    eKeyCommand = new AttackCommand();
    //    aKeyCommand = new JumpCommand();
    //}

}


