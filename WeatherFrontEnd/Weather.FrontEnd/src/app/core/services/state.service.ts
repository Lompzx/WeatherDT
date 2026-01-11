import { BehaviorSubject, Observable } from 'rxjs';

export class StateService<T> {

  // Internal BehaviorSubject that holds the current state value.
  // It always keeps the latest value and emits it to new subscribers.
  protected subject: BehaviorSubject<T | null>;

  // Constructor initializes the state with an initial value.
  // If no value is provided, the state starts as null.
  constructor(initialValue: T | null = null) {
    this.subject = new BehaviorSubject<T | null>(initialValue);
  }

  // Exposes the state as a read-only Observable.
  // Consumers can subscribe to it but cannot emit new values.
  get$(): Observable<T | null> {
    return this.subject.asObservable();
  }

  // Returns the current state value synchronously.
  // Useful for imperative reads (e.g., on component initialization).
  getValue(): T | null {
    return this.subject.value;
  }

   // Updates the state with a new value.
  // All subscribers will be notified immediately.
  set(value: T): void {
    this.subject.next(value);
  }

  // Clears the state by resetting it to null.
  // Useful when leaving a feature or resetting the flow.
  clear(): void {
    this.subject.next(null);
  }
}
