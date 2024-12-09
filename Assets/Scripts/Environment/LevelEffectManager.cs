using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEffectManager : MonoBehaviour
{
    [SerializeField] private List<Image> _UIs;
    [SerializeField] List<Transform> _Down;
    [SerializeField] List<Transform> _Up;
    [SerializeField, Range(0.05f, 1)] private float _Speed;
    [SerializeField] private List<SpriteRenderer> _GoFade;
    [SerializeField] private List<GameObject> _Environment;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private ParticleSystem _PuffParticle;
    [SerializeField] private CanvasManager _CanvasManager;
    private bool _passed;
    Ball[] _ball;
    private void Start()
    {
        _ball = FindObjectsOfType<Ball>();
        GameObject[] tempFindedEnvironment = GameObject.FindGameObjectsWithTag("Environment");
        GameObject[] tempFindedCans = GameObject.FindGameObjectsWithTag("Cans");


        _Environment.AddRange(tempFindedEnvironment);
        _Environment.AddRange(tempFindedCans);


        _Environment.ForEach((item) =>
        {
            if (item.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        });

        StartCoroutine(GoEnter());
    }
    public void EntryEffect()
    {
        StartCoroutine(GoEnter());
    }
    public void ExitEffect(bool win = true)
    {
        if (_passed) return;
        StartCoroutine(GoExit(win));
        _passed = true;
    }

    private IEnumerator GoExit(bool win = true)
    {
        if (_UIs != null)
        {
            for (int i = 0; i < _UIs.Count; i++)
            {
                _UIs[i].DOFade(0, _Speed).SetUpdate(true);
            }
        }
        for (int i = 0; i < _ball.Length; i++)
        {
            if (_ball[i].gameObject.activeSelf)
            {
                yield return new WaitForSeconds(.25f);
                CreateAudio.PlayAudio("PuffCloud", 0.35f);
                Instantiate(_PuffParticle, _ball[i].transform.position, _ball[i].transform.rotation);
                _ball[i].gameObject.SetActive(false);
            }
        }
        if (_Environment != null)
        {
            for (int i = 0; i < _Environment.Count; i++)
            {
                if (_Environment[i].activeSelf)
                {
                    yield return new WaitForSeconds(.25f);
                    CreateAudio.PlayAudio("PuffCloud", 0.35f);
                    Instantiate(_PuffParticle, _Environment[i].transform.position, _Environment[i].transform.rotation);
                    _Environment[i].SetActive(false);
                }
            }
        }
        for (int i = 0; i < _Down.Count; i++)
        {
            _Down[i].DOMove(_Down[i].position + (Vector3.down * 4), _Speed).SetEase(Ease.InBack).SetUpdate(true);
            yield return new WaitForSeconds(.1f);
        }
        for (int i = 0; i < _Up.Count; i++)
        {
            _Up[i].DOMove(_Up[i].position + (Vector3.up * 4), _Speed).SetEase(Ease.InBack).SetUpdate(true);
            yield return new WaitForSeconds(.1f);
        }

        _GoFade?.ForEach((goFadeObject) =>
        {
            goFadeObject.DOFade(0, .65f).SetUpdate(true);
        });

        yield return new WaitForSeconds(.5f);
        if (win)
        {
            if (_CanvasManager != null)
                _CanvasManager.NextLevelPanelAction();
            PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if (_CanvasManager != null)
                _CanvasManager.GameOverPanelAction();
        }
    }
    private IEnumerator GoEnter()
    {
        for (int i = 0; i < _ball.Length; i++)
        {
            if (_ball[i].gameObject.activeSelf)
            {
                _GameManager.Ball.Add(_ball[i]);
                _ball[i].gameObject.SetActive(false);
            }
        }
        _GameManager.shotCount = _GameManager.Ball.Count;
        _GameManager._CounterTMP.text = "X" + _GameManager.shotCount.ToString();

        if (_UIs != null)
        {
            for (int i = 0; i < _UIs.Count; i++)
            {
                _UIs[i].color = new Color(255, 255, 255, 0);
            }
        }
        _Environment?.ForEach((environment) =>
        {
            environment.SetActive(false);
        });
        _Down?.ForEach((downObject) =>
        {
            downObject.position += (Vector3.down * 4);
        });
        _Up?.ForEach((upObject) =>
        {
            upObject.position += (Vector3.up * 4);
        });
        _GoFade?.ForEach((goFadeObject) =>
        {
            goFadeObject.DOFade(Random.Range(.5f, 1), .5f).SetUpdate(true);
        });
        for (int i = 0; i < _Down.Count; i++)
        {
            _Down[i].DOMove(_Down[i].position + (Vector3.up * 4), _Speed).SetEase(Ease.OutBack).SetUpdate(true);
            yield return new WaitForSeconds(.1f);
        }
        for (int i = 0; i < _Up.Count; i++)
        {
            _Up[i].DOMove(_Up[i].position + (Vector3.down * 4), _Speed).SetEase(Ease.OutBack).SetUpdate(true);
            yield return new WaitForSeconds(.1f);
        }

        yield return new WaitForSeconds(1);
        if (_UIs != null)
        {
            for (int i = 0; i < _UIs.Count; i++)
            {
                _UIs[i].DOFade(1, _Speed).SetUpdate(true);
            }
        }
        for (int i = 0; i < _Environment.Count; i++)
        {
            yield return new WaitForSeconds(.25f);
            CreateAudio.PlayAudio("PuffCloud", 0.35f);
            _Environment[i].SetActive(true);
        }
        Cans[] cans = FindObjectsOfType<Cans>();
        _GameManager.TargetHit = cans.Length;
        yield return new WaitForSeconds(.25f);
        _Environment?.ForEach((item) =>
        {
            if (item.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        });
        if (_GameManager != null)
            _GameManager.EnableBall();
    }
}