import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { MembersService } from 'src/app/_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm, FormsModule } from '@angular/forms';
import { NgIf, DatePipe } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PhotoEditorComponent } from '../photo-editor/photo-editor.component';
import { TimeagoModule } from 'ngx-timeago';

@Component({
    selector: 'app-member-edit',
    templateUrl: './member-edit.component.html',
    styleUrl: './member-edit.component.css',
    standalone: true,
    imports: [NgIf, TabsModule, FormsModule, PhotoEditorComponent, DatePipe, TimeagoModule]
})
export class MemberEditComponent implements OnInit {
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  private memberService = inject(MembersService); 
  @ViewChild('editForm') editForm: NgForm | undefined;
  member: Member | undefined;
  user = this.accountService.currentUser()
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  
  ngOnInit(): void {
    this.loadMember()
  }
  
  loadMember() {
    if (!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member => this.member = member
    })
  }

  updateMember(){
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Profile updated successfully <3');
        this.editForm?.reset(this.member);
      }
    })
  }
}