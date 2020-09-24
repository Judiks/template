import { Component, OnInit } from '@angular/core';
import { SpinnerService } from '../spinner-service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnInit {
  public spinnerIsRun: boolean;
  constructor(private spinnerService: SpinnerService) {
    this.spinnerIsRun = false;
    this.spinnerService.isStart.subscribe(isStart => {
      this.spinnerIsRun = isStart;
    });
   }

  ngOnInit() {
  }

}
