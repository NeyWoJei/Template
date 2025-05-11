using System;
using System.Collections.Generic;
using R3;

namespace Game.Systems
{
    public class EventBus
    {  
        public Subject<Unit> OnGameEvent { get; } = new Subject<Unit>();
        public Subject<string> OnTriggerEvent { get; } = new Subject<string>();
        private readonly Dictionary<Type, object> _subjects = new();

        public Subject<T> GetSubject<T>() {
            var type = typeof(T);
            if (!_subjects.TryGetValue(type, out var Subject)) {
                Subject = new Subject<T>();
                _subjects[type] = Subject;
            }
            return (Subject<T>)Subject;
        }
        public void Publish<T>(T evt) {
            GetSubject<T>().OnNext(evt);
        }
        public IObservable<T> Receive<T>() {
            return (IObservable<T>)GetSubject<T>();
        }

        public void TriggerGameEvent() {
            OnGameEvent.OnNext(Unit.Default);
        }

        public void TriggerEventWithId(string eventId) {
            OnTriggerEvent.OnNext(eventId);
        }
    }
}
