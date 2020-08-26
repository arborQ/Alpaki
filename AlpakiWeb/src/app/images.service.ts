import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';
import { map } from 'rxjs/operators';

export interface IAddImageResponse {
  imageId: string;
}

@Injectable({
  providedIn: 'root'
})
export class ImagesService {

  constructor(private http: HttpClient) { }

  sendFile(file: File) : Promise<IAddImageResponse> {
    let formData = new FormData();
    formData.append('formFile', file);

    let params = new HttpParams();

    const options = {
      params: params,
      reportProgress: true,
    };

    const req = new HttpRequest('POST', '/api/images', formData, options);
    
    return this.http.post<IAddImageResponse>('/api/images', formData, options).toPromise();
  }
}
