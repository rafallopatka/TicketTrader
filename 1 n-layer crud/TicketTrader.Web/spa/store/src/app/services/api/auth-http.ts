
import { Injectable, Inject, Optional, OpaqueToken } from '@angular/core';
import { Http, Headers, ResponseContentType, Response, ConnectionBackend, RequestOptions, RequestOptionsArgs } from '@angular/http';

import { environment } from 'environments/environment';
import { SecurityService } from 'app/services/security.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthHttp {
    constructor(private http: Http, private securityService: SecurityService) { }

    request(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.request(url, o));
    }
    /**
     * Performs a request with `get` http method.
     */
    get(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.get(url, o));
    }
    /**
     * Performs a request with `post` http method.
     */
    post(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.post(url, body, o));
    }
    /**
     * Performs a request with `put` http method.
     */
    put(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.put(url, body, o));
    }
    /**
     * Performs a request with `delete` http method.
     */
    delete(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.delete(url, o));
    }
    /**
     * Performs a request with `patch` http method.
     */
    patch(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.patch(url, body, o));
    }
    /**
     * Performs a request with `head` http method.
     */
    head(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.head(url, o));
    }
    /**
     * Performs a request with `options` http method.
     */
    options(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return this.withOptions(options)
            .switchMap(o => this.http.options(url, o));
    }

    private getAccessToken(): Observable<string> {
        return this.securityService.getUser().map(u => u.access_token);
    }

    private withOptions(options: RequestOptionsArgs): Observable<RequestOptionsArgs> {
        if (options == null) {
            options = {};
        }

        if (options.headers == null) {
            options.headers = new Headers();
        }

        return this.getAccessToken()
            .map(accessToken => {
                options.headers.set('Authorization', 'Bearer ' + accessToken);
                return options;
            })
    }

    public asHttp(): Http {
        return this as any as Http;
    }
}
