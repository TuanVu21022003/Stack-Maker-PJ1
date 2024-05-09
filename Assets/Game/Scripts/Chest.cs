using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestOpen, chestClose;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == KeyConstants.KEY_TAG_PLAYER)
        {
            chestClose.gameObject.SetActive(false);
            
            chestOpen.gameObject.SetActive(true);

            other.gameObject.GetComponent<Player>().SetFinal();
        }
    }
}
