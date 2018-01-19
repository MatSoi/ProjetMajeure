using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    private float speed;
    private Vector3 direction;
    private float fadeTime;
    public AnimationClip critAnim;
    private bool crit;

    // Called once per frame
    private void Update()
    {
        if (!crit)
        {
            float translation = speed * Time.deltaTime;
            transform.Translate(direction * translation);
        }

        transform.LookAt(2 * transform.position - FloatingTextController.Instance.player.transform.position);
        
    }

    public void Initialize(float speed, Vector3 direction, float fadeTime, bool crit)
    {

        this.speed = speed;
        this.direction = direction;
        this.fadeTime = fadeTime;
        this.crit = crit;
        if (crit)
        {
            GetComponent<Animator>().SetTrigger("Critical");
            StartCoroutine(Critical());
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }
    private IEnumerator Critical()
    {
        yield return new WaitForSeconds(critAnim.length);
        crit = false;
        StartCoroutine(FadeOut());

    }
    private IEnumerator FadeOut()
    {
        float startAlpha = GetComponent<Text>().color.a;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;
        while (progress < 1.0)
        {
            Color tmpColor = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));
            progress += rate * Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
