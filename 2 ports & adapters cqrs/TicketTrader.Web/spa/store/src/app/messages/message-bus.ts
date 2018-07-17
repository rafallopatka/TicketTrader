import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs/Rx';

interface Message {
  channel: string;
  data: any;
}

@Injectable()
export class MessageBus {
  private message$: Subject<Message>

  constructor() {
    this.message$ = new Subject<Message>();
  }

  public publish<T>(message: T): void {
    const channel = (<any>message.constructor).name;
    const messageEnvelope =  { channel: channel, data: message };
    console.log('Message', messageEnvelope);
    this.message$.next(messageEnvelope);
  }

  public of<T>(messageType: { new(...args: any[]): T }): Observable<T> {
    const channel = (<any>messageType).name;
    const message = this.message$;

    return this.message$
      .filter(m => m.channel === channel)
      .map(m => m.data);
  }
}
