import { Component, inject, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { Membership } from '../../_models/membership';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { MemberService } from '../../_services/member.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { Supplement } from '../../_models/supplement';
import { SupplementOrder } from '../../_models/SupplementOrder';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [DatePipe,NgIf, NgFor, NgClass, CurrencyPipe, TabsModule, GalleryModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private memberService = inject(MemberService);
  member?: Member;
  memberships: Membership[] = [];
  memberLoginName: string|null = null;
  totalDueAmount :number =0;
  totalPaidAmount :number =0;
  images: GalleryItem[] =[];
  supplementOrdered: SupplementOrder[] =[];
  
  ngOnInit(): void {
    this.memberLoginName = this.route.snapshot.paramMap.get('memberLoginName'); //while routeLink we have passed memberLoginName
    if (this.memberLoginName) {
      this.fetchMemberDetails(this.memberLoginName);
    }
  }

  fetchMemberDetails(loginName: string): void {
    this.memberService.getMemberByMemberLoginName(loginName).subscribe({
      next: (member) => {
        this.member = member;
        this.memberships = member.memberships || [];
        member.photos.map(p=>{
          this.images.push(new ImageItem({src: p.url, thumb: p.url}))
        });
        this.supplementOrdered = member.supplementOrders;
        this.calculateAmounts();
      },
      error: (err) => {
        console.error('Error fetching member details:', err);
      }
    });
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
  calculateAmounts():void{
    let sumForDue = 0;
    let sumForPaid = 0;
    if(this.memberships.length>0)
    {
      for (let index = 0; index < this.memberships.length; index++) {
        sumForDue += this.memberships[index].dueAmount;
        sumForPaid += this.memberships[index].paidAmount;
      }
      this.totalDueAmount = sumForDue;
      this.totalPaidAmount = sumForPaid;
    }
  }
}
