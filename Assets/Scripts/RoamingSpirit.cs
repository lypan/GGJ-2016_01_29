using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class RoamingSpirit : MonoBehaviour {

	// Use this for initialization
    [SerializeField] Vector3 start;
    [SerializeField] Vector3 end;
    [SerializeField] float speed;
    Vector2 tempEnd;
    Vector2 tempStart;
    float startRatio;
    float endRatio;
    bool moving;
    float randomWait;
    float timeCount;
    float startTime;
    float length;

	void Start () {
        startRatio = 0;
        timeCount = 0;
        endRatio = ((float)Random.Range(0,11))/10.0f;
        randomWait =((float) Random.Range(50, 250)) / 100.0f;
        moving = false;
        speed = 3.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!moving)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= randomWait)
            {
                timeCount = 0;
                randomWait = ((float)Random.Range(50, 250)) / 100.0f;
                startTime = Time.time;
                moving = true;
                float x = (float)(Random.Range(0, 100)) / 100.0f;
                tempStart = transform.position;
                tempEnd = Vector3.Lerp(start, end, x);
                length = Vector3.Distance(tempStart, tempEnd);
            }

        }
        else if (moving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / length;
            transform.position = Vector3.Lerp(tempStart,tempEnd, fracJourney);
            if (Vector3.Distance(transform.position, tempEnd) <= 0.01f)
            {
                moving = false;
            }

        }
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        GameObject other = c.gameObject;
        //Debug.Log("SPIRIT++");
        if (other.tag == "Player" && !other.GetComponent<PlatformerCharacter2D>().onDamage)
        {
            Debug.Log("SPIRIT++");
            other.GetComponent<PlatformerCharacter2D>().SetSpirit( other.GetComponent<PlatformerCharacter2D>().GetSpirit() + 1 );
            Destroy(gameObject);
        }

    }
}
