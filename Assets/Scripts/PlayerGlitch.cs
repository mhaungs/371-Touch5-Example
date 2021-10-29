using System.Collections;
using UnityEngine;

public class PlayerGlitch : MonoBehaviour
{
    public float glitchChance = 0.1f;

    Renderer _holoRenderer;
    readonly WaitForSeconds glitchLoopWait = new WaitForSeconds(0.1f);
    WaitForSeconds _glitchDuration = new WaitForSeconds(0.1f);

    void Awake()
    {
        _holoRenderer = GetComponent<Renderer>();
    }

    IEnumerator Start()
    {
        while(true)
        {
            float glitchTest = Random.Range(0f, 1f);
            if(glitchTest <= glitchChance)
            {
                StartCoroutine(Glitch());
            }
            yield return glitchLoopWait;
        }
    }

    IEnumerator Glitch()
    {
        _glitchDuration = new WaitForSeconds(Random.Range(0.05f, 0.25f));
        _holoRenderer.material.SetFloat("_Amount", 1f);
        _holoRenderer.material.SetFloat("_CutoutThresh", 0.094f);
        _holoRenderer.material.SetFloat("_Amplitude", Random.Range(100, 250));
        _holoRenderer.material.SetFloat("_Speed", Random.Range(1, 10));
        yield return _glitchDuration;
        _holoRenderer.material.SetFloat("_Amount", 0f);
        _holoRenderer.material.SetFloat("_CutoutThresh", 0f);
    }
}
