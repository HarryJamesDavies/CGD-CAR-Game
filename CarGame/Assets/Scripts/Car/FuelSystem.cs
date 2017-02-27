using UnityEngine;
using System.Collections;

namespace HF
{
	public class FuelSystem : MonoBehaviour
	{
		public float m_fuel = 2.0f;
		public float m_maxFuel = 0.0f;
		public float m_refuelTimer = 0.0f;
		public float m_refuelTime = 2.0f;

		public bool m_setup = false;
		public bool m_reduceFuel = false;
		public bool m_refuel = false;

		void Start()
		{
			EventManager.m_instance.SubscribeToEvent(Events.Event.GM_DRIVEANDSEEK, ActivateFuel);
			EventManager.m_instance.SubscribeToEvent(Events.Event.GM_FREEROAM, DeactivateFuel);
		}

		void Update()
		{
			if (!m_setup)
			{
				if (GetComponent<Car>().m_runner)
				{
					m_fuel = 100.0f;
					m_maxFuel = 100.0f;
					m_setup = true;
				}
				else
				{
					m_fuel = 4000.0f;
					m_maxFuel = 4000.0f;
				}
			}

			if (m_refuel)
			{
				if (m_refuelTimer <= m_refuelTime)
				{
					m_refuelTimer += Time.deltaTime;
				}
				else
				{
					m_refuel = false;
				}
			}
		}

		void FixedUpdate()
		{
			//make sure fuel doesn't go below 0
			if (m_fuel < 0.0f)
			{
				m_fuel = 0.0f;
				m_refuel = true;
			}
		}

		public void Refuel()
		{
			if (m_refuel)
			{
				if (m_fuel < m_maxFuel)
				{
					m_fuel += 5.0f;
				}
			}
		}

		void ActivateFuel()
		{
			m_reduceFuel = true;
		}

		void DeactivateFuel()
		{
			m_reduceFuel = false;
		}

		void OnDestroy()
		{
			EventManager.m_instance.UnsubscribeToEvent(Events.Event.GM_DRIVEANDSEEK, ActivateFuel);
			EventManager.m_instance.UnsubscribeToEvent(Events.Event.GM_FREEROAM, DeactivateFuel);
		}
	}
}