using UnityEngine;

public class Enemy : MonoBehaviour
{    
    public void Init(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            Debug.Log("Enemy Hit !");
            other.gameObject.SetActive(false);
        }
    }
}
