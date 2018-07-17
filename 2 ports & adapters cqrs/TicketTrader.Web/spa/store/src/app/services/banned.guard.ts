import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SecurityService } from 'app/services/security.service';
import { TempDataService } from 'app/services/TempDataService';

@Injectable()
export class BannedGuard implements CanActivate {

  constructor(private securityService: SecurityService,
    private router: Router,
    private tempData: TempDataService) { }

  canActivate() {
    const isBanned = this.securityService
    .getUser()
    .map(u => {
      return u.profile.is_banned_customer === 'false';
    })

    isBanned.subscribe(loggedin => {
      if (!loggedin) {
        const currentUrl = window.location.href;
        this.tempData.set('post_authorization_return_url', currentUrl);
        this.router.navigate(['Forbidden']);
      }
    });
    return isBanned;
  }
}
