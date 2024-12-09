using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEffectManager : MonoBehaviour
{
    [SerializeField] private List<Image> _UIs;
    [SerializeField] List<Transform> _Down;
    [SerializeField] List<Transform> _Up;
    [SerializeField, Range(0.05f, 1)] private float _Speed;
    [SerializeField] private List<SpriteRenderer> _GoFade;
    [SerializeField] private List<GameObject> _Environment;
    [SerializeField] private ParticleSystem _PuffParticle;
    [SerializeField] private Transform _PlayerT, _CansT, _PlayerTo, _CansTo, _PlayerFrom, _CansFrom;
    private bool _passed;
    [SerializeField] private CanvasGroup _CanvasGroup;
    private void Start()
    {
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
        if (_CanvasGroup != null)
        {
            _CanvasGroup.DOFade(0, _Speed).SetUpdate(true);
        }
        if (_UIs != null)
        {
            for (int i = 0; i < _UIs.Count; i++)
            {
                _UIs[i].DOFade(0, _Speed).SetUpdate(true);
            }
        }
        Ball[] balls = FindObjectsOfType<Ball>();
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].gameObject.activeSelf)
            {
                yield return new WaitForSeconds(.25f);
                Instantiate(_PuffParticle, balls[i].transform.position, balls[i].transform.rotation);
                balls[i].gameObject.SetActive(false);
            }
        }
        if (_Environment != null)
        {
            for (int i = 0; i < _Environment.Count; i++)
            {
                if (_Environment[i].activeSelf)
                {
                    yield return new WaitForSeconds(.25f);
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
        _PlayerT.DOMove(_PlayerFrom.position, _Speed).SetEase(Ease.InBack).SetUpdate(true);
        _CansT.DOMove(_CansFrom.position, _Speed).SetEase(Ease.InBack).SetUpdate(true);

        _GoFade?.ForEach((goFadeObject) =>
        {
            goFadeObject.DOFade(0, _Speed).SetUpdate(true);
        });
    }
    private IEnumerator GoEnter()
    {
        _PlayerT.position = _PlayerFrom.position;
        _CansT.position = _CansFrom.position;
        if (_CanvasGroup != null)
        {
            _CanvasGroup.alpha = 0;
        }
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
            goFadeObject.DOFade(Random.Range(.5f, 1), _Speed).SetUpdate(true);
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
        if (_CanvasGroup != null)
        {
            _CanvasGroup.DOFade(1, _Speed).SetUpdate(true);
        }
        yield return new WaitForSeconds(1);
        _PlayerT.DOMove(_PlayerTo.position, _Speed).SetEase(Ease.OutBack).SetUpdate(true);
        _CansT.DOMove(_CansTo.position, _Speed).SetEase(Ease.OutBack).SetUpdate(true);

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
            _Environment[i].SetActive(true);
        }
    }
}
