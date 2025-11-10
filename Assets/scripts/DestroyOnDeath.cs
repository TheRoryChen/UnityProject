using UnityEngine;

public class DestroyOnDeath: MonoBehaviour {
    public Health hp;
    void Awake(){ if (!hp) hp = GetComponent<Health>(); }
    void Start(){ if (hp) hp.onDeath += () => Destroy(gameObject); }
}