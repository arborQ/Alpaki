import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, of } from 'rxjs';
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

updateState(dreams: IDream[]) {
  this.dreams.next(dreams);
}

  getDreams() {
    this.http.get<IDreamQueryResponse>('/api/dreams?page=1')
      .subscribe(response => {
        this.dreams.next(response.dreams);
      });

    return this.dreams.asObservable();
  }

  getDream(dreamId: number): Observable<IDream> {
    const [existingDream] = this.dreams.value.filter(d => d.dreamId === dreamId);

    if (existingDream) {
      return this.dreams.pipe(map(dreams =>  dreams.filter(d => d.dreamId === dreamId)[0]));
    }

    return this.http
      .get<IDream>(`/api/dreamers/dreams?dreamId=${dreamId}`);
  }

  deleteDream(dreamId: number) {
    const beforeDelete = this.dreams.value;
    const users = [...beforeDelete.filter(u => u.dreamId !== dreamId)];
    this.dreams.next(users);

    this.http.delete(`/api/dreams?dreamId=${dreamId}`).toPromise().catch(() => {
      setTimeout(() => {
        alert('ojojoj');

        this.dreams.next(beforeDelete);
      }, 1000);
    });
  }
}
