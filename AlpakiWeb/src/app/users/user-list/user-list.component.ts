import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { UsersService } from '../users.service';
import { map, switchMap, last, takeLast, takeWhile, filter, take, skip, flatMap, concatMap, distinctUntilChanged, distinctUntilKeyChanged } from 'rxjs/operators';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSort, Sort } from '@angular/material/sort';
import { combineLatest } from 'rxjs';
import { query } from '@angular/animations';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.less']
})
export class UserListComponent {

  constructor(private usersService: UsersService, private activeRoute: ActivatedRoute, private router: Router) { }


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

  // ngOnInit(): void {
  //   this.activeRoute.queryParams.subscribe(a => {
  //     console.log({ a });
  //   });
  // }
  // ngAfterViewInit(): void {
  //   // this.sort.sortChange.subscribe((sort: Sort) => {
  //   //   this.router.navigate(['users/list'], { queryParams: { sortBy: sort.active, sortDir: sort.direction }, queryParamsHandling: 'merge' });
  //   // });

  //   // this.userResponse$.subscribe(_ => {
  //   //   console.log({ _ });
  //   //   this.isLoading = _.loading;
  //   // });
  // }


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
