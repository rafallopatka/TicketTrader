import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-page-not-found',
  template: '',
  styles: ['']
})
export class PageNotFoundComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    window.location.href = window.location.protocol + '//' + window.location.hostname + '/Spa/Store/404.html';
  }
}
