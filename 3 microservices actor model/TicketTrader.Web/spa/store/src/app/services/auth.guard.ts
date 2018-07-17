import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SecurityService } from 'app/services/security.service';
import { TempDataService } from 'app/services/TempDataService';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private securityService: SecurityService,
    private router: Router,
    private tempData: TempDataService) { }

  canActivate() {
    const isLoggedIn = this.securityService.isUserLoggedIn();
    isLoggedIn.subscribe(loggedin => {
      if (!loggedin) {
        const currentUrl = window.location.href;
        this.tempData.set('post_authorization_return_url', currentUrl);
        this.router.navigate(['Authorize']);
      }
    });
    return isLoggedIn;
  }
}
