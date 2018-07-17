import { Component, OnInit } from '@angular/core';
import { TempDataService } from 'app/services/TempDataService';

@Component({
  selector: 'app-post-authorize-redirect',
  template: '',
  styles: ['']
})
export class PostAuthorizeRedirectComponent implements OnInit {

  constructor(private tempData: TempDataService) { }

  ngOnInit() {
    const returnUrl = this.tempData.get<string>('post_authorization_return_url');
    window.location.href = returnUrl;
  }

}
