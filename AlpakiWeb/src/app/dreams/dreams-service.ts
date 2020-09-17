import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { DreamsGQL, DreamsQuery, DreamsQueryVariables } from './dreams.list.generated';
import { DreamDetailsGQL, DreamDetailsQuery, DreamDetailsQueryVariables  } from './dreams.details.generated';
import { ApolloQueryResult } from 'apollo-client';

export interface IDreamQueryResponse {
  dreams: IDream[];
}
export interface IDreamCategory {
  dreamCategoryId: number;
  categoryName: string;
}

export interface IDream {
  dreamId: number;
  title: string;
  city: string;
  displayName: string;
  age: number;
  dreamState: string;
  gender: number;
  dreamCategory: IDreamCategory;
}

@Injectable({
  providedIn: 'root'
})
export class DreamsService {
  private dreams: BehaviorSubject<IDream[]> = new BehaviorSubject<IDream[]>([]);
  private loadedDreamsSubject: BehaviorSubject<ApolloQueryResult<DreamsQuery>> = new BehaviorSubject<ApolloQueryResult<DreamsQuery>>(null);

  constructor(private http: HttpClient, private gql: DreamsGQL, private gqlDetails: DreamDetailsGQL) {

  }

  updateState(dreams: IDream[]) {
    this.dreams.next(dreams);
  }

  get loadedDreams() {
    return this.loadedDreamsSubject.asObservable();
  }

  loadDreams(model: DreamsQueryVariables) {
    this.gql.fetch(model).toPromise().then(p => this.loadedDreamsSubject.next(p));
  }

  getDream(dreamId: number): Promise<ApolloQueryResult<DreamDetailsQuery>> {
    return this.gqlDetails.fetch({ dreamId: dreamId.toString() }).toPromise();
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
