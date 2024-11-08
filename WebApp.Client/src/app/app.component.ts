import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from "./navbar/navbar.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";
import { NgxSpinnerComponent } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FooterComponent, NavbarComponent, HomeComponent, NgxSpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title='SDronacharya Fitness Zone'
  private accountService = inject(AccountService);
  members: any;
  ngOnInit(): void {
    this.setCurrentMember();
  }
  setCurrentMember()
  {
    const memberString = localStorage.getItem('member');
    if(!memberString) return;
    const member = JSON.parse(memberString);
    this.accountService.currentMember.set(member);
  }
}
