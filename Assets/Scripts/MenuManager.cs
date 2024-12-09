using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private ButtonClickManager _StartButton, _LevelSelectionButton;
    [SerializeField] private MenuEffectManager _MenuEffectManager;
    void Start()
    {
        _StartButton.DoSomething += ()=> StartCoroutine(PlayGame());
        _LevelSelectionButton.DoSomething += () => StartCoroutine(LevelSelectionButtonAction());
    }
    public IEnumerator PlayGame()
    {
        _MenuEffectManager.ExitEffect();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel", 1));
    }
    public IEnumerator LevelSelectionButtonAction()
    {
        _MenuEffectManager.ExitEffect();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("LevelSelection");
    }
}
