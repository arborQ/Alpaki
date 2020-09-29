import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'apollo-link';

export interface ICurrentUserProfile {
  firstName: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http: HttpClient) { }

  currentUserProfile() {
    return this.http.get<ICurrentUserProfile>('/api/user/me');
  }

  updateCurrentUserProfile(newProfile: ICurrentUserProfile) {
    return this.http.patch('/api/user/me', newProfile);
  }
}
