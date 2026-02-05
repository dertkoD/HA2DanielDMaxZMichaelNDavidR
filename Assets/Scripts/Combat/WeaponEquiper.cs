using UnityEngine;

public class WeaponEquipper : MonoBehaviour
{
    [SerializeField] private AgentRoot agentRoot;
    [SerializeField] private Transform handSocket; // Ссылка на кость руки в иерархии

    [Header("Listening To")]
    [SerializeField] private WeaponPickedEventChannelSO weaponPickedChannel;

    [Header("Broadcasting To")]
    [SerializeField] private WeaponEquippedActionChannelSO weaponEquippedAction;

    private void OnEnable()
    {
        if (weaponPickedChannel) 
            weaponPickedChannel.Register(OnWeaponPicked); // Используем Register (UnityEvent style)
    }

    private void OnDisable()
    {
        if (weaponPickedChannel) 
            weaponPickedChannel.Unregister(OnWeaponPicked);
    }

    private void OnWeaponPicked(WeaponPickupData data)
    {
        if (!agentRoot) return;

        // Проверяем, что оружие подобрал именно ЭТОТ агент
        if (data.pickerAgentId == agentRoot.AgentId)
        {
            EquipWeapon(data);
        }
    }

    private void EquipWeapon(WeaponPickupData data)
    {
        // 1. Очищаем руку, если там что-то было (опционально)
        foreach (Transform child in handSocket) Destroy(child.gameObject);

        // 2. Спавним новое оружие
        if (data.weaponPrefab != null && handSocket != null)
        {
            var weaponInstance = Instantiate(data.weaponPrefab, handSocket);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;
        }

        // 3. Сообщаем системе, что оружие надето (включаем радиус)
        if (weaponEquippedAction)
        {
            weaponEquippedAction.Raise(agentRoot.AgentId, data.weaponId);
        }
    }
    
    
}