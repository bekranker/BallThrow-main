using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private ParticleSystem _PuffParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cans"))
        {
            _GameManager.IncreaseScore();
            CreateAudio.PlayAudio("CansDieSoundEffect", .25f);
            Instantiate(_PuffParticle, collision.transform.position, collision.transform.rotation);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Environment"))
        {
            CreateAudio.PlayAudio("CansDieSoundEffect", .25f);
            Instantiate(_PuffParticle, collision.transform.position, collision.transform.rotation);
            collision.gameObject.SetActive(false);
        }
    }
}
