import { Component, OnInit, Input } from '@angular/core';
import { UserRole } from 'src/app/enums/user.role.enum';

@Component({
  selector: 'app-user-roles',
  templateUrl: './user-roles.component.html',
  styleUrls: ['./user-roles.component.less']
})
export class UserRolesComponent implements OnInit {

  constructor() { }

  @Input() currentRole: UserRole;

  ngOnInit(): void {
  }

  isAdmin(): boolean {
    return this.hasRole(UserRole.Admin);
  }

  isVolounteer(): boolean {
    return this.hasRole(UserRole.Volunteer);
  }

  isCoordinator(): boolean {
    return this.hasRole(UserRole.Coordinator);
  }

  noRole(): boolean {
    return !this.isVolounteer();
  }

  private hasRole(role: UserRole): boolean {
    return (this.currentRole & role) === role;
  }
}
