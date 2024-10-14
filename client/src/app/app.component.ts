import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';
import { NgxSpinnerComponent } from 'ngx-spinner';
import { NavComponent } from './nav/nav.component';
import { RouterOutlet } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: true,
    imports: [NgxSpinnerComponent, NavComponent, RouterOutlet]
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  constructor (private accountSevice: AccountService) {}

  ngOnInit(): void {
    this.setcurrentUser();
  }

  

  setcurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user: User = JSON.parse(userString); 
    this.accountSevice.setCurrentUser(user);
  }

}
