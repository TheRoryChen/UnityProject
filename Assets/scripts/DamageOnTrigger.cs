using UnityEngine;

public class DamageOnTrigger: MonoBehaviour {
    public int damage = 10; public string targetTag = "Enemy";
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag(targetTag)) other.GetComponent<Health>()?.Damage(damage);
    }
}
