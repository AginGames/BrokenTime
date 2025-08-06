using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool oppenheimer = false;
    public int maxHealth = 3000;
    public GameObject blink;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public Image sliderImage;
    public AudioSource damageAudio;
    private Coroutine heal;
    public int health = 3000;
    // Start is called before the first frame update
    private void Awake()
    {
        health = maxHealth;
    }
    void Start()
    {
        healthText.text = health.ToString();
        if (!oppenheimer)StartHeal();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveHealth(500);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            StartHeal();
        }*/
    }

    public void SetHealth(int h, bool isDamage)
    {
        health = h;
        healthText.SetText(h.ToString());
        healthSlider.value = h;

        float healthPercentage = (float)h / maxHealth * 100f;

        Debug.Log("MA " + healthPercentage + "% HP");

        if (isDamage && !oppenheimer) damageAudio.Play();

        if (healthPercentage > 80)
        {
            sliderImage.color = new Color32(43, 221, 102, 255);
        }
        else if (healthPercentage > 50)
        {
            sliderImage.color = new Color32(255, 171, 9, 255);
        }
        else
        {
            sliderImage.color = new Color32(242, 22, 22, 255);
        }
    }

    void StartHeal()
    {
        heal = StartCoroutine(Heal());
    }

    void ResetHeal()
    {
        if (heal != null) StopCoroutine(heal);
        StartHeal();
    }

    private IEnumerator Heal()
    {
        yield return new WaitForSeconds(15f);

        if (health + 500 > maxHealth)
        {
            SetHealth(maxHealth, false);
        }
        else
        {
            SetHealth(health + 500, false);
        }

        Debug.Log("MA TYLE HP:" + health);

        StartHeal();
    }

    public void RemoveHealth(int h)
    {
        SetHealth(health - h, true);

        Debug.Log("MA TYLE HP:" + health);

        StartCoroutine(RemoveHealthCoroutine(h));

        if(!oppenheimer)ResetHeal();
    }

    IEnumerator RemoveHealthCoroutine(int h)
    {
        Debug.Log("MA TYLE HP:" + health);

        if (!oppenheimer)blink.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if(!oppenheimer)blink.SetActive(false);
    }
}
