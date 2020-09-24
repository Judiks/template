import { NgModule } from '@angular/core';
import { AreYouSurePopupComponent } from './components/are-you-sure-popup/are-you-sure-popup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { DebounceDirective } from './directive/app-debounce';
import { MatTooltipModule } from '@angular/material';
import { SpinnerService } from './spinner/spinner-service';
import { SpinnerComponent } from './spinner/spinner/spinner.component';

@NgModule({
    declarations: [
        AreYouSurePopupComponent,
        DebounceDirective,
        SpinnerComponent
    ],
    imports: [
        FormsModule,
        CommonModule,
        MatDialogModule,
        ReactiveFormsModule,
    ],
    entryComponents: [
        AreYouSurePopupComponent
    ],
    exports: [
        AreYouSurePopupComponent,
        DebounceDirective,
        MatTooltipModule,
        SpinnerComponent
    ],
    providers: [
        SpinnerService
    ]
})
export class SharedModule { }
