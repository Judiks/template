import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: '<ngx-spinner></ngx-spinner><router-outlet></router-outlet>'
})
export class AppComponent {
  title = 'app';
  /**
   *
   */
  constructor() {

  }
}
