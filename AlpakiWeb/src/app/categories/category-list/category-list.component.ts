import { Component } from '@angular/core';
import { CategoriesService } from '../categories.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.less']
})
export class CategoryListComponent {

  constructor(private categoriesService: CategoriesService) { }

  categoriesResponse$ = this.categoriesService.getCategories();
  categories$ = this.categoriesResponse$.pipe(map(response => response.data.categories));
  isLoading$ = this.categoriesResponse$.pipe(map(response => response.loading));

  submitUpdateCategory() {
    console.log('submitUpdateCategory');
  }
  submitCreateNewCategory() {
    console.log('submitCreateNewCategory');
  }
}
