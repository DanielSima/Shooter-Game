using UnityEngine;

/// <summary>
/// base class for all bullets.
/// </summary>
public class Bullet : MonoBehaviour
{
    //to be changed for every bullet
    public float damage = 0;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //do damage when colliding with player
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "character")
        {
            DoDamage(collision.gameObject.GetComponent<Character>(), damage);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void DoDamage(Character character, float damage)
    {
        character.ChangeHealth(-damage);
        Destroy(this.gameObject);
    }
}