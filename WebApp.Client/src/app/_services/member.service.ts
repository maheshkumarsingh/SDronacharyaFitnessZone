import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { Observable, of, tap } from 'rxjs';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl: string = environment.apiUrl
  private http = inject(HttpClient);
  members = signal<Member[]>([]);

  getAllMembers(){
    return this.http.get<Member[]>(this.baseUrl+'members').subscribe({
      next: members => this.members.set(members),
    });
  }
  getMemberByMemberLoginName(memberLoginName: string): Observable<Member>{
    const member = this.members().find(x => x.memberLoginName === memberLoginName);
    if(member!=undefined) return of(member);
    return this.http.get<Member>(this.baseUrl+'members/'+memberLoginName);
  }
  createMember(member: Member):Observable<Member>{
    return this.http.post<Member>(this.baseUrl+'members/', member);
  }
  updateMember(member:Member|undefined):Observable<Member>{
    return this.http.put<Member>(this.baseUrl+'members/', member).pipe(
      tap(() =>{
        this.members.update(members => members.map(
          m => m.memberLoginName === member?.memberLoginName ? member : m))
      })
    )
  }
  setMemberMainPhoto(photo: Photo){
    return this.http.put(this.baseUrl+'members/set-main-photo/'+photo.id,{}).pipe(
      tap(() =>{
        this.members.update(member => member.map(m =>{
          if(m.photos.includes(photo)){
            m.imageUrl = photo.url
          }
          return m;
        }))
      })
    )
  }
}