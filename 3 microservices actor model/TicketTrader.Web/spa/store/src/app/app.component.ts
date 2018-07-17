import { Component, OnInit, OnDestroy } from '@angular/core';
import { SecurityService } from 'app/services/security.service';
import { Subscription } from 'rxjs/Subscription';
import { UserClientService } from 'app/services/user-client.service';
import { UserClientDto } from 'app/services/api/app.clients';
import { MessageBus } from 'app/messages/message-bus';
import { UserProfileLoadedMessage, UserClientLoadedMessage } from 'app/messages/messages';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  loadUserClientSubscription: Subscription;
  userLoadProfileSubscription: Subscription;
  userDataLoadedSubscription: Subscription;

  userDisplayName: string;
  isLoggedIn: boolean;
  title = 'TicketTrader.Web Store';

  constructor(
    private securityService: SecurityService,
    private clientsService: UserClientService,
    private messageBus: MessageBus) {
  }

  ngOnInit() {
    const userProfileObservable = this.messageBus
      .of(UserProfileLoadedMessage)
      .map(message => message.user);

    this.userDataLoadedSubscription = userProfileObservable
      .subscribe(user => {
        this.isLoggedIn = true;
        this.userDisplayName = user.profile.name;
      });

    this.loadUserClientSubscription = userProfileObservable
      .switchMap(user => this.clientsService.getOrCreateUserClient(user))
      .subscribe(client => {
        this.messageBus.publish(new UserClientLoadedMessage(client));
      });

    this.loadUserProfile();
  }

  ngOnDestroy(): void {
    this.loadUserClientSubscription.unsubscribe();
    this.userLoadProfileSubscription.unsubscribe();
    this.userDataLoadedSubscription.unsubscribe();
  }

  logout() {
    this.securityService.signoutRedirect();
  }

  loadUserProfile() {
    this.userLoadProfileSubscription = this.securityService
      .getUser()
      .filter(user => user != null)
      .subscribe(user => {
        this.messageBus.publish(new UserProfileLoadedMessage(user))
      });
  }
}
