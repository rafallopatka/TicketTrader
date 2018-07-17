import { Injectable } from '@angular/core';
import { UserManager, Log, MetadataService, User } from 'oidc-client';
import { Observable } from 'rxjs/Observable';

const settings: any = {
  authority: 'http://tickettrader.identity',
  client_id: 'tickettrader.js',
  redirect_uri: window.location.origin + '/Spa/Store/signin-callback.html',
  post_logout_redirect_uri: window.location.origin,

  response_type: 'id_token token',
  scope: 'openid profile email address api.sale ticket_trader_store_claims',

  loadUserInfo: true,

  silent_redirect_uri: window.location.origin + '/Spa/Store/silent-calback.html',
  automaticSilentRenew: true,

  revokeAccessTokenOnSignout: true,
  filterProtocolClaims: false
};

@Injectable()
export class SecurityService {
  userManager: UserManager;

  constructor() {
    // Log.logger = window.console;
    // Log.level = Log.DEBUG;

    this.userManager = new UserManager(settings);

    this.userManager.events.addSilentRenewError(this.onSilentRenewError);
    this.userManager.events.addUserSignedOut(this.onUserSignedOut);
  }

  isUserLoggedIn(): Observable<boolean> {
    return Observable.fromPromise(this.userManager.getUser()).map(user => user !== null);
  }

  signinRedirect(): any {
    this.userManager.signinRedirect();
  }

  signinRedirectCallback(): Observable<any> {
    return Observable.fromPromise(this.userManager.signinRedirectCallback());
  }

  signinSilent(): any {
    this.userManager.signinSilentCallback();
  }

  getUser(): Observable<User> {
    return Observable.fromPromise(this.userManager.getUser());
  }

  signoutRedirect(): any {
    this.userManager.signoutRedirect();
  }

  onSilentRenewError(error: any): any {
    console.log('onSilentRenewError ' + error);
  }
  onUserSignedOut(signedOut: any): any {
    console.log('onUserSignedOut ' + signedOut);
  }
  onaddAccessTokenExpiring(error: any): any {
    console.log('onaddAccessTokenExpiring ' + error);
  }
  onAccessTokenExpired(error: any): any {
    console.log('onAccessTokenExpired ' + error);
  }
}
