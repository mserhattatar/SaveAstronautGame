using System.Collections;
using TMPro;
using UnityEngine;


public class CanvasManager : MonoBehaviour
{
    public delegate void CanvasDelegate();

    public static CanvasDelegate GameStartDelegate;
    public static CanvasDelegate GameOverDelegate;
    public static CanvasDelegate GameResetDelegate;
    public static CanvasDelegate OxygenPercentDelegate;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI levelRemainingDistanceMText;
    [SerializeField] private GameObject levelRemainingDistanceText;
    [SerializeField] private GameObject oxygenTankDamageText;
    [SerializeField] private TextMeshProUGUI oxygenTankDamagePercentText;
    [SerializeField] private Transform astronautPlayer;
    private int _oxygenPercent;

    private void Start()
    {
        LoadingPanelSetActive();
        SetOxygenPercent();
        GameStartDelegate += GameEndPanelSetPassive;
        GameStartDelegate += LevelRemainingDistanceTextSetActive;
        GameStartDelegate += OxygenPercentTextSetActive;
        GameStartDelegate += OxygenTankDamageTextSetActive;
        GameOverDelegate += OxygenPercentTextSetPassive;
        GameOverDelegate += LevelRemainingDistanceTextSetPassive;
        GameOverDelegate += GameOverPanelSetActive;
        GameResetDelegate += LoadingPanelSetActive;
        GameResetDelegate += SetOxygenPercent;
        GameResetDelegate += OxygenPercentTextSetPassive;
        GameResetDelegate += GameEndPanelSetPassive;
        OxygenPercentDelegate += OxygenPercentUpdate;
        OxygenPercentDelegate += OxygenTankDamageTextSetActive;
    }

    private void Update()
    {
        LevelScoreMetreText();
        OxygenPercentText();
    }


    private void LoadingPanelSetActive()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingPanelSetPassiveDelay());
    }

    private IEnumerator LoadingPanelSetPassiveDelay()
    {
        yield return new WaitForSeconds(1.3f);
        loadingPanel.SetActive(false);
    }

    private void GameOverPanelSetActive()
    {
        GameOverPanelScoreText();
        gameOverPanel.SetActive(true);
    }

    private void GameOverPanelScoreText()
    {
        gameOverText.text = "Remaining Distance = " + (100 - (int) astronautPlayer.position.y) + " Meters";
    }

    private void GameEndPanelSetPassive()
    {
        gameOverPanel.SetActive(false);
    }


    private void LevelRemainingDistanceTextSetActive()
    {
        levelRemainingDistanceMText.gameObject.SetActive(true);
    }

    private void LevelRemainingDistanceTextSetPassive()
    {
        levelRemainingDistanceMText.gameObject.SetActive(false);
    }

    private void LevelScoreMetreText()
    {
        if (levelRemainingDistanceMText.gameObject.activeInHierarchy)
            levelRemainingDistanceMText.text = (100 - (int) astronautPlayer.position.y).ToString();
    }

    private void OxygenPercentTextSetActive()
    {
        oxygenTankDamagePercentText.gameObject.SetActive(true);
    }

    private void OxygenPercentTextSetPassive()
    {
        oxygenTankDamagePercentText.gameObject.SetActive(false);
    }

    private void OxygenPercentText()
    {
        oxygenTankDamagePercentText.text = "%"+ _oxygenPercent;
    }

    private void OxygenTankDamageTextSetActive()
    {
        oxygenTankDamageText.SetActive(true);
        StartCoroutine(OxygenTankDamageTextSetPassive());
    }

    private IEnumerator OxygenTankDamageTextSetPassive()
    {
        yield return new WaitForSeconds(4f);
        oxygenTankDamageText.SetActive(false);
    }

    private void OxygenPercentUpdate()
    {
        _oxygenPercent += 10;
        OxygenPercentText();
    }

    private void SetOxygenPercent()
    {
        _oxygenPercent = 50;
    }


    public void ResetLevelButton()
    {
        GameResetDelegate();
    }
}