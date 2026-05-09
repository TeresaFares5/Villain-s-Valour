using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeProjectile : MonoBehaviour
{
    Vector3 direction;

    [SerializeField] float speed;
    public int damage = 5;

    float timeToLeave = 6f;

    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);
        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }

    bool hitDetected = false;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Time.frameCount % 6 == 0)
        {


            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.7f);
            foreach (Collider2D c in hit)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hitDetected = true;
                    PostDamage(damage,transform.position);
                    break;
                }
            }
            if (hitDetected == true)
            {
                if (gameObject.name != "SpinSkull(Clone)")
                {
                    Destroy(gameObject);
                }
            }
        }

        timeToLeave -= Time.deltaTime;
        if (timeToLeave < 0f)
        {
            if(gameObject.name != "SpinSkull(Clone)")
            {
                Destroy(gameObject);

            }
        }
    }

    public void PostDamage(int damage, Vector3 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition); 
    }

}
