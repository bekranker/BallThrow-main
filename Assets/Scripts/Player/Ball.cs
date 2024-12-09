using UnityEngine;
using UnityEngine.SceneManagement;



public class Ball : MonoBehaviour
{
    private Camera _cam;
    public GameObject NextlvlPanel;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public CircleCollider2D col;
    [SerializeField] private Animator _Animator;

    [HideInInspector]
    public Vector3 pos
    {
        get
        {
            PlayerPrefs.SetFloat("ballposx" + SceneManager.GetActiveScene().buildIndex, transform.position.x);
            PlayerPrefs.SetFloat("ballposy" + SceneManager.GetActiveScene().buildIndex, transform.position.y);
            PlayerPrefs.SetFloat("ballposz" + SceneManager.GetActiveScene().buildIndex, transform.position.z);

            return transform.position;
        }
    }





    private void Awake()
    {
        _cam = Camera.main;
    }
    private void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PlayerSprite/Player");
    }
    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    public void ActivateRb()
    {
        rb.isKinematic = false;
    }
    public void DesactiveRb()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }
    public void FlyState()
    {
        //_Animator.SetTrigger("FlyingState");
    }
    public void DieState()
    {
        _Animator.SetTrigger("DieState");
    }
    public void NextLvlBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DesactiveRb();
    }
    public void MainMenuBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void LevelsMenuBtn()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}