import { Component, inject, signal, OnInit, Signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { last, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  private http = inject(HttpClient);
  protected readonly title = signal('Dating App');
  protected members = signal<any>([]);

  async ngOnInit(): Promise<void> {
    this.members.set(await this.getMembers());
  }

  async getMembers(): Promise<Object> {
    try {
      return await lastValueFrom(this.http.get('https://localhost:7177/api/members'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}
