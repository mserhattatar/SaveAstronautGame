using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject homePanel;

    [SerializeField] private TextMeshProUGUI gameEndScoreText;
    [SerializeField] private TextMeshProUGUI startPanelBestScoreText;
    [SerializeField] private TextMeshProUGUI levelScoreText;
    [SerializeField] private Transform astronautPlayer;
    public int bestScore;

    private void Awake()
    {
        instance = this;
        //StudentCollisionController.TeacherCollisionDelegate += GameEndPanelScoreText;
        //StudentCollisionController.TeacherCollisionDelegate += GameEndPanelSetPassive;
        //StudentCollisionController.TeacherCollisionDelegate += GameEndPanelSetActive;
        //StudentCollisionController.TeacherCollisionDelegate += LevelScoreMetreTextSetPassive;
        GameManager.ResetLevelDelegate += GameEndPanelSetPassive;
    }

    private void Start()
    {
        HomePanelSetActive();
    }

    private void Update()
    {
        LevelScoreMetreText();
    }

    private void HomePanelSetPassive()
    {
        homePanel.SetActive(false);
    }

    private void HomePanelSetActive()
    {
        homePanel.SetActive(true);
        StartCoroutine(HomePanelSetPassiveDelay());
    }

    private IEnumerator HomePanelSetPassiveDelay()
    {
        yield return new WaitForSeconds(1.3f);
        HomePanelSetPassive();
        StartPanelSetActive(true);
    }

    private void StartPanelSetActive(bool setActive)
    {
        startPanel.SetActive(setActive);
        if (setActive)
            StartPanelBestScore();
    }

    private void StartPanelBestScore()
    {
        startPanelBestScoreText.text = (bestScore + " M");
    }

    private void GameEndPanelSetActive()
    {
        gameEndPanel.SetActive(true);
    }

    private void GameEndPanelScoreText()
    {
        gameEndScoreText.text = "Score = " + (int) astronautPlayer.position.y / 2 + " meters";
        UpdateBestScore();
    }

    private void GameEndPanelSetPassive()
    {
        gameEndPanel.SetActive(false);
    }


    private void LevelScoreMetreTextSetActive()
    {
        levelScoreText.gameObject.SetActive(true);
    }

    private void LevelScoreMetreTextSetPassive()
    {
        levelScoreText.gameObject.SetActive(false);
    }

    private void LevelScoreMetreText()
    {
        if (levelScoreText.gameObject.activeInHierarchy)
            levelScoreText.text = (int) astronautPlayer.position.y / 2 + "M";
    }

    private void UpdateBestScore()
    {
        if (bestScore < (int) astronautPlayer.position.z / 2)
            bestScore = (int) astronautPlayer.position.z / 2;
    }

    public void ResetLevelButton()
    {
        GameManager.ResetLevelDelegate();
        GameDataScript.SaveLevelDataAsJson();
        HomePanelSetActive();
    }
}