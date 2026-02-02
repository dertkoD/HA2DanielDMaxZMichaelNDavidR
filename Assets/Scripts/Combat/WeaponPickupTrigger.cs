using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int weaponId = 1; // ID оружия (например, 1 - пистолет)
    [SerializeField] private GameObject weaponPrefab; // Префаб, который появится в руке

    [Header("Broadcasting To")]
    [SerializeField] private WeaponPickedEventChannelSO weaponPickedChannel;

    private void OnTriggerEnter(Collider other)
    {
        // Ищем AgentRoot на объекте, который вошел в триггер
        var agent = other.GetComponentInParent<AgentRoot>();

        if (agent != null && weaponPickedChannel != null)
        {
            var data = new WeaponPickupData
            {
                pickerAgentId = agent.AgentId,
                weaponId = weaponId,
                weaponPrefab = weaponPrefab
            };

            // Используем Raise, как в твоем SO
            weaponPickedChannel.Raise(data);

            // Удаляем пикап со сцены
            Destroy(gameObject);
        }
    }
}