using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _backWall;

    [SerializeField]
    private GameObject _unvisitedBlock;

    public bool IsVisited { get; private set; }

    public void Visit()
    {
        IsVisited = true;
        DestroyImmediate(_unvisitedBlock);
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
        //DestroyImmediate(_leftWall);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
        //DestroyImmediate(_rightWall);
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
        //DestroyImmediate(_frontWall);
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
        //DestroyImmediate(_backWall);
    }
}
