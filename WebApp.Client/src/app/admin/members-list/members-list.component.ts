import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { Member } from '../../_models/member';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { Membership } from '../../_models/membership';
import { MemberService } from '../../_services/member.service';
import { PageChangedEvent, PaginationModule } from 'ngx-bootstrap/pagination';
import { AccountService } from '../../_services/account.service';
import { UserParams } from '../../_models/userParams';

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [FormsModule, NgFor, NgIf, NgClass, RouterLink, PaginationModule, FormsModule],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit {

  memberService = inject(MemberService);
  private accountService = inject(AccountService);
  //userParams = new UserParams(this.accountService.currentMember());
  userParams = new UserParams();
  selectedMembershipType: number = 0;
  genderList = [
                  { value: 0, display: 'Select' }, 
                  { value: 'Male', display: 'Males' }, 
                  { value: 'Female', display: 'Females' }
               ];
  planList = [
                { value: '', display: 'Select' }, 
                { value: 'Monthly', display: 'Monthly' }, 
                { value: 'Quaterly', display: 'Quaterly' },
                { value: 'Half_Yearly', display: 'Half-Yearly' },
                { value: 'Yearly', display: 'Yearly' },
             ]
  ngOnInit(): void {
    if (!this.memberService.paginatedResult())
      this.fetchMembers();
    this.userParams.plan='';
    this.userParams.gender =0;
    this.userParams.planStatus = 0;
  }
  resetFilters() {
    this.userParams = new UserParams();
    this.fetchMembers();
  }
  fetchMembers() {
    this.memberService.getAllMembers(this.userParams);
    console.log('Memers-fetched')
  }
  getLatestMembership(memberships: Membership[]): Membership | null {
    if (!memberships || memberships.length === 0) {
      return null;
    }
    return memberships.reduce((latest, current) =>
      new Date(current.membershipEndDate) > new Date(latest.membershipEndDate) ? current : latest
    );
  }
  getMembershipType(type: number): string {
    switch (type) {
      case 0:
        return 'Monthly';
      case 1:
        return 'Quarterly';
      case 2:
        return 'Half-Yearly';
      case 3:
        return 'Yearly';
      default:
        return 'Unknown';
    }
  }
  pageChanged($event: PageChangedEvent) {
    if (this.userParams.pageNumber !== $event.page) {
      this.userParams.pageNumber = $event.page;
      this.fetchMembers();
    }
  }
}
