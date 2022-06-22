using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] float scoreValue;

    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill" || playerTag == "Vortex")
        {
            Debug.Log("Collected Orb");
            //TODO: insert Orb functionality here
            AudioManager.instance.PickupSFX();
            GameplayManager.Instance.OnOrbCollected();
            //TODO: Update statue count in UI and gameManager look at chris' code
            ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS);
            GameplayUIBehavior.Instance.SetOrb(ScoreManager.Instance.GetCurrentOrb());
            GameplayManager.Instance.OnLinkCollected();
            Destroy(this.gameObject);
        }
    }
}
