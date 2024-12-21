import { ChangeDetectorRef, Component, inject, input, OnInit, output } from '@angular/core';
import { Member } from '../../_models/member';
import { DecimalPipe, NgClass, NgFor, NgIf, NgStyle } from '@angular/common';
import {FileUploader, FileUploadModule} from 'ng2-file-upload';
import { AccountService } from '../../_services/account.service';
import { environment } from '../../../environments/environment';
import { Photo } from '../../_models/photo';
import { MemberService } from '../../_services/member.service';
@Component({
  selector: 'app-photo-editor',
  standalone: true,
  imports: [NgIf, NgFor,NgStyle,NgClass, FileUploadModule, DecimalPipe],
  templateUrl: './photo-editor.component.html',
  styleUrl: './photo-editor.component.css'
})
export class PhotoEditorComponent implements OnInit{
  private accountService = inject(AccountService);
  private memberService = inject(MemberService);
  member = input.required<Member>();
  uploader?: FileUploader;
  hasBaseDropZoneOver=false;
  baseUrl=environment.apiUrl;
  memberChange = output<Member>();
  ngOnInit(): void {
    this.initializeUploader();
  }
  fileOverBase(e:any){
    this.hasBaseDropZoneOver =e;
  }
  initializeUploader(){
    this.uploader = new FileUploader({
      url: this.baseUrl+'members/add-photo',
      authToken: 'Bearer '+this.accountService.currentMember()?.token,
      isHTML5:true,
      allowedFileType:['image'],
      removeAfterUpload:true,
      autoUpload:false,
      maxFileSize:10*1024*1024,
    });
    this.uploader.onAfterAddingFile = (file) =>{
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers)=>{
      const photo = JSON.parse(response);
      let updatedMember = {...this.member()};
      updatedMember.photos.push(photo);
      this.memberChange.emit(updatedMember);
    }
  }
  deleteMemberPhoto(photo:Photo){
    this.memberService.deleteMemberPhoto(photo).subscribe({
      next: _ =>{
        const updatedMember = {...this.member()};
        updatedMember.photos = updatedMember.photos.filter(x => x.id !== photo.id);
        this.memberChange.emit(updatedMember);
      }
    })
  }
  setMemberMainPhoto(photo:Photo)
  {
    this.memberService.setMemberMainPhoto(photo).subscribe({
      next: _ =>{
        const currentMember = this.accountService.currentMember();
        if(currentMember)
        {
          currentMember.imageUrl = photo.url;
          this.accountService.currentMember.set(currentMember);
        }
        const updatedMember = {...this.member()};
        updatedMember.imageUrl = photo.url;
        updatedMember.photos.forEach(p => {
          if(p.isMain) p.isMain = false;
          if(p.id === photo.id) p.isMain = true;
        });
        this.memberChange.emit(updatedMember);
      }
    })
  }
}
