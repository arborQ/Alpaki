import { Component } from '@angular/core';
import { UsersService } from '../users.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.less']
})
export class UserListComponent {

  constructor(private usersService: UsersService) { }

  users$ = this.usersService.users();

  userList$ = this.users$.pipe(map(a => a));

  removeUser(userId: number) {
    this.usersService.deleteUser(userId);
  }

  updateUser(userId: numer): void {
    
  }
}
