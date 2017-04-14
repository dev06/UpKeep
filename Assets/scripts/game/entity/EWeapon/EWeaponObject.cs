using UnityEngine;
using System.Collections;
using GameUtility;
namespace Game
{
	[RequireComponent (typeof(ObjectIdentifier))]
	public class EWeaponObject : MonoBehaviour {



		private ParticleSystem muzzle;
		public Weapon instance;
		public ObjectIdentifier objectIdentifier;



		void Start ()
		{
			objectIdentifier = GetComponent<ObjectIdentifier>();
			instance = (Weapon)objectIdentifier.GetObject();
			instance.eWeaponObject = this;
			muzzle = transform.FindChild("muzzle").GetChild(0).GetComponent<ParticleSystem>();
			muzzle.Stop();

		}


		public virtual void Attack(Weapon w, Transform parent)
		{
			Ray ray = new Ray(parent.position, parent.forward);
			RaycastHit hitInfo;
			TriggerMuzzle();

			if (Physics.Raycast(ray.origin, parent.forward, out hitInfo,  w.range))
			{
				GameObject hitObject = hitInfo.transform.gameObject;
				if (hitObject.GetComponent<Mob>() != null)
				{
					Mob mob = (Mob)hitObject.GetComponent<Mob>();
					mob.DoDamage(w.damage);
				}

				if (hitObject.GetComponent<Rigidbody>() != null)
				{
					hitObject.GetComponent<Rigidbody>().velocity = parent.forward * 10.0f;
				}

				if (hitObject.layer == MasterVar.LAYER_BUILDING)
				{
					if (GameResource.BulletHole != null)
					{
						GameObject clone = Instantiate(GameResource.BulletHole) as GameObject;
						clone.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
						clone.transform.forward = hitInfo.normal;
						clone.transform.position = hitInfo.point + clone.transform.forward * .01f;
					}
				}
			}
		}



		public void TriggerMuzzle()
		{
			if (muzzle == null) { return; }
			muzzle.Play();
		}


	}
}
