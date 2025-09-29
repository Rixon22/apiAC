import { Component, inject, input, output } from '@angular/core';
import { RegisterCreds } from '../../../core/types/user';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private accountService = inject(AccountService);
  canceledRegister = output<boolean>();
  protected creds = {} as RegisterCreds;

  register(): void {
    console.log(this.creds);
  }

  cancel(): void {
    this.canceledRegister.emit(false);
  }
}