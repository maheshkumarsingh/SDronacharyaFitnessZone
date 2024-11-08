import { Component, inject, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgIf, TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe, NgIf],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{
  loginMember: any={};
  public accountService = inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);

  ngOnInit(): void {
    
  }
  login()
  {
    this.accountService.login(this.loginMember).subscribe({
      next: _ => {
        if(this.accountService.currentMember()?.memberLoginName==='meet2mahesh17')
        {
          this.router.navigateByUrl('/admin-dashboard');
        }
      },
      error: error => this.toastr.error(error.error)      
    })
  }
  logout()
  {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
