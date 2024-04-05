using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PemukulController : MonoBehaviour
{
    public float kecepatan;
    public string axis;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        float gerak = Input.GetAxis(axis) * kecepatan;
        rb.velocity = new Vector2(gerak, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Basic Border")
        {
            rb.velocity = Vector2.zero;
        }
    }
}
