import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class SpinnerService {
    private start = new Subject<boolean>();
    public isStart = this.start.asObservable();

    public startSpinner(isStart: boolean) {
        this.start.next(isStart);
    }

}
