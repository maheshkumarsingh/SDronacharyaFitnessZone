import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf,HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient)
  title = 'SDronacharyaFitnessZone.Client';
  members : any;
  ngOnInit(): void {
    this.http.get('https://localhost:7062/api/Members').subscribe({
      next : response => this.members = response,
      error : error => console.log(error),
      complete: () => console.log('Request has been completed')
    });
  }
}
