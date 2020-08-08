import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

export interface IDreamQueryResponse {
  dreams: IDream[];
}
export interface IDreamCategory {
  dreamCategoryId: number;
  categoryName: string;
}

export interface IDream {
  dreamId: number;
  firstName: string;
  age: number;
  lastName: string;
  dreamState: string;
  gender: number;
  dreamCategory: IDreamCategory;
}

@Injectable({
  providedIn: 'root'
})
export class DreamsService {

  constructor(private http: HttpClient) { }

  getDreams(): Observable<IDreamQueryResponse> {
    return this.http.get<IDreamQueryResponse>('/api/dreamers?page=2');
  }

  getDream(dreamId: number): Observable<IDreamQueryResponse> {
    return this.http.get<IDreamQueryResponse>(`/api/dreamers/${dreamId}`);
  }
}
