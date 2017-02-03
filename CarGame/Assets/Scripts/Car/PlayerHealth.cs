using UnityEngine;
using System.Collections;

//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author(s): JOE PLANT, ADAM MOOREY
// Purpose: Health system for the players
// Namespace: HF
//
//===============================================================================//

namespace HF
{
    public class PlayerHealth : MonoBehaviour
    {
        public float max_Health = 100f;
        public float cur_Health = 0f;
        public int m_damageCounter = 0;
        public GameObject healthbar;
        public Car m_car;
        public DamageSystem m_damage;
        public Movement m_movement;

        public ParticleSystem Smoke1;

        public string m_tag;

        // Use this for initialization
        void Start()
        {
            cur_Health = max_Health;
            m_car = gameObject.GetComponent<Car>();
            m_damage = gameObject.GetComponent<DamageSystem>();
            m_movement = gameObject.GetComponent<Movement>();
            Smoke1 = gameObject.GetComponentInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckHealth();
        }

        public void decreasehealth()
        {
            cur_Health -= 20.0f;
            float calc_Health = cur_Health / max_Health;
            SetHealthBar(calc_Health);
            m_damageCounter++;
            m_damage.ChangeMesh(m_damageCounter);
        }

        public void SetHealthBar(float myHealth)
        {
            //myHealth value 0-1
            healthbar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f, 1f), healthbar.transform.localScale.y, healthbar.transform.localScale.z);
        }

        public void CheckHealth()
        {
            if (cur_Health == 80.0f)
            {
                Smoke1.Play();
            }
            if (cur_Health == 60.0f)
            {
                this.GetComponentInChildren<ParticleSystem>().startColor = new Color(204, 204, 204, 0.5f);
            }
            if (cur_Health == 40.0f)
            {
                this.GetComponentInChildren<ParticleSystem>().startColor = new Color(153, 153, 153, 0.5f);
            }
            if (cur_Health == 20.0f)
            {
                this.GetComponentInChildren<ParticleSystem>().startColor = new Color(102, 102, 102, 0.5f);
            }
            if (cur_Health == 0.0f)
            {
                this.GetComponentInChildren<ParticleSystem>().startColor = new Color(51, 51, 51, 0.5f);
            }

            if (cur_Health <= 0.0f)
            {
                m_movement.m_controls = false;
                m_car.m_isDead = true;
            }
            else
            {
                //m_movement.m_controls = true;
                m_car.m_isDead = false;
            }
        }

        public void ResetHealth()
        {
            Debug.Log("Health has been reset");
            m_damageCounter = 0;
            m_damage.ChangeMesh(m_damageCounter);
            cur_Health = max_Health;
            float calc_Health = cur_Health / max_Health;
            SetHealthBar(calc_Health);
            Smoke1.Stop();
        }
    }
}