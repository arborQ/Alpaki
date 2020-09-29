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

  sendFile(file: File): Promise<IAddImageResponse> {
    const formData = new FormData();
    formData.append('formFile', file);

    const params = new HttpParams();

    const options = {
      params,
      reportProgress: true,
    };

    return this.http.post<IAddImageResponse>('/api/images', formData, options).toPromise();
  }

  getFile(fileId: string): Promise<string |ArrayBuffer> {
    return this.http
      .get(`/api/images/${fileId}.png`, { responseType: 'blob' })
      .toPromise()
      .then(blob => {
        const reader = new FileReader();
        reader.readAsDataURL(blob);
        return new Promise<string | ArrayBuffer>(resolve => {
          reader.onload = () => {
            resolve(reader.result);
          };
        });
      });
  }
}
