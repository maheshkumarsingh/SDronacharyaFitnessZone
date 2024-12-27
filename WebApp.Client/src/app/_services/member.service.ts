import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { Observable, of, Subscription, tap } from 'rxjs';
import { Photo } from '../_models/photo';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl: string = environment.apiUrl
  private http = inject(HttpClient);
  // members = signal<Member[]>([]);
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null);

  getAllMembers(pageNumber?: number , pageSize?:number):Subscription{
    let params = new HttpParams();
    if(pageNumber && pageSize){
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    //https://localhost:7221/api/members?pageNumber=1&pageSize=5
    return this.http.get<Member[]>(this.baseUrl+'members', {observe: 'response', params}).subscribe({
      next: response =>{
        this.paginatedResult.set({
          items: response.body as Member[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        })
      }
    });
  }
  getMemberByMemberLoginName(memberLoginName: string){
    // const member = this.members().find(x => x.memberLoginName === memberLoginName);
    // if(member!=undefined) return of(member);
    // return this.http.get<Member>(this.baseUrl+'members/'+memberLoginName);
  }
  createMember(member: Member):Observable<Member>{
    return this.http.post<Member>(this.baseUrl+'members/', member);
  }
  updateMember(member:Member|undefined):Observable<Member>{
    return this.http.put<Member>(this.baseUrl+'members/', member).pipe(
      // tap(() =>{
      //   this.members.update(members => members.map(
      //     m => m.memberLoginName === member?.memberLoginName ? member : m))
      // })
    )
  }
  setMemberMainPhoto(photo: Photo):Observable<any>{
    return this.http.put(this.baseUrl+'members/set-main-photo/'+photo.id,{})
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

  deleteMemberPhoto(photo:Photo):Observable<any>{
    return this.http.delete(this.baseUrl+'members/delete-photo/'+photo.id)
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