using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
public class SpiritCenter : MonoBehaviour {

	// Use this for initialization
    [SerializeField] PlatformerCharacter2D m_character;
    int Spirit;
    Transform center;
    bool shoot;
    Vector3[] tangant;
    float shootCount;

    void Start () {
        center = m_character.transform.Find("Center");
        tangant = new Vector3[8];
	}
	
	// Update is called once per frame
	void Update () {
	     center = m_character.transform.Find("Center");
         transform.position = center.transform.position - new Vector3(0,1.0f,0);
	}

    void FixedUpdate()
    {
        Spirit = m_character.GetSpirit();
        if (!shoot)
        {

            if (Spirit >= 1)
                transform.Find("Spirit1").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 1)
                transform.Find("Spirit1").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 2)
                transform.Find("Spirit2").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 2)
                transform.Find("Spirit2").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 3)
                transform.Find("Spirit3").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 3)
                transform.Find("Spirit3").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 4)
                transform.Find("Spirit4").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 4)
                transform.Find("Spirit4").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 5)
                transform.Find("Spirit5").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 5)
                transform.Find("Spirit5").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 6)
                transform.Find("Spirit6").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 6)
                transform.Find("Spirit6").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 7)
                transform.Find("Spirit7").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 7)
                transform.Find("Spirit7").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 8)
                transform.Find("Spirit8").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 8)
                transform.Find("Spirit8").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            transform.Find("Spirit1").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit2").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit3").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit4").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit5").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit6").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit7").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit8").transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 180.0f);
            transform.Find("Spirit1").transform.rotation = Quaternion.identity;
            transform.Find("Spirit2").transform.rotation = Quaternion.identity;
            transform.Find("Spirit3").transform.rotation = Quaternion.identity;
            transform.Find("Spirit4").transform.rotation = Quaternion.identity;
            transform.Find("Spirit5").transform.rotation = Quaternion.identity;
            transform.Find("Spirit6").transform.rotation = Quaternion.identity;
            transform.Find("Spirit7").transform.rotation = Quaternion.identity;
            transform.Find("Spirit8").transform.rotation = Quaternion.identity;
            //transform.Rotate(new Vector3(0,0,1), Time.deltaTime * 180, Space.World);
        }
        else if (shoot)
        {
            for (int i = 1; i <= 8; i++)
            {
                transform.Find("Spirit" + i).position += 7.5f*tangant[i-1] * Time.fixedDeltaTime;
                Color c;
                c =transform.Find("Spirit" + i).gameObject.GetComponent<SpriteRenderer>().color;
                c.a = (1.5f - shootCount) / 1.5f;
                transform.Find("Spirit" + i).gameObject.GetComponent<SpriteRenderer>().color = c;
            }



            shootCount += Time.fixedDeltaTime;
            if (shootCount >= 1.5f)
            {
                
                 for (int i = 1; i <= 8; i++)
                 {
                     Destroy(transform.Find("Spirit" + i).gameObject);
                 }

                shootCount = 0;
                shoot = false;
                m_character.SetSpirit(0);
            }
        }
    }

    public void ShootSpirit()
    {
       
        for (int i = 1; i <= 8; i++)
        {
            Transform t = transform.Find("Spirit" + i);
            Debug.Log(t.position.x + " " + t.position.y + " " + t.position.z);
            tangant[i - 1] = Vector3.Cross(new Vector3(0, 0, 1), (t.position - transform.position).normalized);
            
        }
        shoot = true;
    }
}
