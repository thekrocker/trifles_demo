using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game_Props;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackSystem : MonoBehaviour
{
    public List<StackCube> CurrentCubeStacks = new();

    [SerializeField] private StackCube objToSpawn;

    public static StackSystem Instance;

    public Action<int> OnStack;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private int _cubeNameIdx;


    private void OnEnable()
    {
        EventManager.OnShuffle += CheckMatch;
    }

    private void OnDisable()
    {
        EventManager.OnShuffle -= CheckMatch;
    }

    public void AddStack(IStackable stackable, Transform playerTransform, Color color)
    {
        Debug.Log("Adding stack...");
        var cubeToAdd = Instantiate(objToSpawn, playerTransform, true);

        _cubeNameIdx++;
        cubeToAdd.name = $"Stacked Cube: {_cubeNameIdx}";
        cubeToAdd.SetColor(color);

        CurrentCubeStacks.Add(cubeToAdd);

        var calculatedOffset = CurrentCubeStacks.Count * stackable.GetHeight();

        cubeToAdd.transform.position = new Vector3(playerTransform.position.x,
            playerTransform.position.y - calculatedOffset, playerTransform.position.z);


        CheckMatch();

        OnStack?.Invoke(CurrentCubeStacks.Count);
    }

    public const int MATCH_COUNT = 3;


    #region Handle

    private void CheckMatch()
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
            Debug.Log("Match..");
            RemoveCubes(i, count);
        }
    }


    private void RemoveCubes(int index, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            CurrentCubeStacks[index - i].ScaleDown();
            CurrentCubeStacks.RemoveAt(index - i);
        }
        SetPositions();
    }

    private void SetPositions()
    {
        for (int i = 0; i < CurrentCubeStacks.Count; i++)
        {
            var cube = CurrentCubeStacks[i];
            cube.transform.localPosition = new Vector3(cube.transform.localPosition.x, (i + 1) * -1f ,cube.transform.localPosition.z);
        }
        
        CheckMatch();
    }


    public void ShuffleColors()
    {
        for (var i = CurrentCubeStacks.Count - 1; i > 1; i--)
        {
            var j = Random.Range(0, CurrentCubeStacks.Count - 1);

            (CurrentCubeStacks[j].transform.localPosition, CurrentCubeStacks[i].transform.localPosition) = (
                CurrentCubeStacks[i].transform.localPosition, CurrentCubeStacks[j].transform.localPosition); // set position

            (CurrentCubeStacks[j], CurrentCubeStacks[i]) = (CurrentCubeStacks[i], CurrentCubeStacks[j]); // set index
        }

        Debug.Log("Shuffling..");
        EventManager.OnShuffle?.Invoke();
    }

    public void OrderColors()
    {
        Debug.Log("Ordering colors..");
    }
}

#endregion