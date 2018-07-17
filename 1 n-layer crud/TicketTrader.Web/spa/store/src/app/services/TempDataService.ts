import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TempDataService {
    constructor() {
    }

    set(key: string, value: any) {
        sessionStorage.setItem('temp_' + key, JSON.stringify(value));
    }

    get<T>(key: string) {
        const str = sessionStorage.getItem('temp_' + key);
        const obj = JSON.parse(str);
        sessionStorage.removeItem('temp_' + key);
        return obj as T;
    }

    hasKey(key: string): boolean {
        return sessionStorage.getItem('temp_' + key) != null;
    }
}
