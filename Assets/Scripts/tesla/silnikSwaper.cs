using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class silnikSwaper : MonoBehaviour
{
    public GameObject empty;
    public GameObject fullopen;
    public GameObject tuba;
    public GameObject kuko;
    public GameObject cewa;
    public GameObject fullclosed;
    public AudioSource click;
    public AudioSource done;

    public void ZmianaBanana(int numer)
    {
        switch (numer)
        {
            case 1:
            case 2:
            case 3:
                click.Play(0);
                break;
        }

        switch (numer)
        {
            case 1:
                empty.SetActive(false);
                tuba.SetActive(true);
                break;
            case 2:
                tuba.SetActive(false);
                kuko.SetActive(true);
                break;
            case 3:
                kuko.SetActive(false);
                cewa.SetActive(true);
                break;
            case 4:
                cewa.SetActive(false);
                fullopen.SetActive(true);
                StartCoroutine(EndThisShitAfterDelay());
                break;
            default:
                empty.SetActive(true);
                break;

        }
    }

    IEnumerator EndThisShitAfterDelay()
    {
        done.Play(0);

        yield return new WaitForSeconds(1);

        FindObjectOfType<TeslaManager>().Endthisshit();
    }
}
