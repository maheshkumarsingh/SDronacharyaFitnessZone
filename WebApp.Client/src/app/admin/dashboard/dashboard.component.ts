import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { MembersListComponent } from "../members-list/members-list.component";
import { MemberCreateComponent } from "../member-create/member-create.component";
import { ReportComponent } from "../report/report.component";
import { RouterLink } from '@angular/router';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MembersListComponent, RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  accountService = inject(AccountService);
  ngOnInit(): void {
    console.log('admin-dashoard');
  }
}
