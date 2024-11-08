import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { Member } from '../../_models/member';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { Membership } from '../../_models/membership';
import { MemberService } from '../../_services/member.service';

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [FormsModule, NgFor, NgIf, NgClass, RouterLink],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit{
  //members: Member[] = [];
  memberService = inject(MemberService);
  
  ngOnInit(): void {
    if(this.memberService.members.length === 0)
      this.fetchMembers();
  }

  fetchMembers() {
    this.memberService.getAllMembers();
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
        return 'Unknown'; // Fallback in case of unexpected value
    }
  }  
}
