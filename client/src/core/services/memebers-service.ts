import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member, Photo } from '../../types/members';
import { Observable } from 'rxjs';
import { AccountService } from './account-service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;
  private accountService = inject(AccountService);

  getMember(id: string): Observable<Member> {
    return this.http.get<Member>(this.baseUrl + "members/" + id)
  }

  getMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(this.baseUrl + "members");
  }

  getPhotos(id: string) {
    return this.http.get<Photo[]>(`${this.baseUrl}members/${id}/photos`);
  }

  private getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: "Bearer " + this.accountService.currentUser()?.token
      })
    };
  }
}