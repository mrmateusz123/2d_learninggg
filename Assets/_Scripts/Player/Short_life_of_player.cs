using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Short_life_of_player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private int maxhp = 100;
    private int hp;
    private bool dd = true;
    [SerializeField] private Text hptext;
    [SerializeField] private AudioSource AudioSource;
    private void Start()
    {
        hp = maxhp;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart_Level();
        }
        if (hp == 0 && dd == true)
        {
            hptext.text = "HP: " + hp;
            dd = false;
            ded();

;       }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("traps"))
        {
            hp = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enbullet"))
        {
            hp = hp - 20;
            hptext.text = "HP: " + hp;
            Destroy(collision.gameObject);
        }
    }

private void ded()
    {
        rb.bodyType = RigidbodyType2D.Static;
        AudioSource.enabled = false;
        animator.SetTrigger("Death");
    }
    private void Restart_Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
