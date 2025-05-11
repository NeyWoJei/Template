using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class GameLoop : MonoBehaviour, IInitializable, ITickable, IFixedTickable
    {
        private readonly List<ITickable> _tickables = new List<ITickable>();
        private readonly List<IInitializable> _initializables = new List<IInitializable>();
        private readonly List<IFixedTickable> _fixedTickables = new List<IFixedTickable>();

        private void Awake()
        {
            Debug.Log("GameLoop Awake");
        }

        public void RegisterTickable(ITickable tickable)
        {
            if ((object)tickable != this && !_tickables.Contains(tickable))
            {
                _tickables.Add(tickable);
                Debug.Log($"Registered Tickable: {tickable.GetType().Name}");
            }
        }

        public void RegisterInitializable(IInitializable initializable)
        {
            if ((object)initializable != this && !_initializables.Contains(initializable))
            {
                _initializables.Add(initializable);
            }
        }

        public void RegisterFixedTickable(IFixedTickable fixedTickable)
        {
            if ((object)fixedTickable != this && !_fixedTickables.Contains(fixedTickable))            
            {
                _fixedTickables.Add(fixedTickable);
            }
        }

        public void Initialize()
        {
            foreach (var i in _initializables)
                i.Initialize();
        }

        public void Tick()
        {
            // Debug.Log("GameLoop Tick");
            for (int i = 0; i < _tickables.Count; i++)
                _tickables[i].Tick();
        }

        public void FixedTick()
        {
            for (int i = 0; i < _fixedTickables.Count; i++)
                _fixedTickables[i].FixedTick();
        }

        private void Update()
        {
            if (Application.isPlaying)
                Tick(); 
        }

        private void FixedUpdate()
        {
            if (Application.isPlaying)
                FixedTick(); 
        }
    }
}