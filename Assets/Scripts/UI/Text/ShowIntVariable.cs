using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowIntVariable : MonoBehaviour
{
    [SerializeField] private IntVariable intVariable;

    [Tooltip("Text to concatenate with the score variable.")]
    [Multiline]
    [SerializeField] private string precedingText = null;

    [Space]
    [SerializeField] private bool shouldUpdateEveryFrame;

    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        UpdateText();

        if (!shouldUpdateEveryFrame)
        {
            enabled = false;
        }
    }

    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (textMeshPro != null)
        {

            textMeshPro.text = precedingText + " " + intVariable.value.ToString();
        }
    }
}
