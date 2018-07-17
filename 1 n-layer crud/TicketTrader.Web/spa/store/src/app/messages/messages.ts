import { User } from 'oidc-client';
import { UserClientDto } from 'app/services/api/app.clients';

export class UserProfileLoadedMessage {
    constructor(public user: User) { }
}

export class UserClientLoadedMessage {
    constructor(public client: UserClientDto) { }
}
