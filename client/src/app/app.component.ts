import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { isEmpty } from 'rxjs/operators';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'The Dating app';
  users: any;
  constructor(private accountService: AccountService) {}
  ngOnInit() {
    this.setCurrentUser();
  }
  setCurrentUser() {
    let user: User | null = JSON.parse(localStorage.getItem('user') || '{}');
    if (Object.keys(user as User).length === 0) {
      user = null;
    }
    this.accountService.setCurrentUser(user);
  }
}
