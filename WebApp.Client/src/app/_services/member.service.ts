import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { Observable, of, Subscription, tap } from 'rxjs';
import { Photo } from '../_models/photo';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl: string = environment.apiUrl
  private http = inject(HttpClient);
  // members = signal<Member[]>([]);
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null);
  membersCache = new Map();

  getAllMembers(userParams: UserParams) {
    //https://localhost:7221/api/members?pageNumber=1&pageSize=5

    const response = this.membersCache.get(Object.values(userParams).join('-'));
    console.log(Object.values(userParams).join('-'));
    if (response) {
      console.log('response');
      return this.setPaginatedResponse(response);
    }
    let params = this.setPaginationHeader(userParams.pageNumber, userParams.pageSize);
    // if (userParams.phoneNumber) {
    //   params = params.append('phoneNumber', userParams.phoneNumber);
    // }
    // if (userParams.firstName) {
    //   params = params.append('firstName', userParams.firstName);
    // }
    // if (userParams.lastName) {
    //   params = params.append('lastName', userParams.lastName);
    // }
    if (userParams.gender) {
      params = params.append('gender', userParams.gender);
    }
    if (userParams.plan) {
      params = params.append('plan', userParams.plan);
    }
    if (userParams.planStatus) {
      params = params.append('planStatus', userParams.planStatus);
    }
    return this.http.get<Member[]>(this.baseUrl + 'members', { observe: 'response', params }).subscribe({
      next: response => {
        this.setPaginatedResponse(response);
        this.membersCache.set(Object.values(userParams).join('-'), response);
      }
    });
  }
  private setPaginatedResponse(response: HttpResponse<Member[]>) {
    this.paginatedResult.set({
      items: response.body as Member[],
      pagination: JSON.parse(response.headers.get('Pagination')!)
    });
  }
  private setPaginationHeader(pageNumber: number, pageSize: number): HttpParams {
    let params = new HttpParams();
    if (pageNumber && pageSize) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    return params;
  }
  getMemberByMemberLoginName(memberLoginName: string) {
    // const member = this.members().find(x => x.memberLoginName === memberLoginName);
    // if(member!=undefined) return of(member);
    const member: Member= [...this.membersCache.values()]
      .reduce((arr, elem) => arr.concat(elem.body), [])
      .find((m: Member) => m.memberLoginName === memberLoginName);
      console.log('member'+memberLoginName);
      if(member) return of(member);
    return this.http.get<Member>(this.baseUrl+'members/'+memberLoginName);
  }
  createMember(member: Member): Observable<Member> {
    return this.http.post<Member>(this.baseUrl + 'members/', member);
  }
  updateMember(member: Member | undefined): Observable<Member> {
    return this.http.put<Member>(this.baseUrl + 'members/', member).pipe(
      // tap(() =>{
      //   this.members.update(members => members.map(
      //     m => m.memberLoginName === member?.memberLoginName ? member : m))
      // })
    )
  }
  setMemberMainPhoto(photo: Photo): Observable<any> {
    return this.http.put(this.baseUrl + 'members/set-main-photo/' + photo.id, {})
      .pipe(
      // tap(() =>{
      //   this.members.update(member => member.map(m =>{
      //     if(m.photos.includes(photo)){
      //       m.imageUrl = photo.url
      //     }
      //     return m;
      //   }))
      // })
    )
  }

  deleteMemberPhoto(photo: Photo): Observable<any> {
    return this.http.delete(this.baseUrl + 'members/delete-photo/' + photo.id)
      .pipe(
      // tap(() =>{
      //   this.members.update(member => member.map(m =>{
      //     if(m.photos.includes(photo)){
      //       m.imageUrl = photo.url
      //     }
      //     return m;
      //   }))
      // })
    )
  }
}