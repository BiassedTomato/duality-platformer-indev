using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator)), RequireComponent(typeof(InventoryHandler))]


public class PlayerControl : MonoBehaviour
{
    public Tilemap LightPlatform, LightWall, DarkPlatfrom, DarkWall;


    public Queue<Transform> BulletPool = new Queue<Transform>();


    Animator BaseCharAnimator, NegCharAnimator;
    Animator eyenimator;


    InventoryHandler playerInventory;


    public Transform MovementBreak;
    public Transform LightSource;
    public Transform SecondSkin;
    public Transform BulletBag;
    public Transform Menu;
    public Transform Eye;


    public Transform TextPanel;
    public Transform Unhider;
    public Text DisplayText;


    float directionflag;
    public float facing = 1f;
    float dir = 1f;
    float force = 45;
    float movedir;

    bool lightIsOn = false;
    bool isDashing = false;
    bool isClosed = false;
    bool isAiring = false;
    bool isJumping = false;
    public bool isTouchingWall = false;


    Collider2D BreakCollider;
    Collider2D PlatformCollider;
    Rigidbody2D RBody;
    GameObject bul;


    private void FixedUpdate()
    {
        dir = Input.GetAxisRaw("Horizontal");

        movedir = Input.GetAxis("Horizontal");


        if ((dir == -1 || dir == 1) && !isAiring)
        {
            facing = dir;

            transform.localScale = new Vector3(dir, 1, 1);

            BaseCharAnimator.SetTrigger("MoveTrigger");
            BaseCharAnimator.ResetTrigger("StandTrigger");

            NegCharAnimator.SetTrigger("MoveTrigger");
            NegCharAnimator.ResetTrigger("StandTrigger");
        }
        else
        {
            BaseCharAnimator.ResetTrigger("MoveTrigger");
            BaseCharAnimator.SetTrigger("StandTrigger");

            NegCharAnimator.ResetTrigger("MoveTrigger");
            NegCharAnimator.SetTrigger("StandTrigger");
        }

        RBody.velocity = new Vector2(movedir * 10 * (!isAiring ? 1 : 0), RBody.velocity.y);
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int flag = (Menu.localScale.x == 1) ? 0 : 1;

            Menu.localScale = new Vector2(flag, flag);
        }


        if (transform.position.y <= -60)
            transform.position = new Vector2(-13, 30);

        if (Input.GetKeyDown(KeyCode.E) && !isAiring)
        {
            RaycastHit2D[] HitObjects = Physics2D.RaycastAll(transform.position, new Vector2(facing, 0), 4);

            foreach (RaycastHit2D hit in HitObjects)
            {
                if (hit.transform.tag == "Terrain")
                {
                    StartCoroutine(WriteText(hit.transform.GetComponent<Sign>().OnUse()));
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (!lightIsOn)
            {
                StartCoroutine("Pulse");

                lightIsOn = true;
            }
            else
            {
                StopCoroutine("Pulse");

                lightIsOn = false;

                LightSource.localScale = Vector3.zero;
            }
        }
    }
    private void Awake()
    {
        PlatformCollider = LightPlatform.GetComponent<CompositeCollider2D>();

        Menu.localScale = Vector2.zero;

        LightPlatform.GetComponent<TilemapRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        TextPanel.localScale = Vector2.zero;

        eyenimator = Eye.GetComponent<Animator>();
        eyenimator.SetBool("IsClosed", isClosed);

        Unhider.transform.parent = transform;
        Unhider.localScale = Vector2.zero;
        Unhider.gameObject.SetActive(false);

        bul = Resources.Load("Prefabs/bullet") as GameObject;

        playerInventory = GetComponent<InventoryHandler>();

        for (int i = 1; i <= 50; i++)
        {
            Transform instantiatedBul = Instantiate(bul, transform.position, Quaternion.identity).transform;

            BulletPool.Enqueue(instantiatedBul);

            instantiatedBul.parent = BulletBag.transform;
            instantiatedBul.localPosition = Vector3.zero;
            instantiatedBul.gameObject.SetActive(false);
        }
        SecondSkin = transform.GetChild(0).transform;

        BaseCharAnimator = GetComponent<Animator>();

        NegCharAnimator = SecondSkin.GetComponent<Animator>();

        RBody = GetComponent<Rigidbody2D>();
        RBody.gravityScale = 10;

        // StartCoroutines
        StartCoroutine("Shoot");
        StartCoroutine("Jump");
        StartCoroutine("UnhiderTurnOn");
    }


    public void Return(Transform bullet)
    {
        StartCoroutine(ReturnBullet(bullet));
    }//вернуть пулю в очередь
    public void TurnOffUnhider()
    {
        StopCoroutine("UnhiderTurnOff");
        StartCoroutine("UnhiderTurnOff");
    }//метод выключения


    IEnumerator ReturnBullet(Transform bullet)
    {
        yield return new WaitForSeconds(4f);

        BulletPool.Enqueue(bullet);

        bullet.parent = BulletBag.transform;
        bullet.localPosition = Vector3.zero;
        bullet.gameObject.SetActive(false);
    }//корутина возврата
    IEnumerator WriteText(string[] text)
    {
        isAiring = true;

        DisplayText.text = "";

        TextPanel.localScale = new Vector2(1, 1);

        foreach (string sentence in text)
        {
            for (int i = 0; i < sentence.Length; i++)
            {
                DisplayText.text += sentence[i];

                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForEndOfFrame();

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

            DisplayText.text = "";
        }
        int flag = (TextPanel.localScale.x == 1) ? 0 : 1;
        TextPanel.localScale = new Vector2(flag, flag);

        isAiring = false;
    }//вывести массив фраз
    IEnumerator WriteText(string text)
    {
        TextPanel.gameObject.SetActive(true);

        DisplayText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            DisplayText.text += text[i];

            yield return new WaitForSeconds(0.03f);
        }
    }//выывести одну фразу
    IEnumerator Shoot()
    {
        for (; ; )
        {
            if (Input.GetButtonDown("Fire1"))
            {
                playerInventory.Inventory[0].Use();

                yield return new WaitForSeconds(0.3f);
            }
            yield return 0f;
        }
    }//использовать первичное оружие
    IEnumerator UnhiderTurnOn()
    {
        for (; ; )
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                playerInventory.Inventory[1].Use();

                eyenimator.SetBool("IsClosed", true);

                yield return new WaitForSeconds(1f);
            }
            yield return 0f;
        }
    }//включить анхайдер
    IEnumerator UnhiderTurnOff()
    {
        yield return new WaitForSeconds(0.5f);

        Unhider.gameObject.SetActive(false);
        Unhider.parent = transform;
        Unhider.transform.position = transform.position;
        Unhider.localScale = Vector3.zero;

        eyenimator.SetBool("IsClosed", false);
    }//выкл анхайдер
    IEnumerator Pulse()
    {
        for (; ; )
        {
            LightSource.localScale = new Vector2(Random.Range(7f, 13f), Random.Range(8f, 12f));

            yield return new WaitForSeconds(0.1f);

            yield return 0f;
        }
    }//мигание света
    IEnumerator Jump()
    {
        for (; ; )
        {
            if (!isJumping && Input.GetButtonDown("Jump") && !isDashing)
                RBody.velocity = new Vector2(RBody.velocity.x, 0);

            if (!isJumping && Input.GetButton("Jump") && !isDashing)
            {

                RBody.AddForce(Vector2.up * force * (!isAiring ? 1 : 0), ForceMode2D.Impulse);
                force = force / 1.5f;

                if (RBody.velocity.y <= 0)
                    isJumping = false;


            }

            if (Input.GetButtonUp("Jump") && !isDashing)
            {
                force = 45;
                isJumping = false;
            }

            yield return 0f;
        }
    }//прыжок

    IEnumerator FadeJump()
    {
        yield return 0f;
    }
}