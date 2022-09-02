using System;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    [Header("Int Variables")]
    [SerializeField] private IntVariable score;

    #region Data Properties
    private int _gemCount;
    private int GemCount
    {
        get { return _gemCount; }
        set
        {
            _gemCount = value;
            SaveGemCount();
        }
    }

    private void SaveGemCount()
    {
        DataContainer dc = dataManager.latestData;

        if (dc.GemCount == GemCount)
            return;


        dc.GemCount = GemCount;

        dataManager.SaveData(dc);
    }

    private int _highScore;
    private int HighScore
    {
        get { return _highScore; }
        set
        {
            _highScore = value;
            SaveHighScore();
        }
    }

    private void SaveHighScore()
    {
        DataContainer dc = dataManager.latestData;

        if (dc.HighScore == HighScore)
            return;


        dc.HighScore = HighScore;

        dataManager.SaveData(dc);
    }

    #endregion


    [Header("Reward Amounts")]
    [SerializeField] int gemCollectingReward;
    [SerializeField] int switchDirectionReward;

    [Header("Scripts")]
    [SerializeField] private PlayerMovement playerMovementScript;
    [Space]
    [SerializeField] private UIAnimationHandler animationHandler;
    [SerializeField] private SFXHandler sfxHandler;
    [SerializeField] private DataManager dataManager;

    private void Start()
    {
        SetScore(0);

        HandleStartAnimation();

        LoadData();
    }

    private void SetScore(int val)
    {
        score.value = (int)Mathf.Clamp(val, 0, Mathf.Infinity);
    }

    private void HandleStartAnimation()
    {
        animationHandler.EnableMainMenu();
    }

    private void LoadData()
    {
        HighScore = dataManager.latestData.HighScore;
        GemCount = dataManager.latestData.GemCount;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Gem"))
        {

            SetScore(score.value + gemCollectingReward);


            sfxHandler.PlayAudioClip(SFXHandler.AudioClips.GemCollect);


            GemController gemController = collision.gameObject.GetComponent<GemController>();

            gemController.PlayGemCollectParticleEffect();
            gemController.EnableGemCollectText();
            gemController.HideGem();
        }
    }

    #region Public Methods

    // This method is called whenever user clicks on an empty space.
    public void IncrementScoreForSwitchingDirection()
    {
        SetScore(score.value + switchDirectionReward);
    }

    public void UpdateHighScore()
    {
        if (score.value > HighScore)
        {

            HighScore = score.value;
        }
    }

    public bool IsNewHighScore()
    {
        if (score.value > HighScore)
        {

            return true;
        }

        return false;
    }

    public void IncreaseGemCountByScore()
    {
        GemCount = (int)Mathf.Clamp(GemCount + score.value, 0, Mathf.Infinity);
    }

    public void DecreaseGems(int amountToDecrease)
    {
        GemCount = (int)Mathf.Clamp(GemCount - amountToDecrease, 0, Mathf.Infinity);
    }

    #endregion
}
