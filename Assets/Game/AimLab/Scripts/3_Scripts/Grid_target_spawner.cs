using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_target_spawner : MonoBehaviour
{
    public GameObject targetObject1;
    Transform p1,p2,p3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject t=Object.Instantiate(targetObject1,new Vector3(0, 0, 10), Quaternion.identity);
        p1 = t.transform;
        spawningTarget(t.transform);
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Transform spawningTarget(Transform target)
    {
        int pos = Random.Range(1, 8);
        Vector3 position = getNewPosition(target, pos);
        if(p2==null && p3 == null)
        {
            p2 = GameObject.Instantiate(targetObject1, position, Quaternion.identity).transform;
            while (position == p2.position)
            {
                position = getNewPosition(target, Random.Range(1, 8));
            }
            p3= GameObject.Instantiate(targetObject1, position, Quaternion.identity).transform;
            return null;
        }
        while(position==p1.position || position==p2.position || position == p3.position || position.y>8 || position.y<-8 || position.x>4 || position.x<-4)
        {
            position = getNewPosition(target, Random.Range(1, 8));
        }
        GameObject newTarget = GameObject.Instantiate(targetObject1, position, Quaternion.identity);
        
        return newTarget.transform;
    }

    public void destroyTarget(GameObject target)
    {
        Transform destroyedTarget = target.transform;
        Destroy(target);
        Transform newTarget=spawningTarget(destroyedTarget);
        if (target.transform == p1)
            p1 = newTarget.transform;
        else if (target.transform == p2)
            p2 = newTarget.transform;
        else
            p3 = newTarget.transform;
    }

    Vector3 getNewPosition(Transform target,int pos)
    {
        Vector3 position;
        switch (pos)
        {
            case 1:
                position = new Vector3(target.position.x + 1.3f, 0, 10);
                break;

            case 2:
                position = new Vector3(target.position.x - 1.3f, 0, 10);
                break;

            case 3:
                position = new Vector3(0, target.position.y - 1.3f, 10);
                break;

            case 4:
                position = new Vector3(0, target.position.y + 1.3f, 10);
                break;

            case 5:
                position = new Vector3(target.position.x + 1.3f, target.position.y + 1.3f, 10);
                break;

            case 6:
                position = new Vector3(target.position.x + 1.3f, target.position.y - 1.3f, 10);
                break;

            case 7:
                position = new Vector3(target.position.x - 1.3f, target.position.y - 1.3f, 10);
                break;

            case 8:
                position = new Vector3(target.position.x - 1.3f, target.position.y - 1.3f, 10);
                break;
            default: position = new Vector3(0, 0, 0);
                break;
        }
        return position;
    }
    
}
