import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRoute, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SecurityService } from 'app/services/security.service';
import { TempDataService } from 'app/services/TempDataService';

@Injectable()
export class EventGuard implements CanActivate {

  constructor(private securityService: SecurityService,
    private router: Router,
    private tempData: TempDataService,
    private activeRoute: ActivatedRoute) { }

  canActivate() {
    const eventids = this.activeRoute
      .params
      .map(p => {
        return this.readUrlEventId();
      })

    eventids.subscribe(eventId => {
      if (eventId == null || Number.isInteger(eventId) === false) {
        this.router.navigate(['NotFound']);
      }
    });

    return eventids.map(eventId => eventId != null && Number.isInteger(eventId));
  }

  readUrlEventId(): number {
    const urlSegments = window.location.href.split('/');
    return +urlSegments[urlSegments.length - 1];
  }
}
