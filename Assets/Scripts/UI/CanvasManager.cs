using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject PauseCanvas;
    public int restartcounter;
    [SerializeField]
    private ButtonClickManager _PauseButtonClickManager, _ResumeButtonClickManager,
        _RestartButtonClickManager, _NextLevelButton, _LevelSelectionButton, _RestartButtonClickManager2;
    [SerializeField] private GameObject _NextLevelPanel, _GameOverPanel;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private List<Transform> _FilledStars, _WhiteStars;
    [SerializeField] private Transform _LevelCompletedText;
    [SerializeField] private List<Transform> _From;
    [SerializeField] List<Transform> _To;
    [SerializeField] List<ParticleSystem> _Confetties;
    [SerializeField] private Transform _PlayerT, _CansT, _PlayerTTo, _CansTTo;
    [SerializeField] private Transform _particleLeft, _particleRight, _particleTop;
    private void Start()
    {
        Vector2 bottomLeft = new Vector2(Screen.width, Screen.height);

        Debug.Log(bottomLeft);
        _FilledStars.ForEach((star) => { star.localScale = Vector2.zero; });
        if (PlayerPrefs.HasKey("ReCounter"))
        {
            restartcounter = PlayerPrefs.GetInt("ReCounter");
        }
        _PauseButtonClickManager.DoSomething += Pause;
        _ResumeButtonClickManager.DoSomething += PauseExit;
        _RestartButtonClickManager.DoSomething += Restart;
        _RestartButtonClickManager2.DoSomething += Restart;
        _LevelSelectionButton.DoSomething += LevelSelection;
        _NextLevelButton.DoSomething += NextLevel;
    }

    public void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void PauseExit()
    {
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        _GameManager._canPushTheBall = true;
    }
    public void Restart()
    {
        restartcounter++;
        PlayerPrefs.SetInt("ReCounter", restartcounter);
        if (restartcounter == 5)
        {
            restartcounter = 0;
            PlayerPrefs.SetInt("ReCounter", restartcounter);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void NextLevelPanelAction()
    {
        _NextLevelPanel.SetActive(true);
        _LevelCompletedText.GetComponent<TMP_Text>().color = new Color(0.9764706f, 0.3215686f, 0.3215686f, 0);
        _LevelCompletedText.gameObject.SetActive(false);
        StartCoroutine(delayAndAnimatoin());
        _GameOverPanel.SetActive(false);
    }
    private IEnumerator delayAndAnimatoin()
    {

        for (int i = 0; i < _WhiteStars.Count; i++)
        {
            _WhiteStars[i].position = _From[i].position;
            yield return null;
        }
        for (int i = 0; i < _WhiteStars.Count; i++)
        {
            _WhiteStars[i].DOMove(_To[i].position, .25f).SetUpdate(true).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);
        }
        for (int i = 0; i < _FilledStars.Count; i++)
        {
            _FilledStars[i].DOScale(Vector2.one, 0.2f).OnComplete(() =>
            {
                if (i == 2)
                {
                    CreateAudio.PlayAudio("LastStar", 0.4f);
                    Camera.main.DOShakePosition(.3f, .85f, 25).SetUpdate(true);
                    _Confetties.ForEach((_confetti) => { _confetti.gameObject.SetActive(true); _confetti.Play(); });
                }
                else
                {
                    CreateAudio.PlayAudio("Puff", 0.4f);
                    Camera.main.DOShakePosition(0.25f, .5f, 15).SetUpdate(true);
                    _Confetties[i].gameObject.SetActive(true);
                }
            }).SetUpdate(true);
            yield return new WaitForSeconds(.6f);
        }

        _LevelCompletedText.DOScale(Vector2.one, 0.2f).SetUpdate(true).SetEase(Ease.InExpo).OnComplete(() => { Camera.main.DOShakePosition(.3f, .85f, 25).SetUpdate(true); });
        _LevelCompletedText.GetComponent<TMP_Text>().DOFade(1, 0.3f).SetUpdate(true);
        _LevelCompletedText.gameObject.SetActive(true);
        _PlayerT.DOMove(_PlayerTTo.position, 0.75f).SetUpdate(true).SetEase(Ease.OutBack);
        _CansT.DOMove(_CansTTo.position, 0.75f).SetUpdate(true).SetEase(Ease.OutBack);
    }
    public void GameOverPanelAction()
    {
        _GameOverPanel.SetActive(true);
    }
    public void LevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name != "LastScene")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("LevelSelection");
        }
    }
}