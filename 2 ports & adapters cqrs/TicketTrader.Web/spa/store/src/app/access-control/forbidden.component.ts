import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forbidden',
  template: '',
  styles: ['']
})
export class ForbiddenComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    window.location.href = window.location.protocol + '//' + window.location.hostname + '/Spa/Store/403.html';
  }

}
