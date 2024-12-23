import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Membership } from '../_models/membership';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembershipService {
  baseUrl: string = environment.apiUrl
  private http = inject(HttpClient);
  membership = signal<Membership[]>([]);

  createMembership(membership: Membership):Observable<Membership>{
    return this.http.post<Membership>(this.baseUrl+'memberships/', membership);
  }
}
