using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    Rigidbody2D rb;

    public AsteroidManager am;
    public ParticleSystem pe;

    float hor;
    float vert;

    float cooldown;
    float invisibleTimer;

    public AudioSource bang;
    public AudioSource thrust;

    public Transform gun;
    public GameObject bullet;

    public PolygonCollider2D Ship_collider;

    void Start()
    {
        cooldown = 0.0f;
        rb = GetComponent<Rigidbody2D>();
        Ship_collider = GetComponent<PolygonCollider2D>();
    }
	
	void Update()
	{
        bang.enabled = true;

        invisibleTimer -= Time.deltaTime;
        cooldown -= Time.deltaTime;

        if(invisibleTimer <= 0 && !Ship_collider.enabled)
        {
            Ship_collider.enabled = true;
        }

        hor = Input.GetAxis("Horizontal");
        vert = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);

        Thrust();
        _Rotation();
        Fire();

        if(vert > 0)
        {
            pe.Emit(1);
        }
	}

    void Thrust()
    {
        thrust.enabled = true;
        thrust.Play();
        rb.AddForce(transform.up * vert * Time.deltaTime * 300.0f);
    }

    void _Rotation()
    {
        transform.Rotate(0, 0, -hor * Time.deltaTime * 100);
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && cooldown <= 0)
        {
            Instantiate(bullet, gun.position, transform.rotation);
            cooldown = 0.5f;
        }
    }

    public void Die()
    {
        if(am.lives > 0)
        {
            am.lives--;
            transform.position = Vector3.zero;
            Ship_collider.enabled = false;
            invisibleTimer = 2.0f;
        }
        else
        {
            Destroy(gameObject);
        }
            
    }
}
