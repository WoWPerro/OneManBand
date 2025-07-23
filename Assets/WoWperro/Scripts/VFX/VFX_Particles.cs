using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Particles : MonoBehaviour
{
     public GameObject prefab;
     public GameObject particleCorrect;
     public GameObject particleHit;
     public GameObject particlePerfect;
     public GameObject particleMiss;
    private List<GameObject> ParticleList;
    // Start is called before the first frame update
    void Start()
    {
        ParticleList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    public void play(Vector3 vec)
    {
        if(ParticleList.Count > 0)
        {
            int index = 0;
            bool found = false;
            for(int i = 0; i < ParticleList.Count; i++)
            {
                if(!ParticleList[i].activeInHierarchy)
                {
                    found = true;
                    index = i;
                    break;
                }
            }
            if(found)
            {
                ParticleList[index].transform.position = vec;
                ParticleList[index].SetActive(true);
            }
            else
            {
                ParticleList.Add(Instantiate(prefab, vec, Quaternion.identity));
            }
        }
        else
        {
            ParticleList.Add(Instantiate(prefab, vec, Quaternion.identity));
        }
    }

    void CheckInput()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Vector3 touchPos = new Vector2(wp.x, wp.y);
            Instantiate(prefab, touchPos, Quaternion.identity);
        }


        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 touchPos = new Vector2(wp.x, wp.y);
            Instantiate(prefab, touchPos, Quaternion.identity);
        }
        #endif
    }

    public void CreateCorrectParticle(Vector3 pos)
    {
        Instantiate(particleCorrect, pos, Quaternion.identity);
    }
    
    public void CreateMissParticle(Vector3 pos)
    {
        Instantiate(particleMiss, pos, Quaternion.identity);
    }
    
    public void CreatePerfectParticle(Vector3 pos)
    {
        Instantiate(particlePerfect, pos, Quaternion.identity);
    }
    
    public void CreateHitParticle(Vector3 pos)
    {
        Instantiate(particleHit, pos, Quaternion.identity);
    }
}
