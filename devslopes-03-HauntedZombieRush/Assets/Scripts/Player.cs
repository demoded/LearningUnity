using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100.0f;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;
    [SerializeField] private AudioClip sfxCoin;

    private string paramIsDead = "isDead";
    private string paramYVelocity = "yVelocity";
    private Rigidbody rgdbody;
    private Animator animator;
    private AudioSource audioSource;

    private bool isJumping;

    private void Awake()
    {
        rgdbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(rgdbody);
        Assert.IsNotNull(animator);
        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(sfxDeath);
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxCoin);
    }


    // Update is called once per frame
    private void Update()
    {
        CheckForInput();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        HandleJump();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat(paramYVelocity, rgdbody.velocity.y);
    }

    private void HandleJump()
    {
        if (isJumping)
        {
            isJumping = false;
            rgdbody.velocity = new Vector2(0, 0);
            rgdbody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }
    }

    private void CheckForInput()
    {
        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.instance.PlayerStartedGame();
                rgdbody.useGravity = true;
                isJumping = true;
                audioSource.PlayOneShot(sfxJump);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Obstacle":
                audioSource.PlayOneShot(sfxDeath);
                rgdbody.AddForce(new Vector3(jumpForce / 2, jumpForce, jumpForce / 2), ForceMode.Impulse);
                GameManager.instance.PlayerCollided();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "coin":
                audioSource.PlayOneShot(sfxCoin);
                GameManager.instance.ToggleVisibility(other.gameObject, false);
                GameManager.instance.CoinGrab();
                break;
            default:
                break;
        }
    }

    public void InitPlayer()
    {
        rgdbody.velocity = Vector3.zero;
        rgdbody.angularVelocity = Vector3.zero;
        rgdbody.useGravity = false;
        transform.position = new Vector3(-7.3f, 14.3f, -0.3f);
        transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    }
}
