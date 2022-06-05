using System;
using JetBrains.Annotations;

public static class EventManager
{
    public static Action<int> OnStack;
    public static Action<int> OnOrderBy;
    public static Action<bool> OnComboActivated;
    
    public static Action OnDie;
    public static Action OnWin;
    public static Action OnCombo;

}