using UnityEngine;

public class AgentWeaponPickup : MonoBehaviour
{
    [Header("Сюда перетащи объект 'Hand' или 'WeaponSlot'")]
    public Transform weaponHolder; 

    private void OnTriggerEnter(Collider other)
    {
        // 1. Сначала просто пишем, во что мы врезались.
        // Если тут будет имя твоей ПАПКИ (группы), значит ты не удалил с неё коллайдер.
        Debug.Log($"[КОНТАКТ] Агент коснулся объекта: '{other.name}'. Его родитель: '{other.transform.parent?.name ?? "НЕТ"}'");

        // 2. Проверяем тег
        if (other.CompareTag("Weapon"))
        {
            Debug.Log($"[ЛОГИКА] Тег 'Weapon' найден на объекте '{other.name}'. Начинаю подбор...");
            PickUpWeapon(other.gameObject);
        }
        else
        {
            // Если ты думаешь, что настроил тег, а тут вылезет это сообщение — значит, ты настроил криво.
            Debug.LogWarning($"[ПРОПУСК] Объект '{other.name}' имеет тег '{other.tag}', а мы ищем 'Weapon'. Игнорирую.");
        }
    }

    void PickUpWeapon(GameObject groundWeapon)
    {
        // 3. Смотрим, что именно мы сейчас будем уничтожать/перемещать
        Debug.Log($"[ДЕЙСТВИЕ] Обрабатываю объект: {groundWeapon.name}");

        // Логика подбора (перенос в руку)
        // Если у тебя префабы — раскомментируй Instantiate и Destroy ниже, а этот блок закомментируй.
        
        // --- ВАРИАНТ 1: Просто берем этот же объект (Reparenting) ---
        var rb = groundWeapon.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        var col = groundWeapon.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        groundWeapon.transform.SetParent(weaponHolder);
        groundWeapon.transform.localPosition = Vector3.zero;
        groundWeapon.transform.localRotation = Quaternion.identity;
        
        Debug.Log($"[ФИНАЛ] Объект {groundWeapon.name} теперь в руке. Остальные не тронуты.");

        // --- ВАРИАНТ 2: (Если используешь Instantiate) ---
        /*
        GameObject newWeapon = Instantiate(groundWeapon, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        Destroy(newWeapon.GetComponent<Collider>()); // Чтобы рука сама себя не ловила
        
        Debug.Log($"[УНИЧТОЖЕНИЕ] Удаляю объект с земли: {groundWeapon.name}");
        Destroy(groundWeapon);
        */
    }
}