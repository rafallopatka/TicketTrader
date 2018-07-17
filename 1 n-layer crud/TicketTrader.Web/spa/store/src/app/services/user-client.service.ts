import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { User } from 'oidc-client';
import { UserDto, UserClientDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';
import { CacheService } from 'app/services/cache.service';

@Injectable()
export class UserClientService {

  constructor(private clientsFactory: ApiClientsFactory, private cache: CacheService) { }

  getUserClient(user: User): Observable<UserClientDto> {
    if (user == null) {
      return Observable.empty();
    }

    const userId = user.profile.sub;

    const api = this.clientsFactory.createUsersClientsService();
    return api
      .get(userId)
      .map(response => response.failure === true ? [] : response.result)
      .switchMap(clients => {
        if (clients.length === 0) {
          return Observable.empty();
        } else {
          const result = clients[0];
          return Observable.of(result);
        }
      });
  }

  getOrCreateUserClient(user: User): Observable<UserClientDto> {
    if (user == null) {
      return Observable.empty();
    }

    const userId = user.profile.sub;
    const api = this.clientsFactory.createUsersClientsService();
    return api
      .get(userId)
      .map(response => response.failure === true ? [] : response.result)
      .switchMap(clients => {
        if (clients.length === 0) {

          const address = JSON.parse(user.profile.address);

          const userDto = new UserDto();
          userDto.identityUserId = userId;
          userDto.fistName = user.profile.given_name;
          userDto.lastName = user.profile.family_name;
          userDto.email = user.profile.email;
          userDto.address = address.street_address;
          userDto.city = address.locality;
          userDto.country = address.country;
          userDto.postalCode = address.postal_code;

          return api.post(userId, userDto)
            .filter(response => response.failure === false)
            .map(response => response.result)
        } else {
          const result = clients[0];
          return Observable.of(result);
        }
      });
  }
}
