using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 



public class MoveCommand : ICommand
{
    private float _moveSpeed;
    private int  _verticalDir, _horizontalDir;
    private Vector3 _previousPosition;

    public MoveCommand(float _speed, int p_verticalDir, int p_horizontalDir)
    {
        _moveSpeed = _speed;
        _verticalDir = p_verticalDir;
        _horizontalDir = p_horizontalDir;
    }

    public void Execute(PlayerController _player, out System.Func<bool> finished)
    {
        _previousPosition = _player.transform.position;

        var nextPosition = _previousPosition + _player.transform.forward * _verticalDir + _player.transform.right * _horizontalDir;
        _player.transform.DOMove(nextPosition, 0.2f);

        finished = () =>
        {
            return Vector3.Distance(_player.transform.position, nextPosition) <= 0.001f;
        };

    }

    public void Undo(PlayerController _player, out System.Func<bool> finished)
    {
        var nextPosition = _previousPosition;
        _player.transform.DOMove(nextPosition, 0.07f);

        finished = () =>
        {
            return Vector3.Distance(_player.transform.position, nextPosition) <= 0.01f;
        };
    }
}