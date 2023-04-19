using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute(PlayerController _player, out System.Func<bool> finished);

    void Undo(PlayerController _player, out System.Func<bool> finished);
}