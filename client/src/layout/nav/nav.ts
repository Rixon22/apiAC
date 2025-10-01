import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected accountService = inject(AccountService);
  protected creeds: any = {};

  login() {
    this.accountService.login(this.creeds).subscribe({
      next: (response: any) => {
        console.log(response);
        this.creeds = {};
      },
      error: (error: any) => {
        console.log(error);
      }
    });
  }

  logout(): void {
    this.accountService.logout();
  }
}
