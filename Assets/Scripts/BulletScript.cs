using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
    Vector3 InitSpeed;
    [SerializeField] float lifeTime = 1.0f;
    float timeCount;
    [SerializeField] bool isBlack;
    bool hit;
    

	void Start () {
        timeCount = 0;
        lifeTime = 1.0f;
        hit = false;
	}

    public void SetIsBlack(bool Black)
    {
        Debug.Log("qaq" + Black);
        this.isBlack = Black;
    }

	// Update is called once per frame
	void Update () {
        timeCount += Time.deltaTime;
       
        if(timeCount >= lifeTime/2.0f){
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = 2.0f*(lifeTime-timeCount)/(lifeTime);
            GetComponent<SpriteRenderer>().color = c;
        }

        if (timeCount >= lifeTime&&!hit)
        {
            Destroy(gameObject);
        }
	}

    public void GoDestroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.isTrigger)
            return;
        GameObject otherOBJ = other.gameObject;

        if (otherOBJ.tag == "Monster")
        {
            if (isBlack)
                BlackAttack(otherOBJ);
            else if (!isBlack)
                WhiteAttack(otherOBJ);
        }

        // ADD SPECIAL EFFECT
        Debug.Log("qqqqq");
        GetComponent<Animator>().SetTrigger("goExplode");
        GetComponent<Rigidbody2D>().Sleep();
        hit = true;
            
    }

    void BlackAttack(GameObject monster)
    {
        if (monster.GetComponent<Identity>().IsBlack())
        {
            monster.GetComponent<health>().hp += 1; ;
            Debug.Log(" ++ B ");
        }
        else
        {
            monster.GetComponent<health>().hp -= 1;
            Debug.Log(" -- B");
        }

    }

    void WhiteAttack(GameObject monster)
    {
        if (!monster.GetComponent<Identity>().IsBlack())
        {
            monster.GetComponent<health>().hp += 1; ;
            Debug.Log(" ++ W ");
        }
        else
        {
            monster.GetComponent<health>().hp -= 1; ;
            Debug.Log(" -- W ");
        }
    }
}
