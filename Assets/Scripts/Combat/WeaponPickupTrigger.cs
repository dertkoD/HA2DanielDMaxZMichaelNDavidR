using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int weaponId = 1; 
    [SerializeField] private GameObject weaponPrefab; 

    [Header("Broadcasting To")]
    [SerializeField] private WeaponPickedEventChannelSO weaponPickedChannel;

    private void OnTriggerEnter(Collider other)
    {
       
        var agent = other.GetComponentInParent<AgentRoot>();

        if (agent != null && weaponPickedChannel != null)
        {
            var data = new WeaponPickupData
            {
                pickerAgentId = agent.AgentId,
                weaponId = weaponId,
                weaponPrefab = weaponPrefab
            };
            
            weaponPickedChannel.Raise(data);
            
            Destroy(gameObject);
        }
    }
}