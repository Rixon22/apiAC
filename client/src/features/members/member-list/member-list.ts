import { Component, inject, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from '../../../types/members';
import { MembersService } from '../../../core/services/memebers-service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-member-list',
  imports: [AsyncPipe],
  templateUrl: './member-list.html',
  styleUrl: './member-list.css'
})
export class MemberList {
  private MembersService = inject(MembersService);
  protected members$: Observable<Member[]>;

  constructor() {
    this.members$ = this.MembersService.getMembers();
  }
}
