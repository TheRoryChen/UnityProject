using UnityEngine;

public class HurtFlash: MonoBehaviour {
    public Health hp;
    public SpriteRenderer sr;
    Color original;
    void Start() {
        original = sr.color;
        hp.onChange += () => StartCoroutine(Flash());
    }
    System.Collections.IEnumerator Flash() {
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = original;
    }
}