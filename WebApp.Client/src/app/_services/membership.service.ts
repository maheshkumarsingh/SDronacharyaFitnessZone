import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Membership } from '../_models/membership';
import { catchError, Observable, of, Subscription, tap } from 'rxjs';
import { MembershipPlan } from '../_models/membership-plan';

@Injectable({
  providedIn: 'root'
})
export class MembershipService {
  baseUrl: string = environment.apiUrl
  private http = inject(HttpClient);
  memberships = signal<Membership[]>([]);
  membershipPlans = signal<MembershipPlan[]>([]);

  createMembership(membership: Membership): Observable<Membership> {
    return this.http.post<Membership>(this.baseUrl+'memberships/', membership).pipe(
      tap((newMembership) => {
        this.memberships.update((currentMemberships) => [...currentMemberships, newMembership]);
      })
    );
  }
  getMemberships(memberLoginName: string|null): Observable<Membership[]> {
    return this.http.get<Membership[]>(`${this.baseUrl}memberships?memberLoginId=${memberLoginName}`).pipe(
      tap((memberships) => this.memberships.set(memberships)),
      catchError((error) => {
        console.error('Failed to fetch memberships', error);
        return of([]);
      })
    );
  }
  updateMembership(membership: Membership): Observable<Membership> {
    return this.http.put<Membership>(this.baseUrl+'memberships/', membership).pipe(
      tap((updatedMembership) => {
        this.memberships.update((currentMemberships) => 
                  currentMemberships.map((m) => m.id === updatedMembership.id ? updatedMembership : m));
      })
    );
  }
//https://localhost:7221/api/Memberships/allMemberships
  getMembershipPlans(){
    return this.http.get<MembershipPlan[]>(this.baseUrl+'memberships/allMemberships').subscribe({
      next: response => this.membershipPlans.set(response)
    });
  }
}
