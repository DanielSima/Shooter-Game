using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    //!!!!reset the component in inspector when changing those values!!!!
    public void Reset()
    {
        damage = 10f;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Collider>().enabled = false;
        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosion.GetComponent<Collider>().enabled = false;
        explosion.transform.position = collision.contacts[0].point;
        explosion.transform.localScale = new Vector3(3, 3, 3);

        //list of every character
        List<GameObject> characters = new List<GameObject>();
        characters.Add(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("character"))
        {
            characters.Add(g);
        }

        //do damage to every character in radius
        foreach (GameObject g in characters)
        {
            if (Vector3.Distance(collision.contacts[0].point, g.transform.position) < 3f)
            {
                DoDamage(g.gameObject.GetComponent<Character>(), damage);
            }
        }
        StartCoroutine(DestroyExplosion(explosion));
    }

    protected override void DoDamage(Character character, float damage)
    {
        character.ChangeHealth(-damage);
    }

    public IEnumerator DestroyExplosion(GameObject explosion)
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        Destroy(explosion);
        Destroy(this);
    }
}