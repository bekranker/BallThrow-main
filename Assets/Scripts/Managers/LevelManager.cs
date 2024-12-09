using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Locks;
    [SerializeField] private Sprite _UnlockedSprite;
    [SerializeField] private ButtonClickManager _HomeButtonClickManager;
    [SerializeField] private List<Button> _Buttons;
     private void Start()
     {
        for (int a = 0; a < _Buttons.Count; a++)
        {
            if (a <= PlayerPrefs.GetInt("LastLevel"))
            {
                Locks[a].GetComponentInChildren<TMP_Text>().text = (a + 1).ToString();
                Locks[a].GetComponent<Image>().sprite = _UnlockedSprite;
            }
            else
            {
                Locks[a].GetComponentInChildren<TMP_Text>().enabled = false;
                Locks[a].GetComponent<Button>().enabled = false;
                Locks[a].GetComponentInChildren<ButtonClickManager>().enabled = false;
            }
        }
        _HomeButtonClickManager.DoSomething += () => SceneManager.LoadScene(0);
     }
    public void OpenLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}