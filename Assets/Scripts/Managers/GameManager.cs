using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.VisualScripting;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public int BallIndex;
    public static GameManager Instance;
    public int HittedObjectCount, TargetHit;
    public int shotCount;
    public bool wait = true;
    public List<Ball> Ball;
    public Trajectory trajectory;
    public bool IsDragging = false;
    public static event Action OnDragEndAction, OnDragStartAction;


    [SerializeField] private float _PushForce = 4f;
    [SerializeField] private float _EffectSpeed, _EffectForce;
    [SerializeField] private LevelEffectManager _LevelEffectManager;
    [SerializeField] public TMP_Text _CounterTMP;
    [SerializeField] private TMP_Text _LevelText;


    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _direction;
    private Vector2 _force;
    private float _distance;
    private Camera _cam;
    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(1);
    public bool _canPushTheBall;




    private void Awake()
    {
        BallIndex = 0;
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        _cam = Camera.main;
        _LevelText.text = $"Level {SceneManager.GetActiveScene().buildIndex}";
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && shotCount > 0)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                IsDragging = true;
                OnDragStart();
            }

        }

        if (Input.GetMouseButtonUp(0) && shotCount > 0 && IsDragging == true)
        {
            if (EventSystem.current.currentSelectedGameObject == null && IsDragging == true)
            {
                IsDragging = false;

                OnDragEnd();
            }
        }


        if (IsDragging)
        {
            OnDrag();
        }
    }
    public void EnableBall()
    {
        _canPushTheBall = true;
        CreateAudio.PlayAudio("PuffCloud", 0.5f);
        Ball[BallIndex].gameObject.SetActive(true);
    }
    public void IncreaseScore()
    {
        if (HittedObjectCount + 1 == TargetHit)
        {
            _LevelEffectManager.ExitEffect();
            HittedObjectCount++;
        }
        else
        {
            HittedObjectCount++;
        }
    }
    private void OnDragStart()
    {
        if (!_canPushTheBall) return;
        OnDragStartAction?.Invoke();
        Ball[BallIndex].DesactiveRb();
        _startPoint = _cam.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();
    }
    private void OnDragEnd()
    {
        if (!_canPushTheBall) return;
        Ball[BallIndex].ActivateRb();
        Ball[BallIndex].Push(_force);
        Ball[BallIndex].FlyState();
        trajectory.Hide();
        PlayerPrefs.SetFloat("forcex" + SceneManager.GetActiveScene().buildIndex, _force.x);
        PlayerPrefs.SetFloat("forcey" + SceneManager.GetActiveScene().buildIndex, _force.y);
        if (_canPushTheBall)
        {
            _canPushTheBall = false;
            StartCoroutine(WaitForOtherBall());
            OnDragEndAction?.Invoke();
        }
    }
    private IEnumerator WaitForOtherBall()
    {
        DecreaseCounter();
        yield return _delay;
        if (BallIndex + 1 < Ball.Count)
        {
            BallIndex++;
            Ball[BallIndex].gameObject.SetActive(true);
            _canPushTheBall = true;
        }
        if (shotCount <= 0)
        {
            _canPushTheBall = false;
            StartCoroutine(WaitForGameOverScreen());
        }
    }
    private void DecreaseCounter()
    {
        shotCount--;
        _CounterTMP.text = "X" + shotCount.ToString();
        _CounterTMP.transform.DOPunchScale(_EffectForce * Vector2.one, _EffectSpeed).SetUpdate(true);
    }
    private IEnumerator WaitForGameOverScreen()
    {
        yield return new WaitForSeconds(3);
        if (HittedObjectCount == TargetHit)
        {
            _LevelEffectManager.ExitEffect();
            yield return null;
            yield break;
        }
        else
        {
            _LevelEffectManager.ExitEffect(false);
        }
    }
    private void OnDrag()
    {
        if (!_canPushTheBall) return;

        _endPoint = _cam.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = (_direction * _distance * _PushForce);
#if UNITY_EDITOR
        Debug.DrawLine(_startPoint, _endPoint);
#endif
        trajectory.UpdateDots(Ball[BallIndex].pos, _force);
    }
}