using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifesUI : MonoBehaviour
{
    [SerializeField] Image goldImage;
    [SerializeField] Image goldGlow;
    [SerializeField] ParticleSystem goldParticles;

    public IEnumerator LooseLife()
    {
        goldImage.enabled = false;
        goldGlow.GetComponentInChildren<Image>().enabled = false;
        goldParticles.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        goldImage.enabled = true;
        goldGlow.GetComponentInChildren<Image>().enabled = true;
        goldParticles.gameObject.SetActive(false);

        goldImage.gameObject.SetActive(false);
    }

    public IEnumerator WinLife()
    {
        goldImage.enabled = false;
        goldGlow.GetComponentInChildren<Image>().enabled = false;
        goldImage.gameObject.SetActive(true);
        goldParticles.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        goldImage.enabled = true;
        goldGlow.GetComponentInChildren<Image>().enabled = true;

        yield return new WaitForSeconds(0.45f);
        goldParticles.gameObject.SetActive(false);
    }

}
