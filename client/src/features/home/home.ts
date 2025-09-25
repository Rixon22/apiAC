import { Component, Input, signal } from '@angular/core';
import { Register } from "../account/register/register";
import { FormsModule } from '@angular/forms';
import { User } from '../../core/types/user';

@Component({
  selector: 'app-home',
  imports: [Register, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  @Input({ required: true }) membersFromApp: User[] = [];
  protected registerMode = signal(false);

  showRegister(): void {
    this.registerMode.set(true);
  }
}