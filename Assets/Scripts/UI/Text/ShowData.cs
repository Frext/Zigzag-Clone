using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowData : MonoBehaviour
{
    [SerializeField] DataTypes dataTypeToShow;
    public enum DataTypes
    {
        HighScore,
        GemCount
    }

    [Space]
    [Tooltip("Text to concatenate with the data.")]
    [Multiline]
    [SerializeField] private string precedingText = String.Empty;

    [Space]
    [SerializeField] private bool shouldUpdateEveryFrame;

    [Header("Scripts")]
    [SerializeField] private DataManager dataManager;


    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        UpdateDataType();

        if (!shouldUpdateEveryFrame)
        {
            enabled = false;
        }
    }

    void Update()
    {
        UpdateDataType();    
    }

    private void UpdateDataType()
    {
        if (dataTypeToShow == DataTypes.HighScore)
        {

            textMeshPro.text = precedingText + dataManager.latestData.HighScore;
        }
        else if (dataTypeToShow == DataTypes.GemCount)
        {
            textMeshPro.text = precedingText + dataManager.latestData.GemCount;
        }
    }
}
