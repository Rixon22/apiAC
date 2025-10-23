import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastService } from '../../core/services/toast-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  private router = inject(Router);
  private toast = inject(ToastService);
  protected accountService = inject(AccountService);
  protected creeds: any = {};

  login() {
    this.accountService.login(this.creeds).subscribe({
      next: (response: any) => {
        this.router.navigateByUrl('/members');
        this.creeds = {};
        this.toast.success("Logged in!");
      },

      error: error => {
        this.toast.error(error.error);
      }
    });
  }

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
