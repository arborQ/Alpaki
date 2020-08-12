import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
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
  private dreams: BehaviorSubject<IDream[]> = new BehaviorSubject<IDream[]>([]);

  constructor(private http: HttpClient) { }

  getDreams() {
    this.http.get<IDreamQueryResponse>('/api/dreamers?page=1').subscribe(response => {
      this.dreams.next(response.dreams);
    });

    return this.dreams;
  }

  getDream(dreamId: number): Observable<IDreamQueryResponse> {
    return this.http.get<IDreamQueryResponse>(`/api/dreamers/${dreamId}`);
  }

  deleteDream(dreamId: number) {
    const beforeDelete = this.dreams.value;
    const users = [...beforeDelete.filter(u => u.dreamId !== dreamId)];
    this.dreams.next(users);

    this.http.delete(`/api/dreamers?dreamId=${dreamId}`).toPromise().catch(() => {
      setTimeout(() => {
        alert('ojojoj');

        this.dreams.next(beforeDelete);
      }, 1000);
    });
  }
}
