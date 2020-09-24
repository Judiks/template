import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserEditorComponent } from 'src/app/main/user-management/components/user-editor/user-editor.component';

@Component({
  selector: 'app-are-you-sure-popup',
  templateUrl: './are-you-sure-popup.component.html',
  styleUrls: ['./are-you-sure-popup.component.scss']
})
export class AreYouSurePopupComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<UserEditorComponent>) { }

  ngOnInit() {
  }

  public onButtonClick(value: boolean): void {
    this.dialogRef.close(value);
  }

}
