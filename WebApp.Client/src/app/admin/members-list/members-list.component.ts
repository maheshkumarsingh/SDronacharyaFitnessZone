import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { Member } from '../../_models/member';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { Membership } from '../../_models/membership';
import { MemberService } from '../../_services/member.service';
import { PageChangedEvent, PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [FormsModule, NgFor, NgIf, NgClass, RouterLink, PaginationModule],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit {

  memberService = inject(MemberService);
  pageNumber = 1;
  pageSize = 5;



  ngOnInit(): void {
    if (!this.memberService.paginatedResult())
      this.fetchMembers();
  }

  fetchMembers() {
    this.memberService.getAllMembers(this.pageNumber, this.pageSize);
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
    if (this.pageNumber !== $event.page) {
      this.pageNumber = $event.page;
      this.fetchMembers();
    }
  }
}
