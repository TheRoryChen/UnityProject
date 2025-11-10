using UnityEngine;

public class EnemyMob: MonoBehaviour {
    public Transform target; public float speed=2.5f; public int touchDamage=10;
    Rigidbody2D rb; Health hp;
    void Awake(){ rb=GetComponent<Rigidbody2D>(); hp=GetComponent<Health>(); }
    void Update(){
        if (hp==null) return;
        float dir = Mathf.Sign(target.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(dir*speed, rb.linearVelocity.y);
        transform.localScale = new Vector3(dir,1,1);
    }
    void OnCollisionEnter2D(Collision2D c){
        if (c.collider.CompareTag("Player")) c.collider.GetComponent<Health>()?.Damage(touchDamage);
    }
}