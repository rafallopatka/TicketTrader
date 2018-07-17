
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { MessageBus } from 'app/messages/message-bus';

export class BaseComponent {
    constructor(public messageBus: MessageBus) {
    }

    public notifyPropertyChanged<T>(subject: ReplaySubject<T>, value: T) {
        if (value == null) {
            return;
        }

        subject.next(value);
    }
}
