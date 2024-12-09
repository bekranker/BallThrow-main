using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDedection : MonoBehaviour
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private ParticleSystem _PuffParticle;

    private bool _canRunDieState;



    private void Start()
    {
        _canRunDieState = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _Animator.SetTrigger("DieState");
        StartCoroutine(DieState());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canRunDieState) return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canRunDieState = false;
            StartCoroutine(DieState());
        }
    }
    private IEnumerator DieState()
    {
        yield return new WaitForSeconds(4);
        CreateAudio.PlayAudio("Puff", 0.35f);
        Instantiate(_PuffParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
