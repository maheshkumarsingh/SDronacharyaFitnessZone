import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Member } from '../_models/member';
import { map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  private baseURL = environment.apiUrl
  currentMember = signal<Member | null>(null);

  login(model:any):Observable<void>
  {
    return this.http.post<Member>(this.baseURL + 'account/login', model).pipe(
      map(member =>{
        if(member){
          localStorage.setItem('member', JSON.stringify(member));
          this.currentMember.set(member);
        }
      })
    )
  }
  logout(){
    localStorage.removeItem('member');
    this.currentMember.set(null);
  }
}
