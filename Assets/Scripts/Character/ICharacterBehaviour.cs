using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICharacterBehaviour : MonoBehaviour
{
    public interface ITakeDamage
    {
        public void TakeDamage(int damage);
    }

    public interface ICollectable
    {
        public void Collect();
    }
}
