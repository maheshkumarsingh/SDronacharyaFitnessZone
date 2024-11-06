import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl: string = environment.apiUrl
  private http = inject(HttpClient);
  
  getAllMembers():Observable<Member[]>{
    return this.http.get<Member[]>(this.baseUrl+'members');
  }
  getMemberByMemberLoginName(memberLoginName: string): Observable<Member>{
    return this.http.get<Member>(this.baseUrl+'members/'+memberLoginName);
  }
  createMember(member: Member):Observable<Member>{
    return this.http.post<Member>(this.baseUrl+'members/', member);
  }
}