using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeelleAttack : MonoBehaviour
{
    public Transform weaponHolder; // Ссылка на держатель оружия
    public MelleeWeapon weaponScript;   // Ссылка на скрипт оружия
    public float attackAngle = 60f; // Угол атаки
    public float attackSpeed = 0.2f; // Время атаки

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // ЛКМ для атаки
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        weaponScript.isAttacking = true; // Атака начинается

        float startAngle = -attackAngle / 2;
        float endAngle = attackAngle / 2;

        float elapsedTime = 0f;

        while (elapsedTime < attackSpeed)
        {
            float t = elapsedTime / attackSpeed;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            weaponHolder.localRotation = Quaternion.Euler(0, 0, angle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        weaponHolder.localRotation = Quaternion.Euler(0, 0, 0); // Возврат в исходное положение
        weaponScript.isAttacking = false; // Атака закончилась
    }
}
