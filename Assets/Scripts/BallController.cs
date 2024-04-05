using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControler : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 5;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text player1Score_TL;
    [SerializeField] private Text player2Score_TL;
    [SerializeField] private Text player1Score_BR;
    [SerializeField] private Text player2Score_BR;

    int scoreP1;
    int scoreP2;

    private int hitCounter;
    private Rigidbody2D rb;
    private Vector2 initialDirection = Vector2.up;

    GameObject panelSelesai;
    Text txPemenang;

    AudioSource audio;
    public AudioClip hitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);

        scoreP1 = 0;
        scoreP2 = 0;

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = initialDirection * (initialSpeed + speedIncrease * hitCounter);

    }

    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.localPosition = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }


    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;
        if (transform.position.y > 0)
        {
            yDirection = -1;
        }
        else
        {
            yDirection = 1;
        }
        xDirection = (ballPos.x - playerPos.x) / myObject.GetComponent<Collider2D>().bounds.size.x;
        if (xDirection == 0)
        {
            xDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audio.PlayOneShot(hitSound);
        if (collision.gameObject.name == "Pemukul2" || collision.gameObject.name == "Pemukul1")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.y > 0)
        {
            scoreP2 += 1;
            initialDirection = Vector2.up;
            player2Score_TL.text = (int.Parse(player2Score_TL.text) + 1).ToString();
            player2Score_BR.text = (int.Parse(player2Score_BR.text) + 1).ToString();
            if (scoreP2 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player <color=blue>Biru</color> Pemenang!";
                Destroy(gameObject);
                return;
            }

            ResetBall();
        }
        if (transform.position.y < 0)
        {
            scoreP1 += 1;
            initialDirection = Vector2.down;
            player1Score_TL.text = (int.Parse(player1Score_TL.text) + 1).ToString();
            player1Score_BR.text = (int.Parse(player1Score_BR.text) + 1).ToString();
            if (scoreP1 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player <color=red>Merah</color> Pemenang!";
                Destroy(gameObject);
                return;
            }
            ResetBall();
        }
    }
}
