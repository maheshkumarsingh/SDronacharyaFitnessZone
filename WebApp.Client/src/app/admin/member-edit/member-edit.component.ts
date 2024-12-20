import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MemberService } from '../../_services/member.service';
import { Member } from '../../_models/member';
import { DatePipe, NgIf } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PhotoEditorComponent } from "../photo-editor/photo-editor.component";

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [DatePipe, TabsModule, NgIf, FormsModule, PhotoEditorComponent],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  private route = inject(ActivatedRoute);
  private memberService = inject(MemberService);
  private toastr = inject(ToastrService);
  member?: Member;
  photoUrl: string | undefined;
  confirmPassword: string | undefined;
  passwordVisibility: boolean = false;

  ngOnInit(): void {
    this.loadMemberToEdit();
  }
  updateMember() {
    if (this.member?.password === this.confirmPassword) {
      console.log('Updating Member');
      //console.log(this.member?.password + ' And '+ this.confirmPassword);
      this.memberService.updateMember(this.member).subscribe({
        next: (response) => {
          this.member = response
          this.toastr.success('Profile update successfully');
        },
        error: (err) => {
          console.error('Error updating member details:', err);
        }
      });
      this.editForm?.reset(this.member);
    }
    else {
      console.log(this.member?.password + ' And ' + this.confirmPassword);
      this.toastr.error('Profile not updated. Confirm Password doesnot matches with Password');
    }
  }

  loadMemberToEdit() {
    const memberLoginName = this.route.snapshot.paramMap.get('memberLoginName')!;
    console.log('loadMember' + memberLoginName);
    if (!memberLoginName) return;
    this.memberService.getMemberByMemberLoginName(memberLoginName).subscribe({
      next: (member) => {
        this.member = member,
          this.photoUrl = this.member.photos.find(p => p.isMain)?.url;
        console.log('photoUrl' + this.photoUrl);
      },
      error: (err) => {
        console.error('Error fetching member details:', err);
      }
    })
  }
  onMemberChange(event: Member) {
    this.member = event;
  }
  togglePasswordVisibility() {
    this.passwordVisibility = !this.passwordVisibility;
  }
}
