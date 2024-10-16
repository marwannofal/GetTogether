import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgIf, AsyncPipe, TitleCasePipe } from '@angular/common';
import { HasRoleDirective } from '../_directives/has-role.directive';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css'],
    standalone: true,
    imports: [RouterLink, RouterLinkActive, NgIf, HasRoleDirective, BsDropdownModule, FormsModule, AsyncPipe, TitleCasePipe]
})
export class NavComponent implements OnInit {
  model: any = {}

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService){}

  ngOnInit(): void {
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl('/members')
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}