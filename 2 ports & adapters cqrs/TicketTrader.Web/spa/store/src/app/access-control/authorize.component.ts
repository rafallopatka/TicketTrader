import { Component, OnInit } from '@angular/core';
import { SecurityService } from 'app/services/security.service';

@Component({
  selector: 'app-authorize',
  template: '',
  styles: ['']
})
export class AuthorizeComponent implements OnInit {
  constructor(private securityService: SecurityService) { }

  ngOnInit() {
    this.securityService.isUserLoggedIn()
      .subscribe(isLoggedIn => {
        if (isLoggedIn === false) {
          this.securityService.signinRedirect();
        }
      });
  }
}
