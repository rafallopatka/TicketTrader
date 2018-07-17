import { Injectable } from '@angular/core';

@Injectable()
export class CacheService {

  constructor() { }

    set(key: string, value: any) {
        localStorage.setItem('cache_' + key, JSON.stringify(value));
    }

    get<T>(key: string) {
        const str = localStorage.getItem('cache_' + key);
        const obj = JSON.parse(str);
        return obj as T;
    }

    hasKey(key: string): boolean {
        return localStorage.getItem('cache_' + key) != null;
    }

}
