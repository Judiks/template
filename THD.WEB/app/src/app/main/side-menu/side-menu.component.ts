import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationStart, Event as NavigationEvent, NavigationEnd } from '@angular/router';
import { AuthenticationHelper } from 'src/app/shared/helpers/auth.helper';
import { filter } from 'rxjs/operators';
@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent implements OnInit {
  public menu = {
    userRoutes: [
      {
        name: 'Pricing Simulation',
        path: '/pricing-simulation',
      },
      {
        name: 'Promo Simulation',
        path: '/promo-simulation',
      }
    ],
    adminRoutes: [
      {
        name: 'Product Categories Setup',
        path: '/product-categories-setup',
      },
      {
        name: 'Baseline Setup',
        path: '/baseline-setup',
      },
      {
        name: 'Calibration',
        path: '/calibration',
      }
    ],
    superAdminRoute: [
      {
        name: 'User Management',
        path: '/user-management',
      }
    ]
  };
  public currentRoute = '';
  public currentUserName = '';
  public currentUserRoles = [];
  constructor(private router: Router, private authenticationHelper: AuthenticationHelper) {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd)
      )
      .subscribe(
        (event: NavigationEvent) => {
          this.currentRoute = this.router.url;
        }
      );
    this.currentUserName = this.authenticationHelper.currentUserValue.fullName;
    this.currentUserRoles = authenticationHelper.currentUserValue.roles;
  }

  ngOnInit() {


  }

  public logout() {
    this.authenticationHelper.logout();
  }
}
