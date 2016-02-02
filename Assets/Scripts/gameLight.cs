using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameLight : MonoBehaviour {

    public GameObject lightProbe;
    public bool enableLight = true;

	public float lightWidth;
	public Vector3 lightOriginRight;
	public Vector3 lightOriginLeft;
    public float maxDistance = 50.0f;
    public float lightRotateDegree;
    public Vector3 lightDirection;
	
	public bool useWidth = true;

    public Material lightMaterial;
    public List<Vector3> verticeList;

    private Mesh lightMesh;
    private PolygonCollider2D lightCollider;
    private Rigidbody2D lightRigid;

    private Reflector previousHitReflector;
    
    public void ChangeLightDirection(Vector3 direction)
    {
        float angle = Vector3.Angle(lightDirection, direction);
        lightDirection = direction.normalized;
        lightRotateDegree -= angle;
    }

    public void ChangeLightDirection(float degree)
    {
        lightRotateDegree = degree;
        lightRotateDegree = Mathf.Clamp(lightRotateDegree, -360.0f, 360.0f);
        lightDirection.x = Mathf.Cos(lightRotateDegree);
        lightDirection.y = Mathf.Sin(lightRotateDegree);
        lightDirection.Normalize();

    }

    public void RotateLightDirection(float degree)
    {
        lightRotateDegree += degree;
        lightRotateDegree = Mathf.Clamp(lightRotateDegree, -360.0f, 360.0f);
        lightDirection.x = Mathf.Cos(lightRotateDegree);
        lightDirection.y = Mathf.Sin(lightRotateDegree);
        lightDirection.Normalize();
    }

    public void SetEnable(bool set)
    {
        enableLight = set;
        if(!enableLight)
        {
            verticeList.Clear();
            renderLightMesh();
            createCollider();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("light hit!");
    }

	// Use this for initialization
	void Awake () {
        Vector3 temp = lightDirection;
        lightDirection = new Vector3(1, 0, 0);
        lightRotateDegree = 0.0f;
        ChangeLightDirection(temp);

        lightProbe = transform.FindChild("light probe").gameObject;
        lightProbe.transform.position = new Vector3(lightProbe.transform.position.x, lightProbe.transform.position.y, -1);
        MeshFilter lightMeshFilter = (MeshFilter)lightProbe.AddComponent(typeof(MeshFilter));
        MeshRenderer lightRenderer = lightProbe.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        lightRenderer.sharedMaterial = lightMaterial;
        lightMesh = new Mesh();
        lightMeshFilter.mesh = lightMesh;
        lightMesh.name = "lightmesh";
        lightMesh.MarkDynamic();

        lightCollider = lightProbe.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
        lightCollider.isTrigger = true;
        lightRigid = lightProbe.AddComponent<Rigidbody2D>() as Rigidbody2D;
        lightRigid.isKinematic = true;
        verticeList = new List<Vector3>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //RotateLightDirection(1.0f * Time.deltaTime);

        if (!enableLight)
        {
            return;
        }
        createVertciesList();
        renderLightMesh();
        createCollider();
    }


    void createCollider()
    {
        List<Vector2> vector2List = new List<Vector2>();
        foreach(Vector3 vec in verticeList)
        {
            vector2List.Add(new Vector2(vec.x, vec.y));
        }
        lightCollider.points = vector2List.ToArray();
    }


    void createVertciesList()
    {
        verticeList.Clear();
        Vector3 rayDirection = lightDirection.normalized;
		Vector3 topVertexVector = new Vector3();
		Vector3 clampedTopVector = new Vector3();
        if (useWidth)
        {
            topVertexVector = Vector3.Cross(rayDirection, new Vector3(0, 0, 1.0f)).normalized;
            clampedTopVector = Vector3.ClampMagnitude(topVertexVector * lightWidth, lightWidth / 2.0f);
            verticeList.Add(-clampedTopVector);
            verticeList.Add(clampedTopVector);
        }
        else
        {
            clampedTopVector = (lightOriginRight - lightOriginLeft)/2.0f;
            //Debug.Log("length of width:"  + (clampedTopVector*2).magnitude);
            verticeList.Add(lightOriginLeft);
            verticeList.Add(lightOriginRight);
        }

        Vector2 originLeft = new Vector2(verticeList[0].x + lightProbe.transform.position.x, verticeList[0].y + lightProbe.transform.position.y);
        Vector2 originRight = new Vector2(verticeList[1].x + lightProbe.transform.position.x, verticeList[1].y + lightProbe.transform.position.y);
        //List<RaycastHit2D> lightRays;
        LayerMask layer = 1 << LayerMask.NameToLayer("Block Light");

        List<RaycastHit2D> rays = new List<RaycastHit2D>();
        List<RaycastHit2D> hitReflectorRecord = new List<RaycastHit2D>();
        int rayCount = 0;
        if(useWidth)
        {
            rayCount = (int)(lightWidth / 0.1f) + 2;
        }
        else
        {
            rayCount = (int)((clampedTopVector * 2).magnitude / 0.1f) + 2;
        }
        Vector2 offsetVec = new Vector2(0, 0);
        Vector2 increVec = new Vector2(Vector3.ClampMagnitude(clampedTopVector, 0.1f).x, Vector3.ClampMagnitude(clampedTopVector, 0.1f).y);
        Vector3 clampedRayDirection = Vector3.ClampMagnitude(rayDirection * maxDistance, maxDistance);
        //Debug.Log("rayCount: " + rayCount);
        for (int i = 0;i<rayCount;i++)
        {
            Vector3 origin = originRight - offsetVec;
            RaycastHit2D rayEdge = Physics2D.Raycast(origin, rayDirection, maxDistance, layer);
            rays.Add(rayEdge);
            if (rayEdge.collider != null && rayEdge.collider.gameObject != this.gameObject)
            {
                Vector3 hitPoint = new Vector3(rayEdge.point.x, rayEdge.point.y, 0);
                verticeList.Add(hitPoint - lightProbe.transform.position);
                //ray hit reflector
                if(rayEdge.collider.gameObject.GetComponent<Reflector>() != null && rayEdge.collider.gameObject.GetComponent<Reflector>().enableReflect)
                {
                    //if there is no previous recorded hitting ray
                    if(hitReflectorRecord.Count == 0)
					{
                        hitReflectorRecord.Add(rayEdge);
					}
                    //there is one and the same
					else if (hitReflectorRecord.Count != 0 && hitReflectorRecord[0].collider.gameObject == rayEdge.collider.gameObject)
                    {
                        hitReflectorRecord.Add(rayEdge);
                    }
                }

                if (hitReflectorRecord.Count >= 2)
                {
                    Reflector hitReflector = hitReflectorRecord[0].collider.gameObject.GetComponent<Reflector>();
                    previousHitReflector = hitReflector;
                    hitReflector.TriggerReflect(this, hitReflectorRecord[0].point, hitReflectorRecord[hitReflectorRecord.Count - 1].point, hitReflectorRecord[0].normal);
                }
            }
            else verticeList.Add(new Vector3(origin.x, origin.y, -1) - lightProbe.transform.position + clampedRayDirection);
            offsetVec += increVec;
        }
        //not mirror hit
        if(hitReflectorRecord.Count == 0 && previousHitReflector!=null)
        {
            previousHitReflector.LeaveReflect();
            previousHitReflector = null;
        }
    }


    void renderLightMesh()
    {
        //-- Step 5: fill the mesh with vertices--//
        //---------------------------------------------------------------------//
        if (lightMesh == null) return;
        lightMesh.Clear();
        lightMesh.vertices = verticeList.ToArray();

        Vector2[] uvs = new Vector2[verticeList.Count];
		for (int i = 0; i < verticeList.Count; i++) {
            if (i == 0) uvs[i] = new Vector2(1, 1);
            else if (i == 1) uvs[i] = new Vector2(0, 1);
            else if (i == verticeList.Count - 1) uvs[i] = new Vector2(1, 0);
            else uvs[i] = new Vector2(0, 0);		
		}
        lightMesh.uv = uvs;


        //arragne triangle
        int numOfTriangles = verticeList.Count - 2;
        int sizeOfTriangles = numOfTriangles * 3;

        Triangulator tri = new Triangulator(verticeList.ToArray());
        //int[] triangles = new int[sizeOfTriangles];
        int[] triangles = tri.Triangulate();
        /*
        //int[] triangles = new int[6] {0, 1, 2, 2, 3 ,0};
        int nextIndex = 0;
        for (int i = 0; i< numOfTriangles;i+=3)
        {
            triangles[i] = 0;
            triangles[i + 1] = nextIndex + 2;
            triangles[i + 2] = nextIndex + 1;
            nextIndex++; 
        }*/

        lightMesh.triangles = triangles;
        //lightMesh.RecalculateNormals();
        lightProbe.GetComponent<Renderer>().sharedMaterial = lightMaterial;
    }


}
