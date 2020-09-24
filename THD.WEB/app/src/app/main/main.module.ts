import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MainComponent,
    SideMenuComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    MainRoutingModule
  ]
})
export class MainModule { }
