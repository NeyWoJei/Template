using R3;

namespace Game.Systems
{
    public class EventBus
    {  
        public Subject<Unit> OnGameEvent { get; } = new Subject<Unit>();
        public Subject<string> OnTriggerEvent { get; } = new Subject<string>();

        public void TriggerGameEvent() {
            OnGameEvent.OnNext(Unit.Default);
        }

        public void TriggerEventWithId(string eventId) {
            OnTriggerEvent.OnNext(eventId);
        }
    }
}
