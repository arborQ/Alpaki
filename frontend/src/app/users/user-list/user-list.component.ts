import { Component, ViewChild } from '@angular/core';
import { UsersService } from '../users.service';
import { map, switchMap, distinctUntilKeyChanged } from 'rxjs/operators';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.less']
})
export class UserListComponent {

  constructor(
    private usersService: UsersService,
    private activeRoute: ActivatedRoute,
    private router: Router
  ) { }


  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  isLoading = true;

  displayedColumns = ['firstName', 'lastName', 'email', 'phoneNumber'];
  currentPage$ = this.activeRoute.queryParams.pipe(map(params => +params.page ?? 1));
  userResponse$ = this.activeRoute.queryParams
    .pipe(
      map(queryParams => ({
        page: queryParams.page ? +queryParams.page : 1,
        sortDir: queryParams.sortDir ? queryParams.sortDir : 'asc',
        sortBy: queryParams.sortDir ? queryParams.sortDir : 'email',
      })))
    .pipe(distinctUntilKeyChanged<{ page: number }>('page'))
    .pipe(switchMap(queryParams => this.usersService.users(queryParams)));

  removeUser(userId: number) {
    this.usersService.deleteUser(userId);
  }

  updateUser(userId: number): void {
    this.usersService.updateUser(userId);
  }

  onPageChange($event: PageEvent): void {
    this.router.navigate(['users/list'], { queryParams: { page: $event.pageIndex }, queryParamsHandling: 'merge' });
  }
}
