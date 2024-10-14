import { Component, OnInit } from '@angular/core';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { HasRoleDirective } from '../../_directives/has-role.directive';
import { UserManagementComponent } from '../user-management/user-management.component';
import { PhotoManagementComponent } from '../photo-management/photo-management.component';

@Component({
    selector: 'app-admin-panel',
    templateUrl: './admin-panel.component.html',
    styleUrls: ['./admin-panel.component.css'],
    standalone: true,
    imports: [TabsModule, HasRoleDirective, UserManagementComponent, PhotoManagementComponent]
})
export class AdminPanelComponent implements OnInit {
  
  constructor() { }
  
  ngOnInit(): void {
  }

}
