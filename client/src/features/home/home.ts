import { Component, Input, signal } from '@angular/core';
import { Register } from "../account/register/register";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [Register, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  protected registerMode = signal(false);

  showRegister(value: boolean): void {
    this.registerMode.set(value);
  }
}