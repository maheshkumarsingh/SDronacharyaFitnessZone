import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css'
})
export class TestErrorsComponent {
  private baseURL ='https://localhost:7221/api/';
  private http= inject(HttpClient);

  get400Error(){
    this.http.get(this.baseURL + 'buggy/bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get401Error(){
    this.http.get(this.baseURL + 'buggy/auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get404Error(){
    this.http.get(this.baseURL + 'buggy/not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get500Error(){
    this.http.get(this.baseURL + 'buggy/server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get400ValidationError(){
    this.http.get(this.baseURL + 'buggy/register').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
}
