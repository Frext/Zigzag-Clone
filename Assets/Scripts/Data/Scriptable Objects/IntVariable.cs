using UnityEngine;

[CreateAssetMenu(fileName = nameof(IntVariable), menuName = "Scriptable Objects/" + nameof(IntVariable), order = 1)]
public class IntVariable : ScriptableObject
{
    public int value;
}
