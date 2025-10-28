import { Component } from '@angular/core';
import { inject } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MembersService } from '../../../core/services/memebers-service';
import { Observable } from 'rxjs';
import { Member } from '../../../types/members';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-member-detail',
  imports: [AsyncPipe, RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './member-detail.html',
  styleUrl: './member-detail.css'
})
export class MemberDetail {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  protected member$?: Observable<Member>;

  ngOnInit(): void {
    this.member$ = this.loadMember() as Observable<Member>;
    throw new Error('Method not implemented.');
  }

  loadMember(): Observable<Member> | undefined {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      return this.memberService.getMember(id);
    }
    return;
  }
}
