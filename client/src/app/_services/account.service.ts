import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseurl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private presence: PresenceService) {}
  login(model: any) {
    return this.http.post<User>(this.baseurl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
          this.presence.createHubConnection(user);
        }
        this.currentUserSource.next(user);
      })
    );
  }
  register(model: any) {
    return this.http.post<User>(this.baseurl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
          this.presence.createHubConnection(user);
          // console.log(this.currentUser$);
        }
      })
    );
  }
  setCurrentUser(user: User | null) {
    user!.roles = [];
    const roles = this.getDecodedToken(user!.token).role;
    Array.isArray(roles) ? (user!.roles = roles) : user!.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.presence.stopHubConnection();
  }
  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
