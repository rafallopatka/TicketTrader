import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { environment } from 'environments/environment';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { SceneDetailsDto } from 'app/services/api/app.clients';

export class SceneFileWithDescription {
  constructor(public id: number, public details: SceneDetailsDto, public svg: string) { }
}

@Injectable()
export class SceneService {
  constructor(private http: Http,
    private apiClientsFactory: ApiClientsFactory) { }

  public getScene(eventId: number): Observable<SceneFileWithDescription> {
    const sceneClient = this.apiClientsFactory.createEventsScenesService();

    const sceneObservable = sceneClient.get(eventId)
      .map(r => r.result)
      .map(r => r[0])
      .switchMap(scene => {
        const fileObservable = this.getSceneFile(scene.uniqueName)
          .map(file => new SceneFileWithDescription(scene.id, scene, file))
        return fileObservable;
      });

    return sceneObservable;
  }

  private getSceneFile(fileName: string): Observable<string> {
    const hostAddress = environment.hostAddress as string;
    return this.http.get(hostAddress + '/scenes/' + fileName)
      .map(this.extractData)
      .catch(this.handleError)
  }

  private extractData(res: Response) {
    const body = res.text();
    return body;
  }

  private handleError(error: Response | any) {
    // In a real world app, you might use a remote logging infrastructure
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);
  }
}
