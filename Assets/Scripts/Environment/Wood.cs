using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wood : MonoBehaviour
{
    public List<GameObject> Cans = new List<GameObject>();
    [SerializeField] private GameObject _NextLevelPanel;
    public int Cansint;
    private bool _canFinish;
    private void Start()
    {
        _canFinish = true;
    }
    private void Update()
    {
        if (Cansint == 0 && _canFinish)
        {
            Cansint = -1;
            NextLevel();
            _canFinish = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cans"))
        {
            Cans.Remove(collision.gameObject);
            Cansint -= 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cans"))
        {
            if (Cans.Contains(collision.gameObject))
                return;
            Cans.Add(collision.gameObject);
            Cansint += 1;
        }
    }
    void NextLevel()
    {

        _NextLevelPanel.SetActive(true);
        GameObject.Find("Ball").GetComponent<Ball>().DesactiveRb();
    }
}
