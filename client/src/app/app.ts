import { Component, inject, signal, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  private http = inject(HttpClient);
  protected readonly title = signal('Dating App');

  ngOnInit(): void {
    this.http.get('https://localhost:3000/api/members').subscribe(
      next => {
        console.log(next);
      }