import { Component, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { RegisterComponent } from '../register/register.component';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: true,
    imports: [NgIf, RegisterComponent]
})
export class HomeComponent implements OnInit{
  registerMode = false;
  users: any;
  
  constructor() { }

  ngOnInit(): void {
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

}
