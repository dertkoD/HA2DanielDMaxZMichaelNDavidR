using UnityEngine;

public class AgentWeaponPickup : MonoBehaviour
{
    [Header("Куда крепить оружие")]
    public Transform weaponHolder; // Ссылка на "руку" или слот оружия агента

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что коснулись именно оружия
        if (other.CompareTag("Weapon"))
        {
            PickUpWeapon(other.gameObject);
        }
    }

    void PickUpWeapon(GameObject groundWeapon)
    {
        // ВАРИАНТ 1: Если нужно просто "взять" этот же объект в руку
        // Оружие перемещается в руку, на сцене его больше нет (оно теперь у игрока)
        // Второе оружие, которого мы не касались, остается на месте.
        
        // 1. Отключаем физику, чтобы оружие не падало и не толкалось
        var rb = groundWeapon.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        var col = groundWeapon.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // 2. Перемещаем в "руку"
        groundWeapon.transform.SetParent(weaponHolder);
        groundWeapon.transform.localPosition = Vector3.zero; // Позиция относительно руки
        groundWeapon.transform.localRotation = Quaternion.identity; // Поворот относительно руки

        Debug.Log("Агент подобрал: " + groundWeapon.name);
    }
}