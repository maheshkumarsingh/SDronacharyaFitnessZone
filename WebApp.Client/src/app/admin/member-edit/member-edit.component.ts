import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MemberService } from '../../_services/member.service';
import { Member } from '../../_models/member';
import { DatePipe, NgIf } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [DatePipe, TabsModule, NgIf, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event:any) {
    if(this.editForm?.dirty)
    {
      $event.returnValue = true;
    }
  }
  private route = inject(ActivatedRoute);
  private memberService = inject(MemberService);
  private toastr = inject(ToastrService);
  member?: Member;

  ngOnInit(): void {
    this.loadMemberToEdit();
  }
  updateMember() {
    console.log('update member');
    this.memberService.updateMember(this.member).subscribe({
      next: response => this.member = response,
      error: (err)=>{
        console.error('Error updating member details:', err);
      }
    });
    this.toastr.success('Profile update successfully');
    this.editForm?.reset(this.member);
  }

  loadMemberToEdit() {
    const memberLoginName = this.route.snapshot.paramMap.get('memberLoginName')!;
    console.log('loadMember' + memberLoginName);
    if (!memberLoginName) return;
    this.memberService.getMemberByMemberLoginName(memberLoginName).subscribe({
      next: member => this.member = member,
      error: (err) => {
        console.error('Error fetching member details:', err);
      }
    })
  }
}
