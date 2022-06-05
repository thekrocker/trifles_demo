using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using Game_Props;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackSystem : MonoBehaviour
{
    public List<StackCube> CurrentCubeStacks { get; set; } = new();

    [SerializeField] private PlayerInteraction player;
    [SerializeField] private StackCube objToSpawn;

    #region private

    private int _matchComboCount;
    private int _cubeNameIdx;
    private float _elapsedLavaTime;
    private float _lavaDamageInterval = .2f;
    private float _elapsedTime;
    private float _comboResetThreshold = 10f;
    private List<StackCube> _orderedList = new List<StackCube>();

    #endregion


    private const int MATCH_COUNT = 3;

    public static StackSystem Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    public void AddStack(IStackable stackable, Transform playerTransform, Color color)
    {
        Debug.Log("Adding stack...");
        var cubeToAdd = Instantiate(objToSpawn, playerTransform, true);

        // Set cube properties
        _cubeNameIdx++;
        cubeToAdd.name = $"Stacked Cube: {_cubeNameIdx}";
        cubeToAdd.SetColor(color);

        CurrentCubeStacks.Add(cubeToAdd);

        // Set cube position on stacking
        var calculatedOffset = CurrentCubeStacks.Count * stackable.GetHeight();
        cubeToAdd.transform.position = new Vector3(playerTransform.position.x,
            playerTransform.position.y - calculatedOffset, playerTransform.position.z);


        // set player posititoning, then cubes 
        SetPlayerPositioning();

        EventManager.OnStackChange?.Invoke(); // for trail renderer

        EventManager.OnStack?.Invoke(CurrentCubeStacks.Count);


        CheckMatch();
    }


    private void CheckMatch() // TODO: later on MatchSystem class
    {
        if (CurrentCubeStacks.Count <= 0) return;

        var current = CurrentCubeStacks[0];
        var count = 1;

        int i;

        for (i = 1; i < CurrentCubeStacks.Count; i++)
        {
            if (CurrentCubeStacks[i].CurrentColor == current.CurrentColor)
            {
                count++;
            }
            else
            {
                if (count >= MATCH_COUNT) break;
                count = 1;
                current = CurrentCubeStacks[i];
            }
        }

        if (count >= MATCH_COUNT)
        {
            _matchComboCount++;

            if (_matchComboCount >= 3)
            {
                EventManager.OnCombo?.Invoke();
                _matchComboCount = 0;
            }

            CountCombo();
            RemoveCubes(i, count);
        }
    }


    private void CountCombo()
    {
        StartCoroutine(CO_CountCombo());

        _elapsedTime = 0;

        IEnumerator CO_CountCombo()
        {
            while (_elapsedTime < _comboResetThreshold)
            {
                if (_matchComboCount >= 3)
                {
                    _matchComboCount = 0;
                    yield break;
                }

                _elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void RemoveCubes(int index, int count)
    {
        EventManager.OnStackChange?.Invoke();

        for (int i = 1; i <= count; i++)
        {
            CurrentCubeStacks[index - i].ScaleDown(.4f);
            CurrentCubeStacks.RemoveAt(index - i);
        }

        SetPlayerPositioning();
    }


    private void SetPositions()
    {
        for (int i = 0; i < CurrentCubeStacks.Count; i++)
        {
            var cube = CurrentCubeStacks[i];
            cube.transform.localPosition = new Vector3(cube.transform.localPosition.x, (i + 1) * -1f,
                cube.transform.localPosition.z);
        }


        CheckMatch();
    }


    public void ShuffleColors()
    {
        for (var i = CurrentCubeStacks.Count - 1; i > 1; i--)
        {
            var j = Random.Range(0, CurrentCubeStacks.Count - 1);

            (CurrentCubeStacks[j].transform.localPosition, CurrentCubeStacks[i].transform.localPosition) = (
                CurrentCubeStacks[i].transform.localPosition,
                CurrentCubeStacks[j].transform.localPosition); // set position

            (CurrentCubeStacks[j], CurrentCubeStacks[i]) = (CurrentCubeStacks[i], CurrentCubeStacks[j]); // set index
        }

        Debug.Log("Shuffling..");


        SetPositions();
    }

    public void OrderColors()
    {
        if (CurrentCubeStacks.Count <= 2) return;

        _orderedList = CurrentCubeStacks.OrderBy(x => x.CurrentColor.GetHashCode()).ToList();
        CurrentCubeStacks = _orderedList;

        SetPlayerPositioning();
    }

    public void HandleRegularObstacle(int cost)
    {
        if (cost > CurrentCubeStacks.Count)
        {
            EventManager.OnDie?.Invoke();
            return;
        }

        for (int i = 0; i < cost; i++)
        {
            var currentCube = CurrentCubeStacks[^1];
            currentCube.transform.SetParent(null);
            currentCube.ScaleDown(1f);
            CurrentCubeStacks.RemoveAt(CurrentCubeStacks.Count - 1);
            SetPlayerPositioning();
        }

        EventManager.OnStackChange?.Invoke();
    }

    public void HandleLavaObstacle()
    {
        _elapsedTime = 0f;

        StartCoroutine(CO_HandleLavaObstacle());

        IEnumerator CO_HandleLavaObstacle()
        {
            while (player.IsPlayerInArea)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _lavaDamageInterval)
                {
                    if (CurrentCubeStacks.Count <= 0)
                    {
                        EventManager.OnDie?.Invoke();
                        yield break;
                    }

                    var currentCube = CurrentCubeStacks[^1];
                    currentCube.ScaleDown(1f);
                    CurrentCubeStacks.RemoveAt(CurrentCubeStacks.Count - 1);
                    SetPlayerPositioning();
                    _elapsedTime = 0f;
                    EventManager.OnStackChange?.Invoke();
                }

                yield return null;
            }
        }
    }

    private void SetPlayerPositioning()
    {
        player.SetPlayerPosition(CurrentCubeStacks.Count, SetPositions);
    }
}